using Sitecore.DependencyInjection;

namespace TestingTools.Switchers
{
    public class BaseServiceLocatorTestClass
    {
        static BaseServiceLocatorTestClass()
        {
            ServiceLocator.SetServiceProvider(new ServiceProviderWrapper());
        }
    }
}
