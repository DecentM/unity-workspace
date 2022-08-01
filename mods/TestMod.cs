﻿using MelonLoader;

namespace DecentM.Mods.TestMod
{
    public class Mod : MelonMod
    {
        public override void OnApplicationStart()
        {
            LoggerInstance.Msg("Patching...");
            Patcher.Patch();
            LoggerInstance.Msg("After patching!");
        }

        public override void OnApplicationLateStart()
        {
            LoggerInstance.Msg("OnApplicationStart();");
        }
    }
}
