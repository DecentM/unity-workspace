using DecentM.Mods.ComponentsUnleashed.Patches;

namespace DecentM.Mods.ComponentsUnleashed
{
    internal class Patcher
    {
        public static void Patch()
        {
            HarmonyLib.Harmony.CreateAndPatchAll(typeof(ExtendComponentWhitelistPatch));
        }
    }
}
