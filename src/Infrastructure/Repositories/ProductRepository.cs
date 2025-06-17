using Domain.Entities;
using Domain.Interfaces.Repositories;

namespace Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly List<Product> _products =
    [
        new(id: 1, name: "Notebook", price: 5_000m),
        new(id: 2, name: "Smartphone", price: 3_000m),
        new(id: 3, name: "Tablet", price: 2_000m),
        new(id: 4, name: "Monitor", price: 1_500m),
        new(id: 5, name: "Teclado", price: 200m),
        new(id: 6, name: "Mouse", price: 100m),
        new(id: 7, name: "Impressora", price: 800m),
        new(id: 8, name: "Cadeira", price: 1_200m),
        new(id: 9, name: "Mesa", price: 1_500),
        new(id: 10, name: "Fone de Ouvido", price: 300m)
    ];

    public void Add(Product product) => _products.Add(product);

    public async Task<IReadOnlyCollection<Product>> GetAllAsync()
    {
        Console.WriteLine("--> Consultando banco de dados...");
        await Task.Delay(TimeSpan.FromSeconds(2));

        return _products.AsReadOnly();
    }
}