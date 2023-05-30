namespace Mock.Tests;

public interface IInterface
{
    string Name { get; set; }

    string Ping();

    string Pong(string @object);

    string Pang<T>(T @object);
}

public class Interface : IInterface
{
    public virtual string Name { get; set; } = "";

    public Guid Id { get; set; }

    public string Ping()
    {
        return "Ping";
    }

    public string Pong(string @object)
    {
        return "Pong";
    }

    public string Pang<T>(T @object)
    {
        return "Pang";
    }

    public string Pung()
    {
        return "Pung";
    }
}
