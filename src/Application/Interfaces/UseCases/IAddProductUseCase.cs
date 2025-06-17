using Domain.Entities;

namespace Application.Interfaces.UseCases;

public interface IAddProductUseCase
{
    void Execute(Product product);
}