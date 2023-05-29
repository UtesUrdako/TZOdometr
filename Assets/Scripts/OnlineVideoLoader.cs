using Sources;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class OnlineVideoLoader : MonoBehaviour
{

    public VideoPlayer videoPlayer;
    public string videoUrl = "rtsp://:8554/v";

    private Action<bool> checkSound;

    public void Initialize(AudioManager audioManager, Action<bool> checkSound)
    {
        this.checkSound = checkSound;
        videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
        videoPlayer.EnableAudioTrack(0, true);
        videoPlayer.SetTargetAudioSource(0, audioManager.SoundSource);
    }

    public void StartVideo()
    {
        StopCoroutine(CheckStream());

        videoPlayer.url = videoUrl;
        videoPlayer.Prepare();

        StartCoroutine(CheckStream());
    }

    private IEnumerator CheckStream()
    {
        while (true)
        {
            if (!videoPlayer.isPlaying &&
                videoPlayer.audioTrackCount == 0)
            {
                checkSound?.Invoke(false);
                break;
            }
            else
            {
                checkSound?.Invoke(true);
            }

            yield return null;
        }
    }
}