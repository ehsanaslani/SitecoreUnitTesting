using NSubstitute;
using Sitecore.Collections;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;

namespace TestingTools.Extensions
{
    public static class ItemExtensions
    {
        public  static void AddField(this Item item, ID id, string name, string rawValue)
        {
            var field = Substitute.For<Field>(id, item);
            field.Name.Returns(name);
            field.Value.Returns(rawValue);
            item.Fields[id].Returns(field);
            item.Fields[name].Returns(field);
            item[name].Returns(rawValue);
        }

        public static void AddChildren(this Item item, params Item[] childItems)
        {
            item.Children.Returns(new ChildList(item, childItems));
            item.HasChildren.Returns(true);
            foreach (var childItem in childItems)
            {
                childItem.Paths.Path.Returns("item.Paths.Path/childItem.Name");
                childItem.Parent.Returns(item);
                item.Database.GetItem(childItem.ID).Returns(childItem);
                item.Database.GetItem(childItem.ID).Returns(childItem);
            }
        }
    }
}
