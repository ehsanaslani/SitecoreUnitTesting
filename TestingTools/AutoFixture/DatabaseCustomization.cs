using AutoFixture;
using NSubstitute;
using Sitecore.Data;

namespace TestingTools.AutoFixture
{
    public class DatabaseCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize<Database>(x =>
                x.FromFactory(CreateDatabase)
                    .OmitAutoProperties());
        }

        private static Database CreateDatabase()
        {
            var db = Substitute.For<Database>();
            db.Items.Returns(Substitute.For<ItemRecords>(db));
            return db;
        }
    }
}

