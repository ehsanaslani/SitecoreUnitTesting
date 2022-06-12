using System;
using FluentAssertions;
using NSubstitute;
using Sitecore.Abstractions;
using Sitecore.Data.Items;
using TestingTools.AutoFixture;
using Xunit;

namespace Sample.Tests
{
    public class Part3SampleTests
    {
        [Theory, SitecoreAutoData]
        public void GetCurrentPageUrl_Returns_LinkForCurrentPage(Item currentPage, BaseLinkManager linkManager, Uri uri)
        {
            Sitecore.Context.Item = currentPage;
            linkManager.GetItemUrl(currentPage).Returns(uri.AbsoluteUri);

            var sut = new LinkManagerSampleClass(linkManager);

            sut.GetCurrentPageUrl().Should().Be(uri.AbsoluteUri);
        }

        [Theory, SitecoreAutoData]
        public void IsFooEnabled_ReturnTrue_WhenSettingEnabled(BaseSettings settings)
        {
            settings.GetBoolSetting("IsFooEnabled", Arg.Any<bool>()).Returns(true);

            var sut = new ConfigurationSettingsSample(settings);

            sut.IsFooEnabled().Should().BeTrue();
        }
    }
}
