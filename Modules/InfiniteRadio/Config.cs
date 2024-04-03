using ChickenDinnerV2.Core.Interfaces;
using System.Collections.Generic;
using System.ComponentModel;

namespace ChickenDinnerV2.Modules.InfiniteRadio
{
    public class Config : IModuleConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;

        [Description("Instead of RadioLevels, between 0 and 1")]
        public float Modificator { get; set; } = 1.0f;

        [Description("Default: 1: (0.5, 40); 2: (1.2, 60); 3: (2.5, 120); 4: (13.5, 240)")]
        public Dictionary<string, Dictionary<string, float>> RadioLevels { get; set; } = new Dictionary<string, Dictionary<string, float>>
        {
            {
                "Level1", new Dictionary<string, float>
                {
                    { "MaximumRange", 85 },
                    { "MinuteCostWhenTalking", 40 },
                    { "MinuteCostWhenIdle", 0.5f }
                }
            },
            {
                "Level2", new Dictionary<string, float>
                {
                    { "MaximumRange", 175 },
                    { "MinuteCostWhenTalking", 60 },
                    { "MinuteCostWhenIdle", 1.2f }
                }
            },
            {
                "Level3", new Dictionary<string, float>
                {
                    { "MaximumRange", 1500 },
                    { "MinuteCostWhenTalking", 120 },
                    { "MinuteCostWhenIdle", 2.5f }
                }
            },
            {
                "Level4", new Dictionary<string, float>
                {
                    { "MaximumRange", 10000 },
                    { "MinuteCostWhenTalking", 240 },
                    { "MinuteCostWhenIdle", 13.5f }
                }
            }
        };
    }
}
