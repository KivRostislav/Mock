namespace Mock.Components;

public interface IComponentResult<TReturns> : IReturns<TReturns>, ICallback<TReturns>, ICallbase<TReturns>, IThrows<TReturns>, IVerify<TReturns> { }

public interface IComponentResult : ICallback, ICallbase, IThrows, IVerify { }
