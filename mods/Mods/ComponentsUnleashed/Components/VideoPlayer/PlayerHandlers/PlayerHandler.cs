using UnityEngine;

namespace DecentM.Prefabs.VideoPlayer.Handlers
{
    public enum VideoPlayerHandlerType
    {
        AVPro,
        Unity
    }

    public abstract class PlayerHandler : MonoBehaviour
    {
        public abstract VideoPlayerHandlerType type { get; }

        protected BasePlayerHandler baseHandler;

        public void RegisterBaseHandler(BasePlayerHandler baseHandler)
        {
            this.baseHandler = baseHandler;
        }

        public abstract bool IsPlaying { get; }

        public abstract float GetDuration();

        public abstract float GetTime();

        public abstract void SetTime(float time);

        public abstract void LoadURL(string url);

        public abstract void Play();

        public abstract void Play(float time);

        public abstract void Unload();

        public abstract void Pause();

        public abstract void Pause(float time);
    }
}

