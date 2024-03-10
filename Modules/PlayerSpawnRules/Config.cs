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

        public Dictionary<string, Dictionary<string, string>> RoleConfig { get; set; } = new Dictionary<string, Dictionary<string, string>>
        {
            {
                "Scp939", new Dictionary<string, string>
                {
                    { "HP", "100000" }
                }
            }
        };

        public Dictionary<string, Dictionary<string, string>> TeamConfig { get; set; } = new Dictionary<string, Dictionary<string, string>>
        {
            {
                "NineTailedFox", new Dictionary<string, string>
                {
                    { "max_team_size", "1" }
                }
            },
            {
                "ChaosInsurgency", new Dictionary<string, string>
                {
                    { "max_team_size", "1" }
                }
            }
        };

        [Description("Roles order on round start. several variants separated by coma. Key - item name, value - chance")]
        public Dictionary<string, List<List<Dictionary<string, string>>>> RoleItems { get; set; } = new Dictionary<string, List<List<Dictionary<string, string>>>>
        {
            {
                "Scientist", new List<List<Dictionary<string, string>>>
                {
                    {
                        new List<Dictionary<string, string>>
                        {
                            new Dictionary<string, string>
                            {
                                { "Item", "KeycardScientist" },
                                { "Count", "1" },
                                { "Chance", "100" }
                            },
                            new Dictionary<string, string>
                            {
                                { "Item", "GunCOM15" },
                                { "Count", "1" },
                                { "Chance", "50" }
                            }
                        }
                    },
                    {
                        new List<Dictionary<string, string>>
                        {
                            new Dictionary<string, string>
                            {
                                { "Item", "KeycardZoneManager" },
                                { "Count", "1" },
                                { "Chance", "100" }
                            },
                            new Dictionary<string, string>
                            {
                                { "Item", "GunCOM17" },
                                { "Count", "1" },
                                { "Chance", "50" }
                            }
                        }
                    }
                }
            }
        };
    }
}