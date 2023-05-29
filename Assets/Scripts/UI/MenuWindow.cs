using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Odometr
{
    public class MenuWindow : Window
    {
        [SerializeField] private OpenWindowAnimator panel;
        [SerializeField] private Button close;
        [SerializeField] private TMP_InputField addressServer;
        [SerializeField] private TMP_InputField port;
        [SerializeField] private TMP_InputField addressVideo;
        [SerializeField] private Toggle soundMute;
        [SerializeField] private Toggle musicMute;
        [SerializeField] private Slider soundValue;
        [SerializeField] private Slider musicValue;

        public event Action onClose;

        public event Action<bool> onSoundMute;
        public event Action<bool> onMusicMute;

        public event Action<float> onSoundValue;
        public event Action<float> onMusicValue;

        public void Initialize()
        {
            close.onClick.AddListener(() => onClose?.Invoke());

            soundMute.onValueChanged.AddListener(val => onSoundMute?.Invoke(val));
            musicMute.onValueChanged.AddListener(val => onMusicMute?.Invoke(val));

            soundValue.onValueChanged.AddListener(val => onSoundValue?.Invoke(val));
            musicValue.onValueChanged.AddListener(val => onMusicValue?.Invoke(val));
        }

        public float SoundVolume => soundValue.value;
        public float MusicVolume => musicValue.value;

        public string AddressServer
        {
            get => addressServer.text;
            set => addressServer.text = value; 
        }

        public string Port
        {
            get => port.text;
            set => port.text = value;
        }

        public string AddressVideo
        {
            get => addressVideo.text;
            set => addressVideo.text = value;
        }

        public void OpenPanel()
        {
            panel.AnimateOpen();
        }

        public void CloseePanel(Action finishCallback)
        {
            panel.AnimateClose(finishCallback);
        }
    }
}