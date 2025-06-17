using Application.UseCases;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Moq;

namespace Application.Tests.Unit.UseCases;

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
        _mockRepository.Verify(r => r.Add(It.Is<Product>(p => 
            p.Id == product.Id && 
            p.Name == product.Name && 
            p.Price == product.Price)), 
        Times.Once);
    }
}