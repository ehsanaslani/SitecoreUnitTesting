using AutoFixture;
using NSubstitute;
using Sitecore.Abstractions;

namespace TestingTools.AutoFixture
{
    public class BaseSettingsCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize<BaseSettings>(x =>
                x.FromFactory(() => Substitute.For<BaseSettings>())
                    .OmitAutoProperties());
        }
    }
}
