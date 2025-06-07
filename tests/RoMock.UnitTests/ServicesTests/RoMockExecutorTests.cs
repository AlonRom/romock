using RoMock.Library.Services;

namespace RoMock.UnitTests.ServicesTests;
public class RoMockExecutorTests
{
    [Fact]
    public void When_MethodNameIsNull_Expect_NoMethodToBeRegistered()
    {
        // Arrange
        var executor = new RoMockExecutor();

        // Act
        executor.RegisterMockMethod(null, async _ => await Task.FromResult<object>("null"));

        // Assert
        Assert.False(executor.IsMockMethodRegistered("null"));
    }

    [Fact]
    public void When_MethodNameIsNotNull_Expect_MethodToBeRegistered()
    {
        // Arrange
        var executor = new RoMockExecutor();
        var methodName = "TestMethod";

        // Act
        executor.RegisterMockMethod(methodName, async _ => await Task.FromResult<object>("Test Result"));

        // Assert
        Assert.True(executor.IsMockMethodRegistered(methodName));
    }

    [Fact]
    public void When_MethodIsNotRegistered_Expect_IsMockMethodRegisteredToReturnFalse()
    {
        // Arrange
        var executor = new RoMockExecutor();
        var methodName = "NonExistentMethod";

        // Act
        var result = executor.IsMockMethodRegistered(methodName);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void When_MethodIsRegistered_Expect_IsMockMethodRegisteredToReturnTrue()
    {
        // Arrange
        var executor = new RoMockExecutor();
        var methodName = "TestMethod";
        executor.RegisterMockMethod(methodName, async _ => await Task.FromResult<object>("Test Result"));

        // Act
        var result = executor.IsMockMethodRegistered(methodName);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task When_MethodIsRegistered_Expect_ExecuteMockMethodToReturnExpectedResult()
    {
        // Arrange
        var executor = new RoMockExecutor();
        var methodName = "TestMethod";
        executor.RegisterMockMethod(methodName, async _ => await Task.FromResult<object>("Executed Result"));

        // Act
        var result = await executor.ExecuteMockMethod(methodName, []);

        // Assert
        Assert.Equal("Executed Result", result);
    }

    [Fact]
    public async Task When_MethodIsNotRegistered_Expect_ExecuteMockMethodToThrowInvalidOperationException()
    {
        // Arrange
        var executor = new RoMockExecutor();
        var methodName = "NonExistentMethod";

        // Act & Assert
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => executor.ExecuteMockMethod(methodName,
            []));
        Assert.Equal($"No mock method registered for {methodName}", exception.Message);
    }

    [Fact]
    public async Task When_MethodIsRegistered_Expect_ExecuteAsyncToReturnResultCastToGivenType()
    {
        // Arrange
        var executor = new RoMockExecutor();
        var methodName = "TestMethod";
        executor.RegisterMockMethod(methodName, async _ => await Task.FromResult<object>("Test Result"));

        // Act
        var result = await executor.ExecuteAsync<string>(methodName, []);

        // Assert
        Assert.Equal("Test Result", result);
    }

    [Fact]
    public void When_ClearMockMethodsIsCalled_Expect_AllMethodsToBeCleared()
    {
        // Arrange
        var executor = new RoMockExecutor();
        var methodName = "TestMethod";
        executor.RegisterMockMethod(methodName, async _ => await Task.FromResult<object>("Test Result"));

        // Act
        executor.ClearMockMethods();
        var result = executor.IsMockMethodRegistered(methodName);

        // Assert
        Assert.False(result);
    }
}
