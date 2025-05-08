using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly List<Product> _products =
    [
        new(1, "Notebook", 5000),
        new(2, "Smartphone", 3000),
        new(3, "Tablet", 2000),
        new(4, "Monitor", 1500),
        new(5, "Teclado", 200),
        new(6, "Mouse", 100),
        new(7, "Impressora", 800),
        new(8, "Cadeira", 1200),
        new(9, "Mesa", 1500),
        new(10, "Fone de Ouvido", 300)
    ];

    public void Add(Product product) => _products.Add(product);

    public async Task<IReadOnlyCollection<Product>> GetAllAsync()
    {
        Console.WriteLine("--> Consultando banco de dados...");
        await Task.Delay(TimeSpan.FromSeconds(2));

        return _products.AsReadOnly();
    }
}