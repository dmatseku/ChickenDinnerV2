using ChickenDinnerV2.Core.Interfaces;
using System.ComponentModel;

namespace ChickenDinnerV2.Modules.DistanceModification
{
    public class Config : IModuleConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;

        [Description("Door distance to 173. Value from 0 to ~2.58")]
        public float DoorDistanceTo173 { get; set; } = 0f;

        [Description("Don't touch it")]
        public float DoorDistanceTo173Modifier { get; set; } = 0.9f;

    }
}
