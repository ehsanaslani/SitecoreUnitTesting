using FluentAssertions;
using NSubstitute;
using Sitecore.Collections;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Xunit;

namespace Sample.Tests
{
    public class Part1SamplesTests
    {
        [Fact]
        public void HasPersonChildrenAbove18_ShouldNotThrow_WhenAgeIsInvalid()
        {
            var database = Substitute.For<Database>();
            var personData = GetMockItem(ID.NewID, database);
            var child = GetMockItem(ID.NewID, database);
            AddField(child,ID.NewID,"Age", "Invalid Integer");
            personData.Children.Returns(new ChildList(personData, new Item[] {child}));

            var sut = new SampleClass();

            sut.Invoking(s => s.HasPersonChildrenAbove18(personData)).Should().NotThrow();
        }

        [Theory]
        [InlineData("default category value")]
        public void GetCurrentPageCategory_ReturnsDefaultValue_WhenFieldNotSet(string defaultCategoryValue)
        {
            var database = Substitute.For<Database>();
            var currentPage = GetMockItem(ID.NewID, database);
            var defaultValueItem = GetMockItem(ID.NewID, database);
            AddField(defaultValueItem, ID.NewID, "Default Category Value", defaultCategoryValue);
            AddField(currentPage, ID.NewID, "Page Category", string.Empty);
            database.GetItem("path/to/default-page-title/item").Returns(defaultValueItem);
            Sitecore.Context.Item = currentPage;
            Sitecore.Context.Database = database;

            var sut = new SampleClass();

            sut.GetCurrentPageCategory().Should().Be(defaultCategoryValue);
        }

        private static Item GetMockItem(ID itemId , Database db)
        {
            var item = Substitute.For<Item>(itemId, ItemData.Empty, db);
            item.Fields.Returns(Substitute.For<FieldCollection>(item));
            return item;
        }

        private static void AddField(Item item, ID id, string name, string rawValue)
        {
            var field = Substitute.For<Field>(id, item);
            field.Name.Returns(name);
            field.Value.Returns(rawValue);
            item.Fields[id].Returns(field);
            item.Fields[name].Returns(field);
            item[name].Returns(rawValue);
        }
    }
}
