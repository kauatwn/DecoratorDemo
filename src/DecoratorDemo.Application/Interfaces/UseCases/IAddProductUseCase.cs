using DecoratorDemo.Domain.Entities;

namespace DecoratorDemo.Application.Interfaces.UseCases;

public interface IAddProductUseCase
{
    void Execute(Product product);
}