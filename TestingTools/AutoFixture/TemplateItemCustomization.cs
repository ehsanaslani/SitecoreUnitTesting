using AutoFixture;
using AutoFixture.Kernel;
using NSubstitute;
using Sitecore.Data.Items;

namespace TestingTools.AutoFixture
{
    public class TemplateItemCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize<TemplateItem>(x =>
                x.FromFactory(() => CreateTemplateItem(fixture))
                    .OmitAutoProperties()
            );
        }

        private static TemplateItem CreateTemplateItem(ISpecimenBuilder fixture)
        {
            var innerItem = fixture.Create<Item>();
            var template = Substitute.For<TemplateItem>(innerItem);
            template.Database.Returns(innerItem.Database);
            innerItem.Database.GetItem(template.ID).Returns(template.InnerItem);
            template.ID.Returns(innerItem.ID);
            return template;
        }
    }
}