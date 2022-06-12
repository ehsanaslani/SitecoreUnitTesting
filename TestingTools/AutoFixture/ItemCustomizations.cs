using AutoFixture;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Globalization;
using TestingTools.Extensions;

namespace TestingTools.AutoFixture
{
    public class ItemCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize<Item>(x =>
                x.FromFactory(() => CreateItem(fixture))
                    .OmitAutoProperties()
            );
        }

        private static Item CreateItem(IFixture fixture)
        {
            var database = fixture.Create<Database>();
            var itemId = fixture.Create<ID>();
            var templateId = fixture.Create<ID>();

            return database.AddItem(itemId, templateId, Language.Parse("en"), Version.First, 
                fixture.Create<string>(),"sitecore/content");
        }
    }
}
