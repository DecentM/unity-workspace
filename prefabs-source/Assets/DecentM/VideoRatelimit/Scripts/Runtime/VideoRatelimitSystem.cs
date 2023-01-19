using UnityEngine;

using DecentM.Shared;
using DecentM.Collections;

namespace DecentM.VideoRatelimit
{
    public class VideoRatelimitSystem : DBehaviour
    {
        public float ratelimitSeconds = 5.2f;
        public Queue queue = new Queue();

        public const string PlaybackWindowEvent = "OnPlaybackWindow";

        private float elapsed = 0f;
        private bool isWaiting = false;

        private void FixedUpdate()
        {
            if (!this.isWaiting && this.queue.Count != 0)
            {
                this.SendWindowEventToFirst();
                return;
            }

            this.elapsed += Time.fixedUnscaledDeltaTime;
            // Using lte is important here because we want to avoid the rate limit by guaranteeing that we
            // only fire events after it opened.
            if (this.elapsed <= this.ratelimitSeconds)
                return;

            this.elapsed = 0f;
            this.isWaiting = false;

            this.SendWindowEventToFirst();
        }

        // If a player starts loading a URL, we already used up the current window, nothing we can do about it,
        // so we just reset the rate limit counter to 0 to start over.
        public void OnPlayerLoad()
        {
            this.elapsed = 0f;
            this.isWaiting = true;
        }

        public void RequestPlaybackWindow(DBehaviour behaviour)
        {
            this.queue.Enqueue(behaviour);
        }

        private void SendWindowEventToFirst()
        {
            if (this.queue.Count <= 0)
                return;

            DBehaviour behaviour = (DBehaviour)this.queue.Dequeue();
            if (behaviour == null)
                return;

            behaviour.Invoke(PlaybackWindowEvent, 0);

            // pre-emptively start waiting, assuming that this event will cause a video to be loaded
            // if we don't do this, a delayed video load by this behaviour could cause a race
            // condition where we send the window event to the next behaviour and this one starts loading
            // after a small delay
            this.isWaiting = true;
        }
    }
}
