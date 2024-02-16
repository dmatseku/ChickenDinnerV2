using ChickenDinnerV2.Modules.Volkodav.Model;
using CommandSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChickenDinnerV2.Modules.Volkodav.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    internal class UnbanPlayer : ICommand
    {
        public string Command { get; } = "FreeVolkodav";

        public string[] Aliases { get; } = new string[1] { "freevolkodav" };

        public string Description { get; } = "Unban player from the shelter";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {

            if (arguments.Count == 0)
            {
                response = "Provide player Id";
                return false;
            }
            foreach (var arg in arguments)
            {
                if (!BanManager.Unban(arg))
                {
                    response = "Something went wrong with id " + arg;
                    return false;
                }
            }

            response = "Volkodav has been freed";
            return true;
        }
    }
}
