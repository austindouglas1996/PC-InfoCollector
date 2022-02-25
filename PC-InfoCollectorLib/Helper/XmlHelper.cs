using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CrownCollector.Helper
{
    public static class XmlHelper
    {
        public static T DeserializeT<T>(string path)
        {
            if (!File.Exists(path))
                return default(T);

            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                XmlSerializer serialier = new XmlSerializer(typeof(T));
                return (T)serialier.Deserialize(fs);
            }
        }

        public static bool SerializeT<T>(string path, T value, FileMode mode, bool deleteExisting = false)
        {
            if (File.Exists(path))
            {
                if (deleteExisting)
                    File.Delete(path);
                else if (!deleteExisting && mode == FileMode.Create)
                    throw new ArgumentException("File already exists. Unable to serialize object.");
            }

            using (FileStream fs = new FileStream(path, mode))
            {
                XmlSerializer serialier = new XmlSerializer(typeof(T));
                serialier.Serialize(fs, value);
            }

            return true;
        }
    }
}
