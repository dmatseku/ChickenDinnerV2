using ChickenDinnerV2.Modules.Volkodav.Model;
using CommandSystem;
using Exiled.API.Features;
using System;

namespace ChickenDinnerV2.Modules.Volkodav.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    internal class BanPlayer : ICommand
    {
        public string Command { get; } = "Volkodav";

        public string[] Aliases { get; } = new string[1] { "volkodav" };

        public string Description { get; } = "Ban player into the shelter";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {

            if (arguments.Count == 0)
            {
                response = "Provide player Id";
                return false;
            }
            foreach (var arg in arguments)
            {
                if (!BanManager.Ban(arg))
                {
                    response = "Something went wrong with id " + arg;
                    return false;
                }
            }

            response = "Volkodav has been banned";
            return true;
        }
    }
}
