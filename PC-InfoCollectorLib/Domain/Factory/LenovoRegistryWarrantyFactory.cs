using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCInfoCollector.Domain.Factory
{
    public class LenovoRegistryWarrantyFactory
    {
        public const string KeyLocation = @"SOFTWARE\Lenovo\ImController\PluginData\LenovoAuthenticationPlugin\SysInfo\";

        public LenovoWarrantyInfo CreateWarranty()
        {
            LenovoWarrantyInfo info = new LenovoWarrantyInfo();

            using (var hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
            using (var root = hklm.OpenSubKey(KeyLocation, false))
            {
                if (root == null)
                {
                    throw new ArgumentException("Key not found.");
                }

                info.Brand = root.GetValue("Brand").ToString();
                info.SubBrand = root.GetValue("Subbrand").ToString();
                info.Family = root.GetValue("Family").ToString();
                info.Serial = root.GetValue("Serial").ToString();
                info.SystemID = root.GetValue("SystemID").ToString();
            }


            return info;
        }
    }
}
