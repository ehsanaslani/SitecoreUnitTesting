using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Sitecore.DependencyInjection;

namespace TestingTools.Switchers
{
    public class ServiceProviderWrapper : IServiceProvider
    {
        private IServiceProvider _innerProvider;

        private static IServiceProvider _originalProvider = ServiceLocator.ServiceProvider;

        private static ServiceCollection defaultServices = new ServiceCollection().AddBaseServices();

        public ServiceProviderWrapper()
        {
            this._innerProvider = ServiceProviderSwitcher.CurrentValue == null ? ServiceLocator.ServiceProvider : GetServiceProvider(ServiceProviderSwitcher.CurrentValue);
        }

        private IServiceProvider GetServiceProvider(ServiceCollection serviceCollection)
        {
            var services = new ServiceCollection();

            foreach (var descriptor in serviceCollection)
            {
                services.Add(descriptor);
            }

            foreach (var descriptor in defaultServices)
            {
                if (services.All(d => d.ServiceType != descriptor.ServiceType))
                {
                    services.Add(descriptor);
                }
            }

            return services.BuildServiceProvider(false);
        }

        public object GetService(Type serviceType)
        {
            this._innerProvider = ServiceProviderSwitcher.CurrentValue == null ? _originalProvider : GetServiceProvider(ServiceProviderSwitcher.CurrentValue);
            return this._innerProvider.GetService(serviceType);

        }

        public void SetInnerProvider(IServiceProvider provider)
        {
            this._innerProvider = provider;
        }
    }
}
