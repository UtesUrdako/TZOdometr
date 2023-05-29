using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Networking;

namespace Connect
{
    public abstract class DataClass
    {
        private readonly string Url = "http://";

        protected string url;
        protected byte[] myData;
        protected RequestType requestType;
        protected WWWForm form;

        protected DataClass() { }

        internal virtual UnityWebRequest GetWebRequest()
        {
            UnityWebRequest webRequest = null;
            switch (requestType)
            {
                case RequestType.GET:
                    webRequest = UnityWebRequest.Get(url);
                    break;
                case RequestType.POST:
                    webRequest = UnityWebRequest.Post(url, form);
                    break;
                case RequestType.PUT:
                case RequestType.PATCH:
                    webRequest = UnityWebRequest.Put(url, myData);
                    break;
            }
            return webRequest;
        }

        public string GetURL(string address, string port)
        {
            return $"{Url}{address}:{port}/ws";
        }
    }
}