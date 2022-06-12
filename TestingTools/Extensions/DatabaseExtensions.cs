using NSubstitute;
using NSubstitute.ClearExtensions;
using Sitecore.Collections;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Globalization;
using Sitecore.Security.AccessControl;


namespace TestingTools.Extensions
{
    public static class DatabaseExtensions
    {
        public static Item AddItem(this Database db, ID itemId, ID templateId, Language language, Version version, string name = "", string path = "sitecore/content")
        {
            var item = Substitute.For<Item>(itemId, ItemData.Empty, db);
            var templateItem = Substitute.For<Item>(templateId, ItemData.Empty, db);
            item.Name.Returns(name);
            item.Versions.Returns(Substitute.For<ItemVersions>(item));
            item.Paths.Returns(Substitute.For<ItemPath>(item));
            item.Paths.Path.Returns($"{path}/{name}");
            item.Template.Returns(Substitute.For<TemplateItem>(templateItem));
            item.Template.ID.Returns(templateId);
            item.Fields.Returns(Substitute.For<FieldCollection>(item));
            item.Statistics.Returns(Substitute.For<ItemStatistics>(item));
            item.Editing.Returns(Substitute.For<ItemEditing>(item));
            item.Access.Returns(Substitute.For<ItemAccess>(item));
            db.GetItem(item.ID).Returns(item);
            db.GetItem(item.Paths.Path).Returns(item);
            item.Axes.Returns(Substitute.For<ItemAxes>(item));


            return item;
        }

        public static Item AddItem(this Database db, ID itemId, ID templateId, Language language)
        {
            return AddItem(db, itemId, templateId, language, Version.First, itemId.ToString(),
                "sitecore/content");
        }

        public static Item AddItem(this Database db, ID itemId, ID templateId)
        {
            return AddItem(db, itemId, templateId, Language.Parse("en"), Version.First );
        }

        public static Item AddItem(this Database db, ID itemId)
        {
            return AddItem(db, itemId, ID.NewID);
        }

        public static void AddItem(this Database db,Item item)
        {
            item.Database.ClearSubstitute();
            item.Database.Returns(db);
            db.GetItem(item.ID).Returns(item);
            db.GetItem(item.Paths.Path).Returns(item);
        }
    }
}
