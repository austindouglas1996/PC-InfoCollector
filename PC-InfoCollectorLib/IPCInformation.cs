using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCInfoCollector.Domain;

namespace PCInfoCollector
{
    public interface IPCInformation
    {
        int Version { get; set; }
        string ComputerName { get; set; }
        string MotherboardModel { get; set; }
        string Manufacture { get; set; }
        string IPAddress { get; set; }
        string MACAddress { get; set; }
        string LastKnownUser { get; set; }
        DateTime LastRebootTime { get; set; }
        DateTime LastKnownCollection { get; set; }
        string OSName { get; set; }
        string Processor { get; set; }
        string MemoryInGB { get; set; }
        string GraphicsCard { get; set; }
        List<Disk> Disks { get; set; }
        List<Printer> Printers { get; set; }
        WarrantyInfo Warranty { get; set; }
    }
}
