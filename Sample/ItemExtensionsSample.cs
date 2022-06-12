using Sitecore.Data.Items;

namespace Sample
{
    public static  class ItemExtensionsSample
    {
        public static string GetUrl(this Item item)
        {
            return Sitecore.Links.LinkManager.GetItemUrl(item);
        }
    }
}
