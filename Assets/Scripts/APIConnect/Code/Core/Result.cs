using Newtonsoft.Json;
using UnityEngine;

namespace Connect
{
    public class Result
    {
        public Result()
        { }

        private Data _data;

        private class Data
        {
            public string operation;
            public bool status;
            public float odometer;
        }

        internal virtual void Init(string answer)
        {
#if UNITY_EDITOR
            Debug.Log(answer);
#endif
            _data = JsonConvert.DeserializeObject<Data>(answer);
        }

        public string GetOperation() =>
            _data.operation;

        public bool GetStatus() =>
            _data.status;

        public float GetValue() =>
            _data.odometer;
    }
}
