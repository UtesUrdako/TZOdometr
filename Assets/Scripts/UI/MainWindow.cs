using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Odometr
{
    public class MainWindow : Window
    {
        [SerializeField] private Button menuButton;
        [SerializeField] private Button startButton;
        [SerializeField] private Toggle debugMode;
        [SerializeField] private OdometerDial odometerDial;
        [SerializeField] private GameObject connectIndicator;
        [SerializeField] private TMP_Text connectText;

        public event Action onMenuClick;
        public event Action onStartClick;
        public event Action<bool> onChangeDebugMode;

        public void Initialize()
        {
            menuButton.onClick.AddListener(() => onMenuClick?.Invoke());
            startButton.onClick.AddListener(() => onStartClick?.Invoke());
            debugMode.onValueChanged.AddListener(isOn => onChangeDebugMode?.Invoke(isOn));

            odometerDial.Initialize();
            SetConnect(false);
        }

        public void SetConnect(bool isConnect)
        {
            Debug.Log($"Change status connect: {isConnect}");
            connectIndicator.SetActive(isConnect);
            connectText.text = isConnect ? "ONLINE" : "OFFLINE";
        }

        public void SetOdometerValue(float value)
        {
            odometerDial.SetValue(value);
        }

        public void SetOdometerRawValue(float value)
        {
            odometerDial.SetRawValue(value);
        }

        public float GetOdometerValue()
        {
            return odometerDial.GetValue();
        }
    }
}