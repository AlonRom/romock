using System.Net;
using System.Reflection;
using System.Text.Json;
using Moq;
using Moq.Protected;
using RoMock.Library.Services;

namespace RoMock.UnitTests.ServicesTests;
public class RoMockServiceTests
{
    [Fact]
    public void When_RegisterMockMethodCalled_Expect_MethodToBeRegisteredInRoMockExecutor()
    {
        // Arrange
        var mockHttpClient = CreateMockHttpClient();
        var roMockExecutorMock = new Mock<IRoMockExecutor>();
        var roMockService = new RoMockService(mockHttpClient, roMockExecutorMock.Object);
        var methodName = "TestMethod";
        var methodMock = new Mock<MethodInfo>();

        // Act
        roMockService.RegisterMockMethod(methodName, methodMock.Object);

        // Assert
        roMockExecutorMock.Verify(x => x.RegisterMockMethod(methodName, It.IsAny<Func<object[], Task<object>>>()), Times.Once);
    }

    [Fact]
    public async Task When_ExecutingRegisteredMethod_Expect_HttpCallToBeMadeAndResultReturned()
    {
        // Arrange
        var methodName = "TestMethod";
        var expectedReturn = "Test Result";
        var jsonExpectedReturn = JsonSerializer.Serialize(expectedReturn);
        var mockHttpClient = CreateMockHttpClient(HttpStatusCode.OK, jsonExpectedReturn);
        var roMockExecutorMock = new Mock<IRoMockExecutor>();
        var roMockService = new RoMockService(mockHttpClient, roMockExecutorMock.Object);
        var methodMock = new Mock<MethodInfo>();
        methodMock.Setup(m => m.ReturnType).Returns(typeof(Task<string>));

        roMockService.RegisterMockMethod(methodName, methodMock.Object);
        roMockExecutorMock
            .Setup(x => x.ExecuteAsync<string>(methodName, It.IsAny<object[]>()))
            .Returns(Task.FromResult(expectedReturn));

        // Act
        var result = await roMockService.ExecuteMockMethod(methodName, methodMock.Object);

        // Assert
        Assert.Equal(expectedReturn, result);
    }

    [Fact]
    public async Task When_HttpCallFails_Expect_ExceptionToBeLoggedAndNullReturned()
    {
        // Arrange
        var methodName = "TestMethod";
        var mockHttpClient = CreateMockHttpClient(HttpStatusCode.InternalServerError, string.Empty);
        var roMockExecutorMock = new Mock<IRoMockExecutor>();
        var roMockService = new RoMockService(mockHttpClient, roMockExecutorMock.Object);
        var methodMock = new Mock<MethodInfo>();
        methodMock.Setup(m => m.ReturnType).Returns(typeof(Task<string>));

        roMockService.RegisterMockMethod(methodName, methodMock.Object);

        // Act
        var result = await roMockService.ExecuteMockMethod(methodName, methodMock.Object);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task When_DeserializingResponse_Expect_CorrectObjectToBeReturned()
    {
        // Arrange
        var expectedReturn = "Test Result";
        var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(JsonSerializer.Serialize(expectedReturn))
        };

        var roMockService = CreateRoMockServiceWithMockHttpClient(responseMessage);
        var result = await roMockService.DeserializeResponse(responseMessage, typeof(string));

        // Assert
        Assert.Equal(expectedReturn, result);
    }

    [Fact]
    public void When_ClearMockMethodsIsCalled_Expect_AllMethodsToBeCleared()
    {
        // Arrange
        var roMockExecutorMock = new Mock<IRoMockExecutor>();

        // Act
        roMockExecutorMock.Object.ClearMockMethods();

        // Assert
        roMockExecutorMock.Verify(x => x.ClearMockMethods(), Times.Once);
    }

    private HttpClient CreateMockHttpClient(HttpStatusCode statusCode = HttpStatusCode.OK, string content = "", string baseAddress = "https://test.com/")
    {
        var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = statusCode,
                Content = new StringContent(content),
            })
            .Verifiable();

        var client = new HttpClient(handlerMock.Object) { BaseAddress = new Uri(baseAddress) };
        return client;
    }

    private RoMockService CreateRoMockServiceWithMockHttpClient(HttpResponseMessage responseMessage)
    {
        var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(responseMessage)
            .Verifiable();

        var httpClient = new HttpClient(handlerMock.Object);
        var roMockExecutorMock = new Mock<IRoMockExecutor>();

        return new RoMockService(httpClient, roMockExecutorMock.Object);
    }
}
