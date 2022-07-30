using MelonLoader;

namespace DecentM.Mods.TestMod
{
    public class Mod : MelonMod
    {
        public override void OnApplicationStart()
        {
            // Patcher.Patch();
        }

        public override void OnApplicationLateStart()
        {
            LoggerInstance.Msg("[DecentM] OnApplicationStart();");
        }
    }
}
