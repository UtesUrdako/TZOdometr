using UnityEngine;

namespace Connect
{
    public class GetOdometrValueData : DataClass
    {
        public GetOdometrValueData(string address, string port) : base()
        {
            form = new WWWForm();
            form.AddField("operation", "getCurrentOdometer");
            url = GetURL(address, port);
            requestType = RequestType.POST;
        }
    }
}
