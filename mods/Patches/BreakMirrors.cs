using HarmonyLib;
using UnityEngine;

namespace DecentM.Mods.TestMod.Patches
{
    [HarmonyPatch(nameof(CVRMirror), "Start", MethodType.Normal)]
    internal class BreakMirrors
    {
        static AccessTools.FieldRef<CVRMirror, LayerMask> layerMaskRef = AccessTools.FieldRefAccess<CVRMirror, LayerMask>("m_ReflectLayers");

        [HarmonyPostfix]
        private static void Postfix(CVRMirror __instance)
        {
            layerMaskRef(__instance) = 0;
        }
    }
}
