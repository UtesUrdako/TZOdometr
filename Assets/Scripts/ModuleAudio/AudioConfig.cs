#pragma warning disable 649
using UnityEngine;
using UnityEngine.Audio;

namespace Sources
{
    [CreateAssetMenu(fileName = "AudioConfig", menuName = "Scriptable/AudioConfig")]
    public class AudioConfig : ScriptableObject
    {
        [SerializeField] private AudioMixerGroup _soundMixerGroup;
        [SerializeField] private AudioMixerGroup _musicMixerGroup;
        [SerializeField] private AudioData[] _sounds; 
        [SerializeField] private AudioData[] _music;

        public AudioMixerGroup SoundMixerGroup => _soundMixerGroup;
        public AudioMixerGroup MusicMixerGroup => _musicMixerGroup;
        public AudioData[] Sounds => _sounds;
        public AudioData[] Music => _music;
    }
}