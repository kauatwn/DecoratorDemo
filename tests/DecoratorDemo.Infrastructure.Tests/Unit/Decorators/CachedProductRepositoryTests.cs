﻿using DecoratorDemo.Application.Interfaces.Services;
using DecoratorDemo.Domain.Entities;
using DecoratorDemo.Domain.Interfaces.Repositories;
using DecoratorDemo.Infrastructure.Decorators;
using Moq;

namespace DecoratorDemo.Infrastructure.Tests.Unit.Decorators;

public class CachedProductRepositoryTests
{
    private readonly Mock<IProductRepository> _mockDecorated = new();
    private readonly Mock<ICacheService> _mockCache = new();

    private readonly CachedProductRepository _cachedRepository;

    private const string AllProductsCacheKey = "all-products";
    private static readonly TimeSpan DefaultExpiration = TimeSpan.FromSeconds(30);
    private static readonly IReadOnlyCollection<Product> CachedProducts = [new(id: 1, name: "Product 1", price: 10m)];

    public CachedProductRepositoryTests() =>
        _cachedRepository = new CachedProductRepository(_mockDecorated.Object, _mockCache.Object);

    [Fact]
    public async Task ShouldReturnCachedProductsWhenCacheIsAvailable()
    {
        // Arrange
        _mockCache.Setup(c => c.Get<IReadOnlyCollection<Product>>(AllProductsCacheKey)).Returns(CachedProducts);

        // Act
        IReadOnlyCollection<Product> result = await _cachedRepository.GetAllAsync();

        // Assert
        Assert.Same(CachedProducts, result);

        _mockDecorated.Verify(d => d.GetAllAsync(), Times.Never);
        _mockCache.Verify(c => c.Set(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<TimeSpan>()), Times.Never);
    }

    [Fact]
    public async Task ShouldFetchProductsFromRepositoryWhenCacheIsEmpty()
    {
        // Arrange
        _mockCache.Setup(c => c.Get<IReadOnlyCollection<Product>>(AllProductsCacheKey))
            .Returns((IReadOnlyCollection<Product>?)null);

        _mockDecorated.Setup(d => d.GetAllAsync()).ReturnsAsync(CachedProducts);

        // Act
        IReadOnlyCollection<Product> result = await _cachedRepository.GetAllAsync();

        // Assert
        Assert.Same(CachedProducts, result);

        _mockDecorated.Verify(d => d.GetAllAsync(), Times.Once);
        _mockCache.Verify(c => c.Set(AllProductsCacheKey, CachedProducts, DefaultExpiration), Times.Once);
    }

    [Fact]
    public void ShouldRemoveCacheWhenNewProductIsAdded()
    {
        // Arrange
        Product product = new(id: 1, name: "Product 1", price: 10m);

        // Act
        _cachedRepository.Add(product);

        // Assert
        _mockDecorated.Verify(d => d.Add(product), Times.Once);
        _mockCache.Verify(c => c.Remove(AllProductsCacheKey), Times.Once);
    }
}