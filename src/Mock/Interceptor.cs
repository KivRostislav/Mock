using Castle.DynamicProxy;

namespace Mock;

public class Interceptor : IInterceptor
{
    private readonly ISetup[] _setups;

    public Interceptor(ISetup[] setups)
    {
        _setups = setups;
    }

    public void Intercept(IInvocation invocation)
    {
        ISetup? setup = _setups.Where(x => x.Match(invocation)).FirstOrDefault();

        if (setup == null)
        {
            throw new InvalidOperationException();
        }

        setup.Execute(invocation);
    }
}
