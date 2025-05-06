namespace Domain.Entities;

public class Product(int id, string name, decimal price)
{
    public int Id { get; set; } = id;
    public string Name { get; set; } = name;
    public decimal Price { get; set; } = price;
}