using ChickenDinnerV2.Core.Interfaces;
using System.ComponentModel;
using System.Collections.Generic;

namespace ChickenDinnerV2.Modules.PlayerSpawnRules
{
    public class Config : IModuleConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;

        [Description("Roles order on round start. several variants separated by coma")]
        public List<string> spawnOrder { get; set; } = new List<string>()
        {
            "ClassD",
            "Scp096, Scp173, Scp106, Scp049, Scp079, Scp939, Scp3114"
        };

        [Description("Roles order on round start. several variants separated by coma")]
        public Dictionary<string, Dictionary<string, Dictionary<string, int>>> RoleItems { get; set; } = new Dictionary<string, Dictionary<string, Dictionary<string, int>>>()
        {
            {
                "Scientist", new Dictionary<string, Dictionary<string, int>>
                {
                    {
                        "Scientist", new Dictionary<string, int>
                        {
                            { "KeycardScientist", 1 }
                        }
                    },
                    {
                        "ZoneManager", new Dictionary<string, int>
                        {
                            { "KeycardZoneManager", 1 }
                        }
                    }
                }
            }
        };
    }
}