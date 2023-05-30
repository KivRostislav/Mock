using Castle.DynamicProxy;
using Mock.Components;
using System.Linq.Expressions;
using System.Reflection;

namespace Mock;

public interface ISetupSetter : ISetup, IThrows, ICallback, ICallbase, IVerify, IComponentResult { }

public interface ISetup<TReturns> : ISetup, IReturns<TReturns>, IThrows<TReturns>, ICallback<TReturns>, ICallbase<TReturns>, IVerify<TReturns>, IComponentResult<TReturns> { }

public interface ISetup
{
    MemberInfo MemberInfo { get; }

    Expression[] Arguments { get; }

    bool IsVerifiable { get; set; }

    bool IsVerified { get; }

    void Execute(IInvocation invocation);

    bool Match(IInvocation invocation);
}
