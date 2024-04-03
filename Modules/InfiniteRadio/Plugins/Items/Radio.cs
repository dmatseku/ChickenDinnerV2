using HarmonyLib;
using InventorySystem.Items.Radio;
using ChickenDinnerV2.Modules.InfiniteRadio.Model;

namespace ChickenDinnerV2.Modules.InfiniteRadio.Plugins.Items
{

    [HarmonyPatch(typeof(RadioItem))]
    internal class Radio
    {
        [HarmonyPrefix]
        [HarmonyPatch("OnAdded")]
        public static void Prefix(RadioItem __instance)
        {
            RadioRangesConfig.ApplyBatteryRanges(__instance);
        }
    }
}
