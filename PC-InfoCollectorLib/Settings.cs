using PcInfoCollector.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PcInfoCollector
{
    public class Settings
    {
        /// <summary>
        /// Default name for the settings path filename.
        /// </summary>
        public const string Filename = "Configuration.xml";

        /// <summary>
        /// Initializes a new instance of the <see cref="Settings"/> class.
        /// </summary>
        public Settings()
        {
            WaitingToBeProcessedDir = "./ToBeProcessed/";
        }

        /// <summary>
        /// Directory to find files that are waiting to be processed.
        /// </summary>
        public string WaitingToBeProcessedDir { get; set; }

        /// <summary>
        /// Load a <see cref="Settings"/> instance from a file.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Save a <see cref="Settings"/> instance to a file.
        /// </summary>
        /// <param name="settings"></param>
        public static void SaveSettings(Settings settings)
        {
            XmlHelper.SerializeT<Settings>(Filename, settings, FileMode.Create, true);
        }
    }
}
