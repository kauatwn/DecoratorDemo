using Application.Abstractions;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases;

public class GetAllProductsUseCase(IProductRepository repository) : IGetAllProductsUseCase
{
    public async Task<IEnumerable<Product>> Execute() => await repository.GetAllAsync();
}