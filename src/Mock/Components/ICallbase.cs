namespace Mock.Components;

public interface ICallbase
{
    IComponentResult Callbase();
}


public interface ICallbase<TReturns>
{
    IComponentResult<TReturns> Callbase();
}
