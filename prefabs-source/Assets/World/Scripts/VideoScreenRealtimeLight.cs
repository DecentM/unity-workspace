using UnityEngine;

using DecentM.Shared;
using DecentM.VideoPlayer;
using DecentM.VideoPlayer.Plugins;

public class VideoScreenRealtimeLight : DBehaviour
{
    public ScreenAnalysisPlugin analysis;
    public VideoPlayerSystem system;

    private new Light light;

    private float elapsed = 0;

    private void Start()
    {
        this.light = GetComponent<Light>();
        this.light.enabled = false;
    }

    Color[] history = new Color[8];

    int index = 0;

    private void FixedUpdate()
    {
        if (!this.system.IsPlaying() || this.light == null)
            return;

        this.elapsed += Time.fixedUnscaledDeltaTime;
        if (this.elapsed <= 0.1f)
            return;
        this.elapsed = 0;

        if (index >= this.history.Length)
            index = 0;
        history[index] = this.analysis.GetAverage();
        index++;

        Color color = this.analysis.GetAverage(this.history) * 2;

        this.light.enabled = color.r + color.g + color.b >= 0.15;
        this.light.color = color;
    }
}
