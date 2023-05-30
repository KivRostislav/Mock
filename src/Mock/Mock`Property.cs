using System.Linq.Expressions;
using System.Reflection;

namespace Mock;

public partial class Mock<TMock> : IMock<TMock>
{
    public ISetup<TPropertyType> SetupGetter<TPropertyType>(Expression<Func<TMock, TPropertyType>> member)
    {
        PropertyInfo property = GetPropertyInfo(member);

        if (!property.CanRead)
        {
            throw new Exception();
        }

        if (!property.GetMethod!.IsVirtual)
        {
            throw new Exception();
        }

        ISetup<TPropertyType> setup = new Setup<TPropertyType>(property.GetMethod!, Array.Empty<Expression>());

        Setups.Add(setup);

        return setup;
    }

    public ISetupSetter SetupSetter<TPropertyType>(Expression<Func<TMock, TPropertyType>> member)
    {
        PropertyInfo property = GetPropertyInfo(member);

        if (!property.CanWrite)
        {
            throw new Exception();
        }

        if (!property.SetMethod!.IsVirtual)
        {
            throw new Exception();
        }

        ISetupSetter setup = new SetupSetter(property.SetMethod!, Array.Empty<Expression>());

        Setups.Add(setup);

        return setup;
    }

    public IMock<TMock> SetupProperty<TPropertyType>(Expression<Func<TMock, TPropertyType>> member)
    {
        PropertyInfo property = GetPropertyInfo(member);

        if (!property.CanRead || !property.CanWrite)
        {
            throw new Exception();
        }

        if (!property.GetMethod!.IsVirtual || !property.SetMethod!.IsVirtual)
        {
            throw new Exception();
        }

        ISetup setup = new SetupProperty(property, Array.Empty<Expression>());

        Setups.Add(setup);

        return this;
    }


    private static PropertyInfo GetPropertyInfo<TPropertyType>(Expression<Func<TMock, TPropertyType>> member)
    {
        if (member.Body.NodeType != ExpressionType.MemberAccess)
        {
            throw new Exception();
        }

        return (PropertyInfo)((MemberExpression)member.Body).Member;
    }
}
