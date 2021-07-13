using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Fiddler.HttpPrivateInfoCheck.Configurations
{
    public class ConfigurationsManager
    {
        private string _configFile;

        private string ConfigFile
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(_configFile))
                {
                    return _configFile;
                }

                var appData = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);

                _configFile = Path.Combine(appData, "FiddlerPlugin.HttpPrivateInfoCheck");
                Directory.CreateDirectory(_configFile);
                _configFile = Path.Combine(_configFile, "config.json");
                return _configFile;
            }
        }

        public static ConfigurationsManager Instance { get; } = new ConfigurationsManager();

        public GlobalConfiguration Configurations { get; private set; } = new GlobalConfiguration();

        public GlobalConfiguration LoadConfiguration()
        {
            if (!File.Exists(ConfigFile))
            {
                Configurations = new GlobalConfiguration();
                return Configurations;
            }

            try
            {
                var text = File.ReadAllText(ConfigFile);
                var configuration = JsonConvert.DeserializeObject<GlobalConfiguration>(text);
                Configurations = configuration;
                return Configurations;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                Configurations = new GlobalConfiguration();
                return Configurations;
            }
        }

        public void Save()
        {
            var settings = new JsonSerializerSettings();
            settings.Formatting = Formatting.Indented;
            var text = JsonConvert.SerializeObject(Configurations, settings);
            File.WriteAllText(ConfigFile, text, Encoding.UTF8);
        }

    }
}
