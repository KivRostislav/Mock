using System.Linq.Expressions;

namespace Mock;

public interface IMock<TMock> where TMock : class
{
    TMock Object { get; }

    void Verify();

    ISetup<TReturns> Setup<TReturns>(Expression<Func<TMock, TReturns>> member);

    ISetup<TPropertyType> SetupGetter<TPropertyType>(Expression<Func<TMock, TPropertyType>> member);

    ISetupSetter SetupSetter<TPropertyType>(Expression<Func<TMock, TPropertyType>> member);

    IMock<TMock> SetupProperty<TPropertyType>(Expression<Func<TMock, TPropertyType>> member);
}
