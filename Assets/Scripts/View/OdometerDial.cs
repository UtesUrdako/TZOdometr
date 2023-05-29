using System;
using UnityEngine;

namespace Odometr
{
    public class OdometerDial : MonoBehaviour
    {
        [SerializeField] private NumberSlot[] numbers;

        private float value;
        private float maxValue;

        public void Initialize()
        {
            string max = "";
            foreach (var number in numbers)
                max += "9";
            maxValue = Int32.Parse(max);
        }

        public void SetValue(float value)
        {
            this.value += value;
            if (this.value > maxValue)
                this.value = maxValue;

            for (int i = 0; i < numbers.Length; i++)
            {
                int period = (int)Mathf.Pow(10, i);
                float num = this.value % (period * 10) / period;
                numbers[i].SetNumber(num);
            }
        }

        public void SetRawValue(float value)
        {
            this.value = value;
            if (this.value > maxValue)
                this.value = maxValue;

            for (int i = 0; i < numbers.Length; i++)
            {
                int period = (int)Mathf.Pow(10, i);
                float num = this.value % (period * 10) / period;
                numbers[i].SetNumber(num);
            }
        }

        public float GetValue() =>
            value;
    }
}