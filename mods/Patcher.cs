using HarmonyLib;

namespace DecentM.Mods.TestMod
{
    internal class Patcher
    {
        public static void Patch()
        {
            HarmonyLib.Harmony harmony = new HarmonyLib.Harmony("com.decentm.testmod");

            harmony.PatchAll();
        }
    }
}
