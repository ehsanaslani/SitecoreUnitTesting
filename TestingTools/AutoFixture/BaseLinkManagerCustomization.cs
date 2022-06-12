using AutoFixture;
using NSubstitute;
using Sitecore.Abstractions;

namespace TestingTools.AutoFixture
{
    public class BaseLinkManagerCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize<BaseLinkManager>(x =>
                x.FromFactory(() => Substitute.For<BaseLinkManager>())
                    .OmitAutoProperties());
        }
    }
}

