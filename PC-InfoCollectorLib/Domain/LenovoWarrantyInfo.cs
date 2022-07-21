using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PCInfoCollector.Domain
{
    public class LenovoWarrantyInfo : WarrantyInfo
    {
        public LenovoWarrantyInfo()
        {
            this.Provider = "Lenovo";
            this.Brand = "";
            this.SubBrand = "";
            this.Family = "";
            this.Serial = "";
            this.SystemID = "";
        }

        public string Brand { get; set; }
        public string SubBrand { get; set; }
        public string Family { get; set; }
        public string Serial { get; set; }
        public string SystemID { get; set; }
    }
}
