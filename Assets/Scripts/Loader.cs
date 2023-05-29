using Connect;
using Sources;
using System.Collections;
using TMPro;
using UnityEngine;
using WebSocketSharp;

namespace Odometr
{
    public class Loader : MonoBehaviour
    {
        [SerializeField] private MainWindow mainWindow;
        [SerializeField] private MenuWindow menuWindow;
        [SerializeField] private AudioManager audioManager;
        [SerializeField] private OnlineVideoLoader videoLoader;

        private APIConnect connect;
        private bool isDebugMode;

        private void Awake()
        {
            mainWindow.Initialize();
            menuWindow.Initialize();

            mainWindow.onStartClick += videoLoader.StartVideo;
            mainWindow.onMenuClick += OpenMenu;
            menuWindow.onClose += CloseMenu;
            mainWindow.onChangeDebugMode += OnChangeDebugMode;

            menuWindow.onSoundMute += MuteSound;
            menuWindow.onMusicMute += MuteMusic;
            menuWindow.onSoundValue += audioManager.SetSoundVolume;
            menuWindow.onMusicValue += audioManager.SetMusicVolume;

            ConfigReader configReader = new ConfigReader();
            configReader.ReadConfig();
            menuWindow.AddressServer = configReader.Address;
            menuWindow.Port = configReader.Port;

            audioManager.Initialize();
            audioManager.SetSoundVolume(menuWindow.SoundVolume);
            audioManager.SetMusicVolume(menuWindow.MusicVolume);
            audioManager.PlayAllMusic();

            Connect();

            videoLoader.Initialize(audioManager, OnCheckSound);
        }

        private void Connect()
        {
            if (isDebugMode) return;
            
            connect = new APIConnect();
            connect.Initialize(menuWindow.AddressServer, menuWindow.Port);
            connect.Success += Success;
            connect.WsCall("{\"operation\": \"getRandomStatus\"}");
        }

        private void Success(Result result)
        {
            switch (result.GetOperation())
            {
                case "randomStatus":
                    if (result.GetStatus())
                    {
                        mainWindow.SetConnect(true);
                        mainWindow.SetOdometerRawValue(result.GetValue());
                    }
                    else
                    {
                        mainWindow.SetConnect(false);
                        connect.WsCall("{\"operation\": \"getRandomStatus\"}");
                    }
                    break;
                case "odometer_val":
                    mainWindow.SetOdometerRawValue(result.GetValue());
                    break;
            }
        }

        private void OpenMenu()
        {
            mainWindow.DeactivateWindow();
            menuWindow.ActivateWindow();
            menuWindow.OpenPanel();
        }

        private void CloseMenu()
        {
            menuWindow.CloseePanel(() =>
            {
                menuWindow.DeactivateWindow();
                mainWindow.ActivateWindow();
            });
        }

        private void MuteSound(bool isOn)
        {
            audioManager.SetSound2DIsOn(isOn);
            if (isOn)
                audioManager.SetSoundVolume(menuWindow.SoundVolume);
        }

        private void MuteMusic(bool isOn)
        {
            audioManager.SetMusic2DIsOn(isOn);
            if (isOn)
                audioManager.SetMusicVolume(menuWindow.MusicVolume);
        }

        private void OnCheckSound(bool isOn)
        {
            MuteSound(isOn);
            MuteMusic(!isOn);
        }

        private void OnChangeDebugMode(bool isEnable)
        {
            isDebugMode = isEnable;
            StartCoroutine(EmulateOdometer());
        }

        private IEnumerator EmulateOdometer()
        {
            if (!isDebugMode)
            {
                mainWindow.SetConnect(false);
                Connect();
                yield break;
            }

            mainWindow.SetConnect(true);
            mainWindow.SetOdometerValue(Time.fixedDeltaTime);

            yield return new WaitForFixedUpdate();
            StartCoroutine(EmulateOdometer());
        }

        private void OnDestroy()
        {
            connect.Destroy();
        }
    }
}