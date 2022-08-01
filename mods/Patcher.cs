using HarmonyLib;

using DecentM.Mods.TestMod.Patches;

namespace DecentM.Mods.TestMod
{
    internal class Patcher
    {
        public static void Patch()
        {
            AllowMoreComponents.PatchManually();

            // HarmonyLib.Harmony.CreateAndPatchAll(typeof(AllowMoreComponents));
            // HarmonyLib.Harmony.CreateAndPatchAll(typeof(AllowMoreComponents1));
            // HarmonyLib.Harmony.CreateAndPatchAll(typeof(AllowMoreComponents2));

            HarmonyLib.Harmony.CreateAndPatchAll(typeof(NoSteamRestartMetaPort));
            HarmonyLib.Harmony.CreateAndPatchAll(typeof(NoRestartSteamAPI));
        }
    }
}
