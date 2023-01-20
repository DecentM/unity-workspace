using UnityEngine;

using DecentM.Shared;

namespace DecentM.VideoRatelimit
{
    public class PlayerLoadMonitoring : DBehaviour
    {
        public VideoRatelimitSystem system;

        public void OnVideoReady()
        {
            this.system.OnPlayerLoad();
        }
    }
}
