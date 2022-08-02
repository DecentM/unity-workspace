using System;

using UnityEngine;

using ABI_RC.Core.Player;

namespace DecentM.Mods.ComponentsUnleashed.Utilities
{
    public struct TrackingData
    {
        public Vector3 position;
        public Quaternion rotation;
    }

    public enum TrackingDataType
    {
        Head,
        LeftHand,
        RightHand,
    }

    public static class RefugeeTools
    {
        public static int GetPlayerCount()
        {
            throw new NotImplementedException();
        }

        public static CVRPlayerEntity LocalPlayer
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public static bool IsUserInVR(CVRPlayerEntity player)
        {
            throw new NotImplementedException();
        }

        public static TrackingData GetTrackingData(CVRPlayerEntity player, TrackingDataType type)
        {
            throw new NotImplementedException();
        }

        public static bool PlayerIsValid(CVRPlayerEntity player)
        {
            // TODO: Expand this check once I know what I'm doing :D
            return player != null;
        }
    }
}
