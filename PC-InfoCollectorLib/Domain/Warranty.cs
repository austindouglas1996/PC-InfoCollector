using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrownCollector.Domain
{
    public class WarrantyInfo
    {
        public WarrantyInfo()
        {
            Provider = "";
            ProviderInstance = "NoInstance";
            PurchaseDate = DateTime.MinValue;
            ExpirationDate = DateTime.MinValue;
            IsExpired = false;
        }

        public string Provider { get; set; }
        public string ProviderInstance { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsExpired { get; set; }
    }
}
