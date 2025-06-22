using DecoratorDemo.Application.UseCases;
using DecoratorDemo.Domain.Entities;
using DecoratorDemo.Domain.Interfaces.Repositories;
using Moq;

namespace DecoratorDemo.Application.Tests.Unit.UseCases;

public class AddProductUseCaseTests
{
    private readonly Mock<IProductRepository> _mockRepository = new();
    private readonly AddProductUseCase _useCase;

    public AddProductUseCaseTests() => _useCase = new AddProductUseCase(_mockRepository.Object);

    [Fact]
    public void ShouldAddProduct()
    {
        // Arrange
        Product product = new(id: 1, name: "Product 1", price: 10m);

        // Act
        _useCase.Execute(product);

        // Assert
        _mockRepository.Verify(r => r.Add(product), Times.Once);
    }
}