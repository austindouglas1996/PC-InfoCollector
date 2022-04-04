using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCInfoCollector.Domain
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
                if (CheckHex(value))
                {
                    value = "Invalid_Hex";
                }

                // I'm too lazy to do this decently ):
                if (value.Contains("&") |
                    value.Contains("#") |
                    value.Contains(";"))
                {
                    value = value.Replace("&", "");
                    value = value.Replace("#", "");
                    value = value.Replace(";", "");
                }

                _SerialNumber = value;
            }
        }
        private string _SerialNumber = "";
        public string TotalSpace { get; set; }

        public static bool CheckHex(String s)
        {
            // Size of string
            int n = s.Length;

            // Iterate over string
            for (int i = 0; i < n; i++)
            {
                char ch = s[i];

                // Check if the character
                // is invalid
                if ((ch < '0' || ch > '9') &&
                    (ch < 'A' || ch > 'F'))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
