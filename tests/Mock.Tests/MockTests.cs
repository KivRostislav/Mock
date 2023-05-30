namespace Mock.Tests;

public class MockTests
{
    [Fact]
    public void Should_intercept_method_call_with_It_arguments_type()
    {
        IMock<IInterface> mock = new Mock<IInterface>();

        mock.Setup(x => x.Pong(It.IsAny<string>())).Returns("Ping");

        IInterface @interface = mock.Object;

        Assert.Equal("Ping", @interface.Pong("Ping"));
    }

    [Fact]
    public void Should_intercept_method_call()
    {
        IMock<IInterface> mock = new Mock<IInterface>();

        mock.Setup(x => x.Ping()).Returns("Ping");

        IInterface @interface = mock.Object;

        Assert.Equal("Ping", @interface.Ping());
    }

    [Fact]
    public void Should_intercept_method_call_with_many_returns()
    {
        IMock<IInterface> mock = new Mock<IInterface>();

        mock.Setup(x => x.Ping()).Returns("Ping").Returns("Pong");

        IInterface @interface = mock.Object;

        Assert.Equal("Ping", @interface.Ping());
        Assert.Equal("Pong", @interface.Ping());
    }

    [Fact]
    public void Should_intercept_generic_method_call()
    {
        IMock<IInterface> mock = new Mock<IInterface>();

        mock.Setup(x => x.Pang<string>("Ping")).Returns("Ping");

        IInterface @interface = mock.Object;

        Assert.Equal("Ping", @interface.Pang<string>("Ping"));
        Assert.ThrowsAny<Exception>(() => @interface.Pang<string>(""));
        Assert.ThrowsAny<Exception>(() => @interface.Pang<int>(0));
    }

    [Fact]
    public void Should_throw_exception()
    {
        IMock<IInterface> mock = new Mock<IInterface>();

        mock.Setup(x => x.Ping()).Throws(new InvalidOperationException("Ping"));

        IInterface @interface = mock.Object;

        Assert.ThrowsAny<Exception>(() => @interface.Ping());
    }

    [Fact]
    public void Should_invoke_callback_action()
    {
        IMock<IInterface> mock = new Mock<IInterface>();

        bool isCalled = false;

        mock.Setup(x => x.Ping()).Callback(() => isCalled = true);

        IInterface @interface = mock.Object;
        @interface.Ping();

        Assert.True(isCalled);
    }

    [Fact]
    public void Should_call_base_method()
    {
        IMock<Interface> mock = new Mock<Interface>();

        mock.Setup((Interface x) => x.Ping()).Callbase();

        IInterface @interface = mock.Object;

        Assert.Equal(new Interface().Ping(), @interface.Ping());
    }

    [Fact]
    public void Should_verify_method_call()
    {
        IMock<IInterface> mock = new Mock<IInterface>();

        mock.Setup(x => x.Ping()).Verifiable();

        IInterface @interface = mock.Object;

        Assert.ThrowsAny<Exception>(() => mock.Verify());

        @interface.Ping();

        mock.Verify();
    }

    [Fact]
    public void Should_throw_if_member_is_not_virtual()
    {
        IMock<Interface> mock = new Mock<Interface>();

        Assert.ThrowsAny<Exception>(() => mock.Setup(x => x.Pung()));
    }
}
