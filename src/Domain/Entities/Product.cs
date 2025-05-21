namespace Domain.Entities;

public class Product(int id, string name, double price)
{
    public int Id { get; } = id;
    public string Name { get; } = name;
    public double Price { get; } = price;
}