namespace Mock.Tests;

public class MockPropertyTests
{
    [Fact]
    public void Should_intercept_property_getter()
    {
        IMock<IInterface> mock = new Mock<IInterface>();

        mock.SetupGetter(x => x.Name).Returns("Ping");

        IInterface @interface = mock.Object;

        Assert.Equal("Ping", @interface.Name);
    }

    [Fact]
    public void Should_intercept_property_setter()
    {
        IMock<Interface> mock = new Mock<Interface>();

        bool isCalled = false;

        mock.SetupSetter(x  => x.Name).Callback(() => isCalled = true);
        
        Interface @interface = mock.Object;

        @interface.Name = "Ping";

        Assert.True(isCalled);
    }

    [Fact]
    public void Should_intercept_property_getter_and_setter()
    {
        IMock<Interface> mock = new Mock<Interface>();

        mock.SetupProperty(x => x.Name);

        Interface @interface = mock.Object;

        @interface.Name = "Ping";

        Assert.Equal("Ping", @interface.Name);
    }

    [Fact]
    public void Should_throw_if_property_is_not_virtual()
    {
        IMock<Interface> mock = new Mock<Interface>();

        Assert.ThrowsAny<Exception>(() => mock.SetupProperty(x => x.Id));
        Assert.ThrowsAny<Exception>(() => mock.SetupSetter(x => x.Id));
        Assert.ThrowsAny<Exception>(() => mock.SetupGetter(x => x.Id));
    }
}
