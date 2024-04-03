using InventorySystem.Items.Radio;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChickenDinnerV2.Modules.InfiniteRadio.Model
{
    internal static class RadioRangesConfig
    {
        private static Config InfiniteRadioConfig = ChickenDinnerV2.Core.Main.Instance.Config.InfiniteRadio;

        private static RadioRangeMode[] Ranges = null;

        private static RadioRangeMode[] getRangesArray()
        {
            if (Ranges == null)
            {
                List<RadioRangeMode> ranges = new List<RadioRangeMode>();

                for (int level = 0; level < InfiniteRadioConfig.RadioLevels.Count; level++)
                {
                    int maximumRange = Convert.ToInt32(InfiniteRadioConfig.RadioLevels.ElementAt(level).Value["MaximumRange"]);
                    float whenIdle = InfiniteRadioConfig.RadioLevels.ElementAt(level).Value["MinuteCostWhenIdle"] * InfiniteRadioConfig.Modificator;
                    int whenTalking = Convert.ToInt32(InfiniteRadioConfig.RadioLevels.ElementAt(level).Value["MinuteCostWhenTalking"] * InfiniteRadioConfig.Modificator);

                    ranges.Add(new RadioRangeMode { MaximumRange = maximumRange, MinuteCostWhenIdle = whenIdle, MinuteCostWhenTalking = whenTalking });
                }

                Ranges = ranges.ToArray();
            }

            return Ranges;
        }

        public static void ApplyBatteryRanges(RadioItem item)
        {
            item.Ranges = getRangesArray();
        }
    }
}
