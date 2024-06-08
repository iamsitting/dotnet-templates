#pragma warning disable
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Moq;
using TemplateProject.Constants;
using TemplateProject.Infrastructure.Caching;

namespace TemplateProject.Tests;

[TestFixture]
public class MemoryCacheServiceTests
{
    private Mock<IMemoryCache> _memoryCacheMock;
    private MemoryCacheService _cacheService;
    private CacheConfigurationOptions _cacheConfig;

    [SetUp]
    public void SetUp()
    {
        _memoryCacheMock = new Mock<IMemoryCache>();
        _cacheConfig = new CacheConfigurationOptions
        {
            AbsoluteExpirationInMinutes = 60,
            SlidingExpirationInMinutes = 30
        };
        var configOptions = Options.Create(_cacheConfig);

        _cacheService = new MemoryCacheService(_memoryCacheMock.Object, configOptions);
    }

    [Test]
    public void TryGet_ShouldRetrieveItemFromCache()
    {
        // Arrange
        var cacheKey = new CacheKey(CacheKeys.Domain.Books, CacheKeys.CacheType.Id);
        var expectedValue = "TestValue";

        object outValue = expectedValue;
        _memoryCacheMock.Setup(x => x.TryGetValue(cacheKey.ToString(), out outValue)).Returns(true);

        // Act
        var result = _cacheService.TryGet<string>(cacheKey, out var actualValue);

        // Assert
        Assert.That(result, Is.True);
        Assert.That(actualValue, Is.EqualTo(expectedValue));
    }

    [Test]
    public void Clear_ShouldRemoveItemFromCache()
    {
        // Arrange
        var cacheKey = new CacheKey(CacheKeys.Domain.Books, CacheKeys.CacheType.First);

        // Act
        _cacheService.Clear(cacheKey);

        // Assert
        _memoryCacheMock.Verify(x => x.Remove(It.Is<string>(k => k == cacheKey.ToString())), Times.Once);
    }
    
}