namespace DecentM.Prefabs.VideoPlayer.Handlers
{
    public class UnityVideo : PlayerHandler
    {
        public override VideoPlayerHandlerType type => VideoPlayerHandlerType.Unity;

        public UnityEngine.Video.VideoPlayer player;

        private void Start()
        {
            this.player.loopPointReached += OnPlaybackEnd;
            this.player.prepareCompleted += OnPlayerLoaded;
            this.player.errorReceived += OnVideoError;
        }

        private void OnVideoError(UnityEngine.Video.VideoPlayer source, string message)
        {
            this.baseHandler.OnVideoError(message);
        }

        private void OnPlayerLoaded(UnityEngine.Video.VideoPlayer source)
        {
            this.baseHandler.OnVideoReady();
        }

        private void OnPlaybackEnd(UnityEngine.Video.VideoPlayer source)
        {
            this.baseHandler.OnVideoEnd();
        }

        public override bool IsPlaying
        {
            get
            {
                return this.player.isPlaying;
            }
        }

        public override float GetDuration()
        {
            return (float)this.player.length;
        }

        public override float GetTime()
        {
            return (float)this.player.time;
        }

        public override void SetTime(float time)
        {
            this.player.time = time;
        }

        public override void LoadURL(string url)
        {
            this.player.url = url;
        }

        public override void Pause()
        {
            this.player.Pause();
            this.baseHandler.OnVideoPause();
        }

        public override void Pause(float time)
        {
            this.Pause();
            this.SetTime(time);
        }

        public override void Play()
        {
            this.player.Play();
            this.baseHandler.OnVideoPlay();
        }

        public override void Play(float time)
        {
            this.SetTime(time);
            this.Play();
        }

        public override void Unload()
        {
            this.Pause();
            this.SetTime(0);
            this.player.url = null;
        }
    }
}
