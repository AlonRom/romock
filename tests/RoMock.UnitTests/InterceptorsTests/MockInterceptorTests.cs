using System.Reflection;
using Moq;
using RoMock.Library.Interceptors;
using RoMock.Library.Services;
using IInvocation = Castle.DynamicProxy.IInvocation;

namespace RoMock.UnitTests.InterceptorsTests;

public class MockInterceptorTests
{
    [Fact]
    public void Intercept_WhenMethodIsNotAsync_ExpectInvalidOperationException()
    {
        // Arrange
        var roMockExecutorMock = new Mock<IRoMockExecutor>();
        var invocationMock = new Mock<IInvocation>();
        var interceptor = new MockInterceptor(roMockExecutorMock.Object);

        var methodMock = new Mock<MethodInfo>();
        methodMock.Setup(m => m.ReturnType).Returns(typeof(void));

        invocationMock.Setup(i => i.Method).Returns(methodMock.Object);

        // Act & Assert
        var exception = Assert.Throws<InvalidOperationException>(() => interceptor.Intercept(invocationMock.Object));

        // Assert
        Assert.Equal("Only asynchronous methods are supported.", exception.Message);
    }

    [Fact]
    public void Intercept_WhenAsyncMethodIsNotMocked_ExpectProceedToBeCalled()
    {
        // Arrange
        var roMockExecutorMock = new Mock<IRoMockExecutor>();
        var invocationMock = new Mock<IInvocation>();
        var interceptor = new MockInterceptor(roMockExecutorMock.Object);

        var methodName = "AsyncMethod";
        var invocationArgs = new object[] { 1, "arg" };

        var methodMock = new Mock<MethodInfo>();
        methodMock.Setup(m => m.Name).Returns(methodName);
        methodMock.Setup(m => m.ReturnType).Returns(typeof(Task<string>));

        invocationMock.Setup(i => i.Method).Returns(methodMock.Object);
        invocationMock.Setup(i => i.Arguments).Returns(invocationArgs);

        roMockExecutorMock.Setup(r => r.IsMockMethodRegistered(methodName)).Returns(false);
        invocationMock.Setup(i => i.Proceed());

        // Act
        interceptor.Intercept(invocationMock.Object);

        // Assert
        invocationMock.Verify(i => i.Proceed(), Times.Once);
    }
}
