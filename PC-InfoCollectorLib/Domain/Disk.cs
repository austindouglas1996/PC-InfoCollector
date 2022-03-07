using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PcInfoCollector.Domain
{
    public class Disk
    {
        public Disk()
        {
            Caption = "";
            SerialNumber = "";
            TotalSpace = "";
        }

        public string Caption { get; set; }
        public string SerialNumber
        {
            get { return _SerialNumber; }
            set
            {
                // Have noticed some hard drives contain HEX for a serial number.
                if (value.Contains("0x03"))
                    value = value.Replace("0x03", "");

                _SerialNumber = value;
            }
        }
        private string _SerialNumber = "";
        public string TotalSpace { get; set; }
    }
}
