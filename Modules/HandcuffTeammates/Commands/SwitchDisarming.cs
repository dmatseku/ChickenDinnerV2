using System;
using ChickenDinnerV2.Modules.HandcuffTeammates.Model;
using CommandSystem;
using Exiled.API.Features;
using PlayerRoles;

namespace ChickenDinnerV2.Modules.HandcuffTeammates.Commands
{
    [CommandHandler(typeof(GameConsoleCommandHandler))]
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    internal class SwitchDisarming : ICommand
    {
        protected static Config HandcuffTeammatesConfig = ChickenDinnerV2.Core.Main.Instance.Config.HandcuffTeammates;

        public string Command { get; } = "SwitchHandcuff";

        public string[] Aliases { get; } = new string[2] { "switchhandcuff", "shc" };

        public string Description { get; } = "Switch handcuffs for the player";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = PlayerFinder.GetTargetPlayer(Player.Get(sender));

            if (player.Role.Team != Team.FoundationForces)
            {
                response = "Your role is unable to bound players";
            }

            if (player == null)
            {
                response = "There is no player in front of you";
                return false;
            }

            if (player.IsCuffed)
            {
                player.RemoveHandcuffs();
                response = "The player " + player.Nickname + " has been released";
            }
            else
            {
                player.Handcuff();
                response = "The player " + player.Nickname + " has been handcuffed";
            }
            return true;
        }
    }
}
