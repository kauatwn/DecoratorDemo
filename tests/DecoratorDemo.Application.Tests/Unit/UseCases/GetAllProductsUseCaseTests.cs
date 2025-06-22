using DecoratorDemo.Application.UseCases;
using DecoratorDemo.Domain.Entities;
using DecoratorDemo.Domain.Interfaces.Repositories;
using Moq;

namespace DecoratorDemo.Application.Tests.Unit.UseCases;

public class GetAllProductsUseCaseTests
{
    private readonly Mock<IProductRepository> _mockRepository = new();
    private readonly GetAllProductsUseCase _useCase;

    public GetAllProductsUseCaseTests() => _useCase = new GetAllProductsUseCase(_mockRepository.Object);

    [Fact]
    public async Task ShouldGetAllProducts()
    {
        // Arrange
        List<Product> products = [
            new(id: 1, name: "Notebook", price: 5_000m),
            new(id: 2, name: "Smartphone", price: 3_000m),
            new(id: 3, name: "Tablet", price: 2_000m)
        ];

        _mockRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(products);

        // Act
        IEnumerable<Product> result = await _useCase.Execute();

        // Assert
        Assert.Equal(products, result);
    }
}