using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Sources
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioMixerGroup soundGroup;
        [SerializeField] private AudioMixerGroup musicGroup;

        private Dictionary<string, AudioData> sounds = new Dictionary<string, AudioData>();
        private Dictionary<string, AudioData> music = new Dictionary<string, AudioData>();

        [SerializeField] private AudioSource soundSource;
        [SerializeField] private AudioSource musicSource;

        private bool isPlayAllMusic = false;

        public AudioSource SoundSource => soundSource;

        public void Initialize()
        {
            AudioConfig audioConfig = Resources.Load<AudioConfig>("AudioConfig");
            if (audioConfig == null)
            {
                Debug.LogError("Can't find audio list");
                return;
            }

            soundGroup = audioConfig.SoundMixerGroup;
            musicGroup = audioConfig.MusicMixerGroup;

            foreach (AudioData sound in audioConfig.Sounds)
            {
                sounds[sound.Clip.name] = sound;
            }
            foreach (AudioData music in audioConfig.Music)
            {
                this.music[music.Clip.name] = music;
            }

            soundSource = gameObject.AddComponent<AudioSource>();
            musicSource = gameObject.AddComponent<AudioSource>();
            musicSource.loop = true;
            
            soundSource.outputAudioMixerGroup = soundGroup;
            musicSource.outputAudioMixerGroup = musicGroup;
        }
        
        public void PlaySound(AudioClip clip)
        {
            PlaySound(clip.name);
        }

        public void PlaySound(string soundName)
        {
            if (!sounds.ContainsKey(soundName))
            {
                Debug.LogError("There is no sound with name " + soundName);
                return;
            }
            
            AudioData audio = sounds[soundName];
            soundSource.PlayOneShot(audio.Clip, audio.Volume);
        }

        public void PlayAllMusic()
        {
            isPlayAllMusic = true;
            StartCoroutine(PlayAllMusicRoutine());
        }

        public void StopAllMusic()
        {
            isPlayAllMusic = false;
        }
        
        public void SetSound2DIsOn(bool isOn)
        {
            Debug.Log("Sound: " + isOn);
            soundGroup.audioMixer.SetFloat("SoundVolume", isOn ? 0f : -80f);
        }
        
        public void SetMusic2DIsOn(bool isOn)
        {
            Debug.Log("Music: " + isOn);
            musicGroup.audioMixer.SetFloat("MusicVolume", isOn ? 0f : -80f);
        }

        public void SetSoundVolume(float volume)
        {
            Debug.Log("Sound: " + volume);
            soundGroup.audioMixer.SetFloat("SoundVolume", volume * 100 - 80);
        }
        
        public void SetMusicVolume(float volume)
        {
            Debug.Log("Music: " + volume);
            musicGroup.audioMixer.SetFloat("MusicVolume", volume * 100 - 80);
        }
        
        private IEnumerator PlayAllMusicRoutine()
        {
            if (music == null || music.Count == 0)
            {
                Debug.LogError("There is no music");
                yield break;
            }

            while (isPlayAllMusic)
            {
                foreach (var audioData in music.Values)
                {
                    musicSource.clip = audioData.Clip;
                    musicSource.volume = audioData.Volume;
                    musicSource.Play();

                    while (musicSource.isPlaying)
                        yield return null;
                }
            }
        }
        
    }
}

