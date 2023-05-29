using UnityEngine;

namespace Connect
{
    public class PostOdometrValueData : DataClass
    {
        public PostOdometrValueData(string address, string port, int value) : base()
        {
            form = new WWWForm();
            form.AddField("operation", "odometer_val");
            form.AddField("value", value);
            url = GetURL(address, port);
            requestType = RequestType.POST;
        }
    }
}
