using HarmonyLib;
using UnityEngine;

namespace DecentM.Mods.TestMod.Patches
{
    [HarmonyPatch(typeof(CVRMirror), "Start")]
    class BreakMirrors
    {
        static AccessTools.FieldRef<CVRMirror, LayerMask> layerMaskRef = AccessTools.FieldRefAccess<CVRMirror, LayerMask>("m_ReflectLayers");

        [HarmonyPostfix]
        static void Postfix(CVRMirror __instance)
        {
            layerMaskRef(__instance) = 0;
        }
    }
}
