using UnityEngine;

namespace Connect
{
    public class GetOdometrStatusData : DataClass
    {
        public GetOdometrStatusData(string address, string port) : base()
        {
            form = new WWWForm();
            form.AddField("operation", "getRandomStatus");
            url = GetURL(address, port);
            requestType = RequestType.POST;
        }
    }
}
