namespace DecoratorDemo.Domain.Entities;

public class Product(int id, string name, decimal price)
{
    public int Id { get; init; } = id;
    public string Name { get; init; } = name;
    public decimal Price { get; init; } = price;
}