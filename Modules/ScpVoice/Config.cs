using ChickenDinnerV2.Core.Interfaces;
using PlayerRoles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChickenDinnerV2.Modules.ScpVoice
{
    public class Config : IModuleConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;

        public float Distance { get; set; } = 7f;

        public HashSet<RoleTypeId> AllowedRoles { get; set; } = new HashSet<RoleTypeId>
        {
            RoleTypeId.Scp049,
            RoleTypeId.Scp096,
            RoleTypeId.Scp106,
            RoleTypeId.Scp173,
            RoleTypeId.Scp0492,
            RoleTypeId.Scp939
        };

        public bool SendBroadcastOnRoleChange { get; set; } = false;

        public int BroadcastDuration { get; set; } = 5;

        public Dictionary<string, string> Messages { get; set; } = new Dictionary<string, string>
        {
            { "on_role_changed", "<b>Proximity Chat can be toggled with the <color=#42f57b>[ALT]</color> key</b>." },
            { "proximity_enabled", "<i><b>proximity chat <color=#42f57b>enabled</color></b></i>" },
            { "proximity_disabled", "<i><b>proximity chat <color=red>disabled</color></b></i>" }
        };
    }
}
