using System;
using System.Collections.Generic;
using HarmonyLib;
using ABI_RC.Core;
using UnityEngine;
using MelonLoader;

using ABI.CCK.Components;
using ABI_RC.Core.Player;
using ABI_RC.Core.EventSystem;

using RootMotion.FinalIK;
using UnityEngine.Animations;

namespace DecentM.Mods.TestMod.Patches
{
    class AllowMoreComponents
    {
        // static AccessTools.FieldRef<CVRTools, HashSet<Type>> componentWhiteListRef = AccessTools.FieldRefAccess<CVRTools, HashSet<Type>>("componentWhiteList");

        public static void PatchManually()
        {
			HashSet<Type> newWhitelist = new HashSet<Type>
            {
				typeof(SkinnedMeshRenderer),
				typeof(MeshRenderer),
				typeof(CVREyeController),
				typeof(CVRVisemeController),
				typeof(CCDIK),
				typeof(Rigidbody),
				typeof(FixedJoint),
				typeof(HingeJoint),
				typeof(SpringJoint),
				typeof(LineRenderer),
				typeof(MeshFilter),
				typeof(AimConstraint),
				typeof(ParentConstraint),
				typeof(PositionConstraint),
				typeof(RotationConstraint),
				typeof(ScaleConstraint),
				typeof(LookAtConstraint),
				typeof(TrailRenderer),
				typeof(Cloth),
				typeof(LightProbeProxyVolume),
				typeof(LimbIK),
				typeof(BipedIK),
				typeof(GrounderIK),
				typeof(ConfigurableJoint),
				typeof(FullBodyBipedIK),
				typeof(GrounderBipedIK),
				typeof(RotationLimitAngle),
				typeof(RotationLimitHinge),
				typeof(RotationLimitPolygonal),
				typeof(RotationLimitSpline),
				typeof(RotationLimitUtilities),
				typeof(FABRIK),
				typeof(FABRIKRoot),
				typeof(FABRIKChain),
				typeof(CharacterJoint),
				typeof(CVRFaceTracking),
				typeof(CVRMaterialDriver),
				typeof(CVRAdvancedAvatarSettingsTriggerHelper),
				typeof(CVRCameraHelper),
				typeof(CVRAnimatorDriver),
				typeof(CVRMaterialUpdater),
				typeof(CVRParticleSound),
				typeof(CVRAudioDriver),
				typeof(ConstantForce),
				typeof(CVRDistanceConstrain),

				// Custom ones:
				typeof(ScriptableObject),
				typeof(MonoBehaviour),
			};

			Traverse.Create(typeof(CVRTools)).Field("componentWhiteList").SetValue(newWhitelist);
		}
	}
}
