# Decorator Demo

Este projeto é uma API demonstrativa que implementa o padrão **Decorator** para adicionar funcionalidade de cache em memória a um repositório fake de produtos. A implementação segue os princípios SOLID, com foco especial no **Open/Closed Principle**.

## Estrutura do Projeto

O projeto está organizado da seguinte forma:

```plaintext
Decorator_Demo/
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
│       ├── Decorators/
│       │   ├── Bases/
│       │   │   └── ProductRepositoryDecorator.cs
│       │   └── CachedProductRepository.cs
│       ├── Repositories/
│       │   └── ProductRepository.cs
│       └── Services/
│           └── MemoryCacheService.cs
└── tests/
    ├── Application.Tests/
    └── Infrastructure.Tests/
```

## Como Funciona

O padrão Decorator é utilizado para adicionar comportamentos adicionais a um repositório de produtos sem modificar sua implementação original. Neste exemplo, implementamos:

1. **ProductRepository (ConcreteComponent)**
    - Implementação fake com lista em memória
    - Simula latência de banco de dados (2 segundos)
    - Contém 10 produtos pré-cadastrados

2. **CachedProductRepository (ConcreteDecorator)**
    - Adiciona camada de cache com 30 segundos de expiração
    - Invalida cache automaticamente ao adicionar novos produtos
    - Retorna dados do cache quando disponível

3. **ProductRepositoryDecorator (Decorator Base)**
    - Classe abstrata que implementa o padrão decorator
    - Mantém referência para o objeto decorado
