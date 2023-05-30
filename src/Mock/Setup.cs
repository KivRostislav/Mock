using Castle.DynamicProxy;
using Mock.Components;
using Mock.Extensions;
using System.Linq.Expressions;
using System.Reflection;

namespace Mock;

public class SetupProperty : Setup
{
    private object? _value;

    public SetupProperty(MemberInfo memberInfo, Expression[] arguments) : base(memberInfo, arguments)
    {
    }

    public override void Execute(IInvocation invocation)
    {
        if (invocation.Method.IsSetter() && invocation.Arguments.Length == 1)
        {
            _value = invocation.Arguments[0];
        }
        else if (invocation.Method.IsGetter())
        {
            invocation.ReturnValue = _value;
        }
    }

    public override bool Match(IInvocation invocation)
    {
        return invocation.MatchProperty(this);
    }
}

public class SetupSetter : Setup, ISetupSetter
{
    private readonly object? _value;

    public SetupSetter(MemberInfo memberInfo, object? value) : base(memberInfo, Array.Empty<Expression>())
    {
        _value = value;
    }

    public IComponentResult Throws(Exception exception)
    {
        _exception = exception;
        return this;
    }

    public IComponentResult Callback(Action callback)
    {
        _callback = callback;
        return this;
    }

    public IComponentResult Callbase()
    {
        _callbase = true;
        return this;
    }

    public IComponentResult Verifiable()
    {
        IsVerifiable = true;
        return this;
    }
}

public class Setup<TReturns> : Setup, ISetup<TReturns>
{
    public Setup(MemberInfo memberInfo, Expression[] arguments) : base(memberInfo, arguments) { }

    public IComponentResult<TReturns> Returns(TReturns returnValue)
    {
        _returnValues.Add(returnValue);
        return this;
    }

    public IComponentResult<TReturns> Throws(Exception exception)
    {
        _exception = exception;
        return this;
    }

    public IComponentResult<TReturns> Callback(Action callback)
    {
        _callback = callback;
        return this;
    }

    public IComponentResult<TReturns> Callbase()
    {
        _callbase = true;
        return this;
    }

    public IComponentResult<TReturns> Verifiable()
    {
        IsVerifiable = true;
        return this;
    }
}

public abstract class Setup : ISetup
{
    public MemberInfo MemberInfo { get; }

    public Expression[] Arguments { get; }

    public bool IsVerifiable { get; set; }

    public bool IsVerified => _numberOfCalls > 0;

    protected readonly List<object?> _returnValues;

    protected Action _callback;

    protected Exception? _exception;

    protected bool _callbase;

    protected int _numberOfCalls;

    protected Setup(MemberInfo memberInfo, Expression[] arguments)
    {
        MemberInfo = memberInfo;
        Arguments = arguments;
        _callback = () => { };
        _returnValues = new();
    }

    public virtual void Execute(IInvocation invocation)
    {
        _numberOfCalls++;
        _callback.Invoke();

        if (_exception != null)
        {
            throw _exception;
        }

        if (_callbase)
        {
            invocation.Proceed();
            return;
        }

        object? returnValue = null;

        if (_returnValues.Count != 0)
        {
            returnValue = _numberOfCalls > _returnValues.Count ? _returnValues[^1] : _returnValues[_numberOfCalls - 1];
        }

        invocation.ReturnValue = returnValue;
    }

    public virtual bool Match(IInvocation invocation)
    {
        return invocation.Match(this);
    }
}
