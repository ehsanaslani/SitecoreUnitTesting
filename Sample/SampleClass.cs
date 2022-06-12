using System.Linq;
using Sitecore;
using Sitecore.Data.Items;

namespace Sample
{
    public class SampleClass
    {
        /// <summary>
        /// Gets the current page category or the default page category value if not set.
        /// </summary>
        /// <returns>current or default page category</returns>
        public string GetCurrentPageCategory()
        {
            var currentPageTitle = Context.Item["Page Category"];
            if (string.IsNullOrEmpty(currentPageTitle))
            {
                currentPageTitle = Context.Database.GetItem("path/to/default-page-title/item")["Default Category Value"];
            }

            return currentPageTitle;
        }

        public bool HasPersonChildrenAbove18(Item personData)
        {
            var children = personData.Children;
            return children.Any(child => int.Parse(child["Age"]) > 18);
        }
    }
}
