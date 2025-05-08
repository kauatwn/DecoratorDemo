using Infrastructure.Services;
using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure.Tests.Integration.Services;

public class MemoryCacheServiceIntegrationTests : IDisposable
{
    private readonly MemoryCache _memoryCache;
    private readonly MemoryCacheService _cacheService;

    private static readonly TimeSpan TestDefaultExpiration = TimeSpan.FromMilliseconds(500);
    private static readonly TimeSpan BufferTime = TimeSpan.FromMilliseconds(100);

    public MemoryCacheServiceIntegrationTests()
    {
        _memoryCache = new MemoryCache(new MemoryCacheOptions());
        _cacheService = new MemoryCacheService(_memoryCache, TestDefaultExpiration);
    }

    [Fact]
    public void ShouldStoreValueInCacheWhenSetIsCalled()
    {
        // Arrange
        const string key = "store-test-key";
        const string value = "store-test-value";

        // Act
        _cacheService.Set(key, value);
        string? result = _cacheService.Get<string>(key);

        // Assert
        Assert.Equal(value, result);
    }

    [Fact]
    public void ShouldReturnNullWhenKeyDoesNotExist()
    {
        // Arrange
        const string key = "non-existent-test-key";

        // Act
        string? result = _cacheService.Get<string>(key);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void ShouldRemoveValueFromCacheWhenRemoveIsCalled()
    {
        // Arrange
        const string key = "remove-test-key";
        const string value = "remove-test-value";

        _cacheService.Set(key, value);

        // Act
        _cacheService.Remove(key);
        string? result = _cacheService.Get<string>(key);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task ShouldExpireValueWhenExpirationTimeIsProvided()
    {
        // Arrange
        const string key = "expire-custom-test-key";
        const string value = "expire-custom-test-value";
        TimeSpan expiration = TestDefaultExpiration.Add(TimeSpan.FromMilliseconds(500));

        _cacheService.Set(key, value, expiration);

        // Act
        await Task.Delay(expiration + BufferTime);
        string? result = _cacheService.Get<string>(key);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task ShouldRetainValueWhenTimeIsNotExpired()
    {
        // Arrange
        const string key = "retain-test-key";
        const string value = "retain-test-value";

        _cacheService.Set(key, value);

        // Act
        await Task.Delay(TestDefaultExpiration - BufferTime);
        string? result = _cacheService.Get<string>(key);

        // Assert
        Assert.Equal(value, result);
    }

    [Fact]
    public async Task ShouldExpireValueWhenDefaultCacheTimeIsPassed()
    {
        // Arrange
        const string key = "expire-default-test-key";
        const string value = "expire-default-test-value";

        _cacheService.Set(key, value);

        // Act
        await Task.Delay(TestDefaultExpiration + BufferTime);
        string? result = _cacheService.Get<string>(key);

        // Assert
        Assert.Null(result);
    }

    public void Dispose()
    {
        _memoryCache.Dispose();
        GC.SuppressFinalize(this);
    }
}