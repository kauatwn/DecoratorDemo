using Application.Interfaces.UseCases;
using Domain.Entities;
using Domain.Interfaces.Repositories;

namespace Application.UseCases;

public class AddProductUseCase(IProductRepository repository) : IAddProductUseCase
{
    public void Execute(Product product) => repository.Add(product);
}