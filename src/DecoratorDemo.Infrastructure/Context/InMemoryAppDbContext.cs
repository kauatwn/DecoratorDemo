using DecoratorDemo.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DecoratorDemo.Infrastructure.Context;

public class InMemoryAppDbContext(DbContextOptions<InMemoryAppDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>().HasData(
            new Product(id: 1, name: "Notebook", price: 5_000m),
            new Product(id: 2, name: "Smartphone", price: 3_000m),
            new Product(id: 3, name: "Tablet", price: 2_000m),
            new Product(id: 4, name: "Monitor", price: 1_500m),
            new Product(id: 5, name: "Teclado", price: 200m),
            new Product(id: 6, name: "Mouse", price: 100m),
            new Product(id: 7, name: "Impressora", price: 800m),
            new Product(id: 8, name: "Cadeira", price: 1_200m),
            new Product(id: 9, name: "Mesa", price: 1_500m),
            new Product(id: 10, name: "Fone de Ouvido", price: 300m)
        );
    }
}