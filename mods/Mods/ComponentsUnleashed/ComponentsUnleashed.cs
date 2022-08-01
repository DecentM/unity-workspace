using MelonLoader;

namespace DecentM.Mods.ComponentsUnleashed
{
    public class Mod : MelonMod
    {
        public override void OnApplicationStart()
        {
            LoggerInstance.Msg("Patching...");
            Patcher.Patch();
            LoggerInstance.Msg("After patching");
        }
    }
}
