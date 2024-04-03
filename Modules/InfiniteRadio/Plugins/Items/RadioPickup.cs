using HarmonyLib;
using InventorySystem.Items.Radio;
using System;
using RadioPickupTarget = InventorySystem.Items.Radio.RadioPickup;
using System.Reflection;
using ChickenDinnerV2.Modules.InfiniteRadio.Model;

namespace ChickenDinnerV2.Modules.InfiniteRadio.Plugins.Items
{
    [HarmonyPatch(typeof(RadioPickupTarget))]
    internal class RadioPickup
    {
        private static RadioItem getRadioCache()
        {
            Type type = typeof(RadioPickupTarget);
            FieldInfo info = type.GetField("_radioCache", BindingFlags.NonPublic | BindingFlags.Static);
            return (RadioItem)info.GetValue(null);
        }

        [HarmonyPostfix]
        [HarmonyPatch("Awake")]
        public static void Postfix(RadioPickupTarget __instance)
        {
            RadioRangesConfig.ApplyBatteryRanges(getRadioCache());
        }
    }
}
