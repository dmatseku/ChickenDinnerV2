using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using ChickenDinnerV2.Core.Tools;
using HarmonyLib;
using Interactables.Interobjects.DoorUtils;
using NorthwoodLib.Pools;
using PlayerRoles;

namespace ChickenDinnerV2.Modules.ForbidToOpenLockedDoors.Plugins.Player
{
    [HarmonyPatch(typeof(DoorVariant))]
    [HarmonyPatch("ServerInteract")]
    internal class ForbidOpening
    {
        protected static Config ForbidToOpenLockedDoorsConfig = ChickenDinnerV2.Core.Main.Instance.Config.ForbidToOpenLockedDoors;

        public static bool Is079(ReferenceHub ply)
        {
            return ply.GetRoleId() == RoleTypeId.Scp079;
        }

        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
        {
            if (!ForbidToOpenLockedDoorsConfig.IsEnabled)
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
                    .Copy(count: 36)
                    .Call(typeof(ForbidOpening), nameof(ForbidOpening.Is079), new Type[] { typeof(ReferenceHub) })
                    .Copy(from: 38)
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
