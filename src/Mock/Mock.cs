using Castle.DynamicProxy;
using System.Linq.Expressions;

namespace Mock;

public partial class Mock<TMock> : IMock<TMock> where TMock : class
{
    public List<ISetup> Setups { get; }

    public TMock Object => GetObject();

    private readonly ProxyGenerator _generator;

    public Mock()
    {
        Setups = new();
        _generator = new ProxyGenerator();
    }

    public ISetup<TReturns> Setup<TReturns>(Expression<Func<TMock, TReturns>> member)
    {
        if (member.Body.NodeType != ExpressionType.Call)
        {
            throw new Exception();
        }

        var callExpression = (MethodCallExpression)member.Body;

        if (!callExpression.Method.IsVirtual)
        {
            throw new Exception();
        }

        ISetup<TReturns> setup = new Setup<TReturns>(callExpression.Method, callExpression.Arguments.ToArray());

        Setups.Add(setup);

        return setup;
    }

    public void Verify()
    {
        if (!Setups.Where(x => x.IsVerifiable).All(x => x.IsVerified))
        {
            throw new InvalidOperationException();
        }
    }

    private TMock GetObject()
    {
        if (typeof(TMock).IsInterface)
        {
            return (TMock)_generator.CreateInterfaceProxyWithoutTarget(typeof(TMock), new Interceptor(Setups.ToArray()));
        }

        return (TMock)_generator.CreateClassProxy(typeof(TMock), new Interceptor(Setups.ToArray()));
    }
}
