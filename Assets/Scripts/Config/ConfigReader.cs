using System;
using System.IO;
using UnityEngine;

namespace Odometr
{
    public class ConfigReader
    {
        private string address;
        private string port;

        public string Address => address;
        public string Port => port;

        public void ReadConfig()
        {
            string path = "Config.txt";
            using (StreamReader sr = new StreamReader(GetFileLocation(path), System.Text.Encoding.Default))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] ss = line.Split(':', StringSplitOptions.RemoveEmptyEntries);
                    if (ss.Length == 2)
                    {
                        switch (ss[0])
                        {
                            case "Address server":
                                {
                                    string[] data = ss[1].Split(new char[1] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                    if (data.Length > 0)
                                        address = data[0];
                                }
                                break;
                            case "Port":
                                {
                                    string[] data = ss[1].Split(new char[1] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                    if (data.Length > 0)
                                        port = data[0];
                                }
                                break;
                        }
                    }
                }
            }
        }

        private string GetFileLocation(string relativePath) =>
            Path.Combine(Application.streamingAssetsPath, relativePath);
    }
}