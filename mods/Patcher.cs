using HarmonyLib;

using DecentM.Mods.TestMod.Patches;

namespace DecentM.Mods.TestMod
{
    internal class Patcher
    {
        public static void Patch()
        {
            HarmonyLib.Harmony.CreateAndPatchAll(typeof(BreakMirrors));
        }
    }
}
