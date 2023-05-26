using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Odometr
{
    public class OdometerDial : MonoBehaviour
    {
        [Range(0f, 1000f)] public float value;
        [SerializeField] private NumberSlot[] number;

        private void Update()
        {
            value += Time.deltaTime;
            for (int i = 0; i < number.Length; i++)
            {
                int period = (int)Mathf.Pow(10, i);
                float num = (value % (period * 10) / period);
                number[i].SetNumber(num);
            }
        }
    }
}