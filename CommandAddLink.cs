using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using SDG.Unturned;
using System.Collections.Generic;
using UnityEngine;

namespace SignLink
{
    public class CommandAddLink : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "addlink";

        public string Help => "Add a link to a sign.";

        public string Syntax => "<message> <link>";

        public List<string> Aliases => new List<string>();

        public List<string> Permissions => new List<string> { "ss.addlink" };

        public void Execute(IRocketPlayer caller, string[] command)
        {
            UnturnedPlayer user = (UnturnedPlayer)caller;
            SignLink main = SignLink.Instance;

            Transform trans = RaycastHelper.Raycast(user, 8f);
            if (trans == null)
            {
                UnturnedChat.Say(user, main.Translate("no_sign"), true);
                return;
            }
            InteractableSign sign = trans.GetComponent<InteractableSign>();
            if (sign == null)
            {
                UnturnedChat.Say(user, main.Translate("no_sign"), true);
                return;
            }
            if (command.Length != 2)
            {
                UnturnedChat.Say(user, main.Translate("error_usage"), true);
                return;
            }

            string msg = command[0];
            string link = command[1];

            if (Utils.GetSignLink(sign.GetInstanceID()) != null)
            {
                UnturnedChat.Say(user, main.Translate("sign_already_exist"), true);
                return;
            }

            Utils.AddSignLink(sign.GetInstanceID(), msg, link);
            UnturnedChat.Say(user, main.Translate("sign_added"), true);
        }
    }
}
