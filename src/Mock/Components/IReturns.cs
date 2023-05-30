namespace Mock.Components;

public interface IReturns<TReturns>
{
    IComponentResult<TReturns> Returns(TReturns returnValue);
}
