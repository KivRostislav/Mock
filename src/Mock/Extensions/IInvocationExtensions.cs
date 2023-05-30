using Castle.DynamicProxy;
using System.Linq.Expressions;
using System.Reflection;

namespace Mock.Extensions;

public static class IInvocationExtensions
{
    public static bool Match(this IInvocation invocation, ISetup setup)
    {
        if (invocation.Method != setup.MemberInfo)
        {
            return false;
        }

        if (invocation.Method.IsSetter() || invocation.Method.IsGetter())
        {
            return true;
        }

        if (invocation.Arguments.Length != setup.Arguments.Length)
        {
            return false;
        }

        for (int i = 0; i != invocation.Arguments.Length; i++)
        {
            if (!MatchArguments(setup.Arguments[i], invocation.Arguments[i]))
            {
                return false;
            }
        }

        return true;
    }

    public static bool MatchProperty(this IInvocation invocation, ISetup setup)
    {
        if (!invocation.Method.IsSetter() && !invocation.Method.IsGetter())
        {
            return false;
        }

        PropertyInfo property = (PropertyInfo)setup.MemberInfo;

        if (invocation.Method != property.GetMethod && invocation.Method != property.SetMethod)
        {
            return false;
        }

        return true;
    }

    private static bool MatchArguments(Expression left, object? right)
    {
        if (left is MethodCallExpression callExpression)
        {
            if (callExpression.Method.DeclaringType == typeof(It))
            {
                return callExpression.Method.ReturnType == right?.GetType();
            }
        }

        return Expression.Lambda<Func<object?>>(left).Compile()() == right;
    }
}
