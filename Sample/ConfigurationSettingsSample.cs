using Sitecore.Abstractions;

namespace Sample
{
    public class ConfigurationSettingsSample
    {
        public ConfigurationSettingsSample(BaseSettings settings)
        {
            this.Settings = settings;
        }

        private BaseSettings Settings { get; }

        public bool IsFooEnabled()
        {
            return this.Settings.GetBoolSetting("IsFooEnabled", false);
        }
    }
}
