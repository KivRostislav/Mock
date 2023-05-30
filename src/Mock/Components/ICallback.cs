namespace Mock.Components;

public interface ICallback
{
    IComponentResult Callback(Action callback);
}


public interface ICallback<TReturns>
{
    IComponentResult<TReturns> Callback(Action callback);
}
