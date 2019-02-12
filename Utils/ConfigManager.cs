using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace AlphaPoint_QA.Utils
{
    public static class ConfigManager
    {
        public const string APPSETTINGFILENAME = "appsettings.json";

        static Config _Instance;
        public static AlphaPointWebDriver apwd; 

        public static Config Instance
        {
            get
            {

                if (_Instance == null)
                {
                    if (File.Exists(APPSETTINGFILENAME))
                    {
                        string jsontext = File.ReadAllText(APPSETTINGFILENAME);

                        _Instance = JsonConvert.DeserializeObject<Config>(jsontext);
                    }
                    else
                    {
                        _Instance = new Config();
                    }

                }
                return _Instance;
            }
        }

        public static object GetAppSetting(string key)
        {
            if (ConfigurationManager.AppSettings.AllKeys.Contains(key))
                return ConfigurationManager.AppSettings[key];
            return null;
        }

    }


}
