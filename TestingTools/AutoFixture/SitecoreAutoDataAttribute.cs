using AutoFixture.Xunit2;

namespace TestingTools.AutoFixture
{
    public class SitecoreAutoDataAttribute : AutoDataAttribute
    {
        public SitecoreAutoDataAttribute()
            : base(() => new global::AutoFixture.Fixture()
                .Customize(new DatabaseCustomization())
                .Customize(new ItemCustomization())
                .Customize(new TemplateItemCustomization())
                .Customize(new BaseLinkManagerCustomization())
                .Customize(new BaseSettingsCustomization()))
        { }

    }
}
