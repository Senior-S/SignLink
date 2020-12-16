using Rocket.API;

namespace SignLink
{
    public class Configuration : IRocketPluginConfiguration
    {
        public void LoadDefaults()
        {
            Enabled = true;
        }

        public bool Enabled;
    }
}
