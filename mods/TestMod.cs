using MelonLoader;

namespace DecentM.Mods
{
    public class TestMod : MelonMod
    {
        public override void OnApplicationLateStart()
        {
            LoggerInstance.Msg("[DecentM] OnApplicationStart();");
        }
    }
}
