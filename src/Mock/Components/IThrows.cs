namespace Mock.Components;

public interface IThrows
{
    IComponentResult Throws(Exception exception);
}

public interface IThrows<TReturns>
{
    IComponentResult<TReturns> Throws(Exception exception);
}
