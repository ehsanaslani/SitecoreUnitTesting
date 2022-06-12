using FluentAssertions;
using Sitecore.Data;
using Sitecore.Data.Items;
using TestingTools.AutoFixture;
using TestingTools.Extensions;
using Xunit;

namespace Sample.Tests
{
    public class Part2SampleTests
    {
        [Theory, SitecoreAutoData]
        public void HasPersonChildrenAbove18_ShouldNotThrow_WhenAgeIsInvalid(Item personData, Item child)
        {
            child.AddField(ID.NewID, "Age", "Invalid Integer");
            personData.AddChildren(child);

            var sut = new SampleClass();

            sut.Invoking(s => s.HasPersonChildrenAbove18(personData)).Should().NotThrow();
        }

    }
}
