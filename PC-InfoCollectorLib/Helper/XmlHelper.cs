using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PcInfoCollector.Helper
{
    public static class XmlHelper
    {
        /// <summary>
        /// Deserialize an object that was previously <see cref="SerializeT{T}(string, T, FileMode, bool)"/> back into an object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Serialize an object into a XML file that can be later loaded by <see cref="DeserializeT{T}(string)"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <param name="value"></param>
        /// <param name="mode"></param>
        /// <param name="deleteExisting"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
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
