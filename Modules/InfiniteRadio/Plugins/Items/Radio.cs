using HarmonyLib;
using InventorySystem.Items.Radio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;

namespace ChickenDinnerV2.Modules.InfiniteRadio.Plugins.Items
{

    [HarmonyPatch(typeof(RadioItem))]
    internal class Radio
    {
        protected static Config InfiniteRadioConfig = ChickenDinnerV2.Core.Main.Instance.Config.InfiniteRadio;

        [HarmonyPrefix]
        [HarmonyPatch("OnAdded")]
        public static bool Prefix(RadioItem __instance)
        {
            Dictionary<string, Dictionary<string, string>> ConfigRanges = InfiniteRadioConfig.RadioLevels;

            for (int level = 0; level < ConfigRanges.Count && level < __instance.Ranges.Length; level++)
            {
                float whenTalking = float.Parse(ConfigRanges.ElementAt(level).Value["MinuteCostWhenTalking"], CultureInfo.InvariantCulture.NumberFormat) * InfiniteRadioConfig.Modificator;
                float whenIdle = float.Parse(ConfigRanges.ElementAt(level).Value["MinuteCostWhenIdle"], CultureInfo.InvariantCulture.NumberFormat) * InfiniteRadioConfig.Modificator;

                __instance.Ranges[level].MinuteCostWhenTalking = Convert.ToInt32(whenTalking);
                __instance.Ranges[level].MinuteCostWhenIdle = whenIdle;
            }
            return true;
        }
    }
}
