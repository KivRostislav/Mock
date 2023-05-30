namespace Mock.Components;

public interface IVerify
{
    IComponentResult Verifiable();
}


public interface IVerify<TReturns>
{
    IComponentResult<TReturns> Verifiable();
}
