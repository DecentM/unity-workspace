using System;
using System.Collections.Generic;

using MelonLoader;
using HarmonyLib;

using ABI_RC.Core;

namespace DecentM.Mods.ComponentsUnleashed
{
    public class Mod : MelonMod
    {
        public override void OnApplicationLateStart()
        {
            LoggerInstance.Msg("Extending component whitelist...");
            HashSet<Type> whiteList = (HashSet<Type>)Traverse.Create<CVRTools>().Field("componentWhiteList").GetValue();

            whiteList.Add(typeof(Pubsub.PubsubHost));
            whiteList.Add(typeof(Pubsub.PubsubSubscriber));

            Traverse.Create<CVRTools>().Field("componentWhiteList").SetValue(whiteList);
        }
    }
}
