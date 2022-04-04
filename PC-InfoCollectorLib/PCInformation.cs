using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using PCInfoCollector.Domain;
using PCInfoCollector.Helper;

namespace PCInfoCollector
{
    public class PCInformation : IPCInformation
    {
        public PCInformation()
        {
            Version = 1;
            ComputerName = "";
            MotherboardModel = "";
            Manufacture = "";
            IPAddress = "";
            MACAddress = "";
            LastKnownUser = "";
            LastRebootTime = DateTime.MinValue;
            LastKnownCollection = DateTime.Now;
            OSName = "";
            Processor = "";
            MemoryInGB = "";
            GraphicsCard = "";
            Disks = new List<Disk>();
            Printers = new List<Printer>();
            Warranty = new WarrantyInfo();
        }

        public int Version { get; set; }
        public string ComputerName { get; set; }

        [XmlElement(IsNullable = true)]
        public string MotherboardModel { get; set; }
        public string Manufacture { get; set; }
        public string IPAddress { get; set; }
        public string MACAddress { get; set; }
        public string LastKnownUser { get; set; }
        public DateTime LastRebootTime { get; set; }
        public DateTime LastKnownCollection { get; set; }
        public string OSName { get; set; }
        public string Processor { get; set; }
        public string MemoryInGB { get; set; }
        public string GraphicsCard { get; set; }
        public List<Disk> Disks { get; set; }
        public List<Printer> Printers { get; set; }
        public WarrantyInfo Warranty { get; set; }

        public static PCInformation Load(string path)
        {
            return XmlHelper.DeserializeT<PCInformation>(path);
        }

        public static bool Save(string directoryPath, PCInformation pc)
        {
            string filePath = directoryPath + pc.MACAddress + ".xml";
            return XmlHelper.SerializeT<PCInformation>(filePath, pc, FileMode.Create, true);
        }
    }
}
