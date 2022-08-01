using System;
using HarmonyLib;
using UnityEngine;

using ABI_RC.Core;
using ABI_RC.Core.EventSystem;

namespace DecentM.Mods.ComponentsUnleashed.Patches
{
	class ExtendComponentWhitelistPatch
	{
		[HarmonyPatch(typeof(CVRTools), nameof(CVRTools.CleanAvatarGameObject), new Type[] { typeof(GameObject), typeof(int), typeof(bool), typeof(AssetManagement.AvatarTags), typeof(bool), typeof(bool), typeof(bool), typeof(bool) })]
		[HarmonyPrefix]
		static bool AvatarPrefix(GameObject avatar, int layer, bool isFriend, AssetManagement.AvatarTags tags, bool disableAudio = false, bool forceShow = false, bool forceBlock = false, bool secondRun = false)
		{
			CVRTools.SetGameObjectLayerRecursive(avatar, layer);

			if (layer == 10)
				CVRTools.PlaceHapticTriggersAndPointers(avatar);

			CVRTools.GenerateDefaultPointer(avatar);
			CVRTools.AvatarCleaned.Invoke(avatar);

			return false;
		}

        [HarmonyPatch(typeof(CVRTools), nameof(CVRTools.CleanPropGameObjectNetwork))]
        [HarmonyPrefix]
		static bool PropPrefix()
        {
			return false;
        }
	}
}
