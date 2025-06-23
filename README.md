# Decorator Demo

Este projeto é uma API demonstrativa que implementa o padrão **Decorator** para adicionar funcionalidade de cache em memória a um repositório de produtos. A implementação segue os princípios da **Clean Architecture** e **SOLID**, com foco especial no **Open/Closed Principle**, garantindo um código extensível e de fácil manutenção.

## Sumário

- [Pré-requisitos](#pré-requisitos)
- [Como Executar](#como-executar)
  - [Clone o Projeto](#clone-o-projeto)
  - [Executar com Docker](#executar-com-docker)
  - [Executar Localmente com .NET SDK](#executar-localmente-com-net-sdk)
- [Estrutura do Projeto](#estrutura-do-projeto)
- [Como Funciona](#como-funciona)
- [Fluxo Simplificado](#fluxo-simplificado)

## Pré-requisitos

Escolha uma das seguintes opções para executar o projeto:

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/)
- [Postman](https://www.postman.com/) ou similar (para testar endpoints)

## Como Executar

Você pode executar o projeto de duas formas:

1. **Com Docker** (recomendado para evitar configurações locais)
2. **Localmente com .NET SDK** (caso já tenha o ambiente .NET configurado)

### Clone o Projeto

Clone este repositório em sua máquina local:

```bash
git clone https://github.com/kauatwn/DecoratorDemo.git
```

### Executar com Docker

1. Navegue até a pasta raiz do projeto:

    ```bash
    cd DecoratorDemo/
    ```

2. Construa a imagem Docker:

    ```bash
    docker build -t decoratordemoapi:dev -f src/DecoratorDemo.API/Dockerfile .
    ```

3. Execute o container:

    ```bash
    docker run --rm -it -p 5000:8080 --name DecoratorDemo.API decoratordemoapi:dev
    ```

Após executar os comandos acima, a API estará disponível em `http://localhost:5000`.

### Executar Localmente com .NET SDK

1. Navegue até o diretório da API:

    ```bash
    cd src/DecoratorDemo.API/
    ```

2. Restaure as dependências do projeto:

    ```bash
    dotnet restore
    ```

3. Inicie a aplicação:

    ```bash
    dotnet run
    ```

Após rodar a aplicação, a API ficará acessível em `http://localhost:5025`.

## Estrutura do Projeto

O projeto está organizado da seguinte forma:

```plaintext
DecoratorDemo/
├── src/
│   ├── API/
│   │   └── Controllers/
│   │       └── ProductsController.cs
│   ├── Application/
│   │   └── UseCases/
│   │       ├── AddProductUseCase.cs
│   │       └── GetAllProductsUseCase.cs
│   ├── Domain/
│   │   └── Entities/
│   │       └── Product.cs
│   └── Infrastructure/
│       ├── Context/
│       │   └── InMemoryAppDbContext.cs
│       ├── Decorators/
│       │   ├── Bases/
│       │   │   └── ProductRepositoryDecorator.cs
│       │   └── CachedProductRepository.cs
│       ├── Repositories/
│       │   └── ProductRepository.cs
│       └── Services/
│           └── MemoryCacheService.cs
└── tests/
    ├── DecoratorDemo.Application.Tests/
    └── DecoratorDemo.Infrastructure.Tests/
```

## Como Funciona

O padrão **Decorator** permite adicionar comportamentos a objetos individualmente, sem afetar o comportamento de outros objetos da mesma classe. Neste projeto, ele é usado para adicionar cache em memória a um repositório de produtos, demonstrando como estender funcionalidades de forma flexível e não intrusiva.

1. **ProductRepositoryDecorator (Decorator Base)**
    - Classe abstrata que implementa o padrão decorator
    - Mantém referência ao objeto decorado (`_decorated`)
    - Fornece a estrutura base para todos os decoradores

2. **ProductRepository (ConcreteComponent)**
    - Implementação real com Entity Framework Core
    - Usa contexto in-memory e simula latência (2s)
    - Contém a lógica "crua" sem cache

3. **CachedProductRepository (ConcreteDecorator)**
    - Herda da classe base decorator
    - Adiciona cache com TTL de 30s
    - Implementa estratégia cache-first
    - Invalida cache automaticamente em escritas

## Fluxo simplificado

1. Obter produtos (com cache)

    ```mermaid
    sequenceDiagram
        Client->>+API: GET /api/products
        API->>+CachedRepository: GetAllAsync()
        CachedRepository->>+Cache: Existe?
        Cache-->>-CachedRepository: Dados em cache
        CachedRepository-->>-API: Retorna produtos
        API-->>-Client: 200 OK
    ```

2. Obter produtos (sem cache)

    ```mermaid
    sequenceDiagram
        Client->>+API: GET /api/products
        API->>+CachedRepository: GetAllAsync()
        CachedRepository->>+Cache: Existe?
        Cache-->>-CachedRepository: Vazio
        CachedRepository->>+ProductRepository: GetAllAsync()
        ProductRepository-->>-CachedRepository: Dados (2s)
        CachedRepository->>Cache: Armazena (30s)
        CachedRepository-->>-API: Retorna produtos
        API-->>-Client: 200 OK
    ```

3. Adicionar produto

    ```mermaid
    sequenceDiagram
    Client->>+API: POST /api/products
    API->>+CachedRepository: Add(product)
    CachedRepository->>+ProductRepository: Add(product)
    ProductRepository-->>-CachedRepository: Confirmação
    CachedRepository->>Cache: Invalida
    CachedRepository-->>-API: Sucesso
    API-->>-Client: 201 Created
    ```
