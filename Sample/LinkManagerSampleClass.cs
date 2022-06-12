using Sitecore.Abstractions;

namespace Sample
{
    public class LinkManagerSampleClass
    {
        public LinkManagerSampleClass(BaseLinkManager linkManager)
        {
            this.LinkManager = linkManager;
        }

        private BaseLinkManager LinkManager { get; }

        public string GetCurrentPageUrl()
        {
            return this.LinkManager.GetItemUrl(Sitecore.Context.Item);
        }
    }
}
