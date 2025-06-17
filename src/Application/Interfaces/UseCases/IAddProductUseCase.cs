using Domain.Entities;

namespace Application.Abstractions;

public interface IAddProductUseCase
{
    void Execute(Product product);
}