using System.Reflection;
using RoMock.Library.Extensions;
using RoMock.Library.Interfaces;

namespace RoMock.UnitTests.ExtensionsTests;


public class AssemblyExtensionsTests
{
    private readonly Assembly[] _assemblies;

    public AssemblyExtensionsTests()
    {
        _assemblies = [typeof(IMockable).Assembly, typeof(MockImplementation).Assembly];
    }

    [Fact]
    public void When_FindImplementationType_WithExistingImplementation_Expect_ReturnImplementationType()
    {
        // Arrange
        var interfaceType = typeof(IMockable);

        // Act
        var implementationType = _assemblies.FindImplementationType(interfaceType);

        // Assert
        Assert.NotNull(implementationType);
        Assert.Equal(typeof(MockImplementation), implementationType);
    }

    [Fact]
    public void When_FindImplementationType_WithNoImplementation_Expect_ReturnNull()
    {
        // Arrange
        var interfaceType = typeof(IEnumerable<>); // An interface with no implementation

        // Act
        var implementationType = _assemblies.FindImplementationType(interfaceType);

        // Assert
        Assert.Null(implementationType);
    }

    [Fact]
    public void When_FindMockableInterfaces_WithNoInterfaces_Expect_ReturnEmpty()
    {
        // Arrange
        var targetType = typeof(IEnumerable<>); // An interface not implemented

        // Act
        var interfaces = _assemblies.FindMockableInterfaces(targetType);

        // Assert
        Assert.Empty(interfaces);
    }

    [Fact]
    public void When_FindMethodsWithMockableAttribute_WithMockableMethods_Expect_ReturnMethods()
    {
        // Arrange
        var targetType = typeof(IMockable);

        // Act
        var methods = _assemblies.FindMethodsWithMockableAttribute(targetType);

        // Assert
        var methodInfos = methods.ToList();
        Assert.Equal(2, methodInfos.Count());
        Assert.Contains(methodInfos, m => m.Name == "Foo");
        Assert.Contains(methodInfos, m => m.Name == "Bar");
    }

    [Fact]
    public void When_FindMethodsWithMockableAttribute_WithNoMockableMethods_Expect_ReturnEmpty()
    {
        // Arrange
        var targetType = typeof(IEnumerable<>); // An interface not implemented

        // Act
        var methods = _assemblies.FindMethodsWithMockableAttribute(targetType);

        // Assert
        Assert.Empty(methods);
    }

    [Fact]
    public void When_FindMethodsWithMockableAttribute_WithReflectionException_Expect_HandleGracefully()
    {
        // Arrange
        var invalidAssemblies = new[] { Assembly.Load("mscorlib") }; // Load an assembly that might throw reflection exceptions
        var targetType = typeof(IMockable);

        // Act
        var methods = invalidAssemblies.FindMethodsWithMockableAttribute(targetType);

        // Assert
        Assert.Empty(methods); // Should not throw any exception and return an empty collection
    }
}
