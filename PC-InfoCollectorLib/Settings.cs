using CrownCollector.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CrownCollector
{
    public class Settings
    {
        public const string Filename = "Configuration.xml";

        public Settings()
        {
            WaitingToBeProcessedDir = "./ToBeProcessed/";
        }

        public string WaitingToBeProcessedDir { get; set; }

        public static Settings LoadSettings(string path = Filename)
        {
            Settings settings = XmlHelper.DeserializeT<Settings>(path);

            // Create and save if the settings does not exist.
            if (settings == null)
            {
                settings = new Settings();
                SaveSettings(settings);
            }

            return settings;
        }

        public static void SaveSettings(Settings settings)
        {
            XmlHelper.SerializeT<Settings>(Filename, settings, FileMode.Create, true);
        }
    }
}
