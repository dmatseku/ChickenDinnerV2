using ChickenDinnerV2.Core.Interfaces;
using System.Collections.Generic;
using System.ComponentModel;

namespace ChickenDinnerV2.Modules.DoNotTriggerTesla
{
    public class Config : IModuleConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;

        [Description("Tesla Role Whitelist\n    #   Roles:\n    #     Scp173\n    #     ClassD\n    #     Spectator\n    #     Scp106\n    #     NtfSpecialist\n    #     Scp049\n    #     Scientist\n    #     Scp079\n    #     ChaosConscript\n    #     Scp096\n    #     Scp0492\n    #     NtfSergeant\n    #     NtfCaptain\n    #     NtfPrivate\n    #     Tutorial\n    #     FacilityGuard\n    #     Scp939\n    #     CustomRole\n    #     ChaosRifleman\n    #     ChaosRepressor\n    #     ChaosMarauder\n    #     Overwatch\n    #     Filmmaker\n    #   States:\n    #    -1 - any\n    #     0 -  unleashed\n    #     1 - bounded")]
        public Dictionary<string, int> TeslaWhitelist { get; set; } = new Dictionary<string, int>
        {
            { "Tutorial", -1 }
        };
    }
}
