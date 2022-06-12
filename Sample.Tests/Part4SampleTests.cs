using System;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Sitecore.Abstractions;
using Sitecore.Data.Items;
using TestingTools.AutoFixture;
using TestingTools.Switchers;
using Xunit;

namespace Sample.Tests
{
    public class Part4SampleTests : BaseServiceLocatorTestClass
    {
        [Theory, SitecoreAutoData]
        public void GetUrl_Returns_ItemUrl(BaseLinkManager linkManager, Item item, Uri itemUrl)
        {
            linkManager.GetItemUrl(item).Returns(itemUrl.AbsoluteUri);
            //Set up ServiceProvider
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<BaseLinkManager>(provider => linkManager);

            using (new ServiceProviderSwitcher(serviceCollection))
            {
                item.GetUrl().Should().Be(itemUrl.AbsoluteUri);
            }
        }
    }
}
