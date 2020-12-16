using Rocket.Core.Plugins;
using Rocket.Unturned.Events;
using Rocket.Unturned.Player;
using SDG.Unturned;
using System.IO;
using UnityEngine;
using Logger = Rocket.Core.Logging.Logger;
using Rocket.API.Collections;

namespace SignLink
{
    public class SignLink : RocketPlugin<Configuration>
    {
        internal static SignLink Instance;

        protected override void Load()
        {
            Logger.Log(" Plugin loaded correctly!");
            Logger.Log(" More plugins: www.dvtserver.xyz");
            if (!Configuration.Instance.Enabled)
            {
                Logger.Log(" Plugin disabled! Please enable it in the config.");
                this.Unload();
                return;
            }

            if (!File.Exists(Utils.path))
            {
                Utils.CreateInitialFile();
            }

            Instance = this;
            UnturnedPlayerEvents.OnPlayerUpdateGesture += OnGesture;
        }

        private void OnGesture(UnturnedPlayer user, UnturnedPlayerEvents.PlayerGesture gesture)
        {
            if (gesture == UnturnedPlayerEvents.PlayerGesture.PunchLeft || gesture == UnturnedPlayerEvents.PlayerGesture.PunchRight)
            {
                Transform trans = RaycastHelper.Raycast(user, 5f);
                if (trans == null)
                {
                    return;
                }
                InteractableSign sign = trans.GetComponent<InteractableSign>();
                if (sign == null) return;
                string[] info = Utils.GetSignLink(sign.GetInstanceID());
                if (info == null) return;
                user.Player.sendBrowserRequest(info[0], info[1]);
            }
        }

        public override TranslationList DefaultTranslations
        {
            get
            {
                return new TranslationList()
                {
                    { "no_sign", "You need to see a sign to use this command." },
                    { "error_usage", "Error! Correct usage: /addlink {message} {link}" },
                    { "sign_already_exist", "This sign already have a link!" },
                    { "sign_added", "Link added successfully" }
                };
            }
        }

        protected override void Unload()
        {
            UnturnedPlayerEvents.OnPlayerUpdateGesture -= OnGesture;
            Logger.Log(" Plugin unloaded correctly!");
        }
    }
}