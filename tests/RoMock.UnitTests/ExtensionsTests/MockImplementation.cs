using RoMock.Library.Attributes;
using RoMock.Library.Interfaces;

namespace RoMock.UnitTests.ExtensionsTests;

public class MockImplementation : IMockable
{
    [MockableMethod]
    public void Foo()
    {
    }

    [MockableMethod]
    public void Bar()
    {
    }

    public void NonMockableMethod()
    {
    }
}
