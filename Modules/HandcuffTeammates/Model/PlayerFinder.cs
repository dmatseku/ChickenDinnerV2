using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ChickenDinnerV2.Modules.HandcuffTeammates.Model
{
    internal static class PlayerFinder
    {
        private static float getProduct(Player sender, Player target)
        {
            Log.Warn("Target: " + target.Nickname);
            Log.Warn("Position: [" + target.Transform.position.x + ", " + target.Transform.position.y + ", " + target.Transform.position.z + "]");
            Vector3 targetDirecton = (target.Transform.position - sender.Transform.position).normalized;
            Log.Warn("Relative direction to target: [" + targetDirecton.x + ", " + targetDirecton.y + ", " + targetDirecton.z + "]");

            return Vector3.Dot(sender.Transform.forward, targetDirecton);
        }

        private static float getDistance(Player sender, Player target)
        {
            return (sender.Position - target.Position).sqrMagnitude;
        }

        public static Player GetTargetPlayer(Player sender)
        {
            foreach (Player player in Player.List)
            {
                if (player == sender) continue;

                float distance = getDistance(sender, player);

                if (distance < 20)
                {
                    float range = distance / 20;
                    float product = getProduct(sender, player);

                    if (product > 0.866 + 0.084 * range)
                    {
                        return player;
                    }

                }
            }
            return null;
        }
    }
}
