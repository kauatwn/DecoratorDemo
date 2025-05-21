using Application.Abstractions;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases;

public class AddProductUseCase(IProductRepository repository) : IAddProductUseCase
{
    public void Execute(Product product) => repository.Add(product);
}