using System.Reflection;

namespace Mock.Extensions;

public static class MethodInfoExtensions
{
    public static bool IsGetter(this MethodInfo method)
    {
        return method.Name.StartsWith("get_") && method.GetParameters().Length == 0;
    }

    public static bool IsSetter(this MethodInfo method)
    {
        return method.Name.StartsWith("set_") && method.GetParameters().Length == 1;
    }
}
