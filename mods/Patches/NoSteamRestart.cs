using System;
using System.Collections.Generic;
using HarmonyLib;
using ABI_RC.Core.Savior;
using UnityEngine;
using Steamworks;

namespace DecentM.Mods.NoSteamRestart.Patches
{
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
}
