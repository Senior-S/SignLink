using SDG.Unturned;
using UnityEngine;
using SDG.Framework.Landscapes;
using System.Linq;
using Rocket.Unturned.Player;
using Rocket.API;

namespace SignLink
{
    public class RaycastHelper
    {
        public static Transform Raycast(IRocketPlayer rocketPlayer, float distance)
        {
            UnturnedPlayer player = (UnturnedPlayer)rocketPlayer;
            if (Physics.Raycast(player.Player.look.aim.position, player.Player.look.aim.forward, out RaycastHit hit, distance, RayMasks.BARRICADE_INTERACT | RayMasks.BARRICADE))
            {
                Transform transform = hit.transform;


                return transform;
            }
            return null;
        }
    }
}
