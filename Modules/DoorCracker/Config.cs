using ChickenDinnerV2.Core.Interfaces;
using PlayerRoles;
using System.Collections.Generic;
using System.ComponentModel;

namespace ChickenDinnerV2.Modules.DoorCracker
{
    public class Config : IModuleConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;

        [Description("Simple door crack duration")]
        public int SimpleDoor { get; set; } = 5;

        [Description("Gate crack duration")]
        public int Gate { get; set; } = 15;

        [Description("Keycard door crack duration")]
        public int KeycardDoor { get; set; } = 25;

        [Description("Keycard gate crack duration")]
        public int KeycardGate { get; set; } = 35;

        public Dictionary<RoleTypeId, ItemType> AllowedRolesAndCards { get; set; } = new Dictionary<RoleTypeId, ItemType>
        {
            { RoleTypeId.NtfCaptain, ItemType.KeycardContainmentEngineer },
            { RoleTypeId.ChaosMarauder, ItemType.KeycardChaosInsurgency },
            { RoleTypeId.ChaosRepressor, ItemType.KeycardChaosInsurgency }
        };
    }
}
