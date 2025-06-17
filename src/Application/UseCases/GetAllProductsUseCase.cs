using Application.Interfaces.UseCases;
using Domain.Entities;
using Domain.Interfaces.Repositories;

namespace Application.UseCases;

public class GetAllProductsUseCase(IProductRepository repository) : IGetAllProductsUseCase
{
    public async Task<IEnumerable<Product>> Execute() => await repository.GetAllAsync();
}