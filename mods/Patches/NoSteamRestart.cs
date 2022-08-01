using System;
using System.Collections.Generic;
using HarmonyLib;
using ABI_RC.Core.Savior;
using UnityEngine;
using Steamworks;
using MelonLoader;

namespace DecentM.Mods.TestMod.Patches
{
    [HarmonyPatch(typeof(MetaPort), "Start")]
    class NoSteamRestartMetaPort
    {
        [HarmonyPrefix]
        static bool Prefix()
        {
            return false;
        }
    }

	[HarmonyPatch(typeof(SteamAPI), nameof(SteamAPI.RestartAppIfNecessary))]
	class NoRestartSteamAPI
    {
		[HarmonyPrefix]
		static bool Prefix(ref bool __result)
        {
			__result = false;
			return false;
        }
	}

	/* [HarmonyPatch(typeof(SteamManager), "Awake")]
    class NoSteamRestartSteamManager
    {
		static AccessTools.FieldRef<SteamManager, Boolean> m_bInitializedRef = AccessTools.FieldRefAccess<SteamManager, Boolean>("m_bInitialized");
		static AccessTools.FieldRef<SteamManager, SteamManager> s_instanceRef = AccessTools.FieldRefAccess<SteamManager, SteamManager>("s_instance");
		static AccessTools.FieldRef<SteamManager, GameObject> gameObjectRef = AccessTools.FieldRefAccess<SteamManager, GameObject>("gameObject");

		private static bool initialised = false;

		[HarmonyPrefix]
        static bool Prefix(SteamManager __instance)
        {
			if (s_instanceRef() != null)
			{
				UnityEngine.Object.Destroy(gameObjectRef(__instance));
				return false;
			}

			s_instanceRef() = __instance;

			// If we initialised once, we just ignore the new init request
			if (initialised)
				return false;

			UnityEngine.Object.DontDestroyOnLoad(gameObjectRef(__instance));

			// Init steam API, since it's required to show content
			bool steamInited = SteamAPI.Init();
			m_bInitializedRef(__instance) = steamInited;

			// Steam couldn't load for some reason, try again with the vanilla handler
			if (!steamInited)
			{
				MelonLogger.Msg("Steamworks failed to load, deferring initialisation to vanilla handler...");
				return true;
			}

			// Mark the current session as initialised to prevent duplicating steamapi inits
			initialised = true;

			return false;
        }
    } */
}
