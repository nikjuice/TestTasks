
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MultiSearch.Services;
using NUnit.Framework;
using Moq;
using MultiSearch.Models;

namespace MultiSearchTests;

public class SearchManagerTests
{
    private Mock<ISearchProvider> _testSearchProviderMock = new();
    private readonly Mock<ILogger<SearchManager>> _loggerMock = new();
    private IServiceProvider _serviceProviderMock;

    [SetUp]
    public void Setup()
    {
        _testSearchProviderMock = new Mock<ISearchProvider>();

        var serviceCollection = new ServiceCollection();

        serviceCollection.AddSingleton(_testSearchProviderMock.Object);
        _serviceProviderMock = serviceCollection.BuildServiceProvider();
    }

    [Test]
    public async Task SearchManagerShouldReturnCorrectSum()
    {
        _testSearchProviderMock.Setup(p => p.SearchAsync(It.Is<string>(s => s.Equals("hello"))))
            .ReturnsAsync(Result<int>.OK(10));
        _testSearchProviderMock.Setup(p => p.SearchAsync(It.Is<string>(s => s.Equals("world"))))
            .ReturnsAsync(Result<int>.OK(20));
        
        var searchManager = new SearchManager(_serviceProviderMock, _loggerMock.Object);
        var result = await searchManager.SearchAsync(new List<string> { "hello", "world" });

        Assert.AreEqual(30, (await result.First().HitsCountResult).Data);
    }
    
    [Test]
    public async Task SearchManagerShouldReturnResultErrorForGeneralExceptionInProvider()
    {
        _testSearchProviderMock.Setup(p => p.SearchAsync(It.Is<string>(s => s.Equals("hello"))))
            .ThrowsAsync(new NullReferenceException());
        
        
        var searchManager = new SearchManager(_serviceProviderMock, _loggerMock.Object);
        var result = await searchManager.SearchAsync(new List<string> { "hello"});

        Assert.False((await result.First().HitsCountResult).Success);
        
    }
    
    [Test]
    public async Task SearchManagerShouldReturnResultErrorWhenProviderReturnsError()
    {
        _testSearchProviderMock.Setup(p => p.SearchAsync(It.Is<string>(s => s.Equals("hello"))))
            .ReturnsAsync(Result<int>.Error("fail", -1));
        
        
        var searchManager = new SearchManager(_serviceProviderMock, _loggerMock.Object);
        var result = await searchManager.SearchAsync(new List<string> { "hello"});

        Assert.False((await result.First().HitsCountResult).Success);
        
    }
    
}
