using ChickenDinnerV2.Core.Tools;
using HarmonyLib;
using PlayerRoles.PlayableScps.Scp173;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using NorthwoodLib.Pools;

namespace ChickenDinnerV2.Modules.ObserversCounter.Plugins.Scp173
{

    [HarmonyPatch(typeof(Scp173TeleportAbility))]
    [HarmonyPatch("ServerProcessCmd")]
    internal class TeleportAbility
    {
        protected static Config ObserversCounterConfig = ChickenDinnerV2.Core.Main.Instance.Config.ObserversCounter;

        public static bool IsTeleportAllowed(Scp173BlinkTimer blinkTimer, Scp173ObserversTracker observersTracker)
        {
            return (blinkTimer.AbilityReady && observersTracker.CurrentObservers < ObserversCounterConfig.ObserversCount);
        }

        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
        {
            if (!ObserversCounterConfig.IsEnabled)
            {
                foreach (CodeInstruction instruction in instructions)
                {
                    yield return instruction;
                }
            }
            else
            {
                List<CodeInstruction> newInstructions = ListPool<CodeInstruction>.Shared.Rent(instructions);
                CodeInstruction[] result;

                result = new ILBuilder(newInstructions, generator)
                    .Copy(count: 19)
                    .Ldarg(0)
                    .Ldfld(typeof(Scp173TeleportAbility), "_observersTracker")
                    .Call(typeof(TeleportAbility), "IsTeleportAllowed", new Type[] { typeof(Scp173BlinkTimer), typeof(Scp173ObserversTracker) })
                    .Copy(from: 20)
                    .GetResult();
                foreach (CodeInstruction instruction in result)
                {
                    yield return instruction;
                }
                ListPool<CodeInstruction>.Shared.Return(newInstructions);
            }
        }
    }
}
