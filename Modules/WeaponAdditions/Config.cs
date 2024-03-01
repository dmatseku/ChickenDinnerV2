using ChickenDinnerV2.Core.Interfaces;
using System.Collections.Generic;
using System.ComponentModel;

namespace ChickenDinnerV2.Modules.WeaponAdditions
{
    public class Config : IModuleConfig
    {
        public bool IsEnabled { get; set; } = true;

        public bool Debug { get; set; }

        [Description("Effect Asignment. weapon item -> effect")]
        public Dictionary<string, string> WeaponEffects { get; set; } = new Dictionary<string, string>()
        {
            { "GunCOM15", "shock" }
        };

        [Description("Effects config")]
        public Dictionary<string, Dictionary<string, string>> EffectsConfig { get; set; } = new Dictionary<string, Dictionary<string, string>>()
        {
            {
                "shock", new Dictionary<string, string>()
                {
                    { "effect_min_time", "5" },
                    { "effect_max_time", "10" },
                    { "post_effects_time", "15" },
                    { "Scp939_hit_count", "5" }
                }
            }
        };
    }
}