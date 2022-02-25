using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrownCollector.Domain
{
    /// <summary>
    /// Provides information on the warranty of the PC.
    /// </summary>
    public class WarrantyInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WarrantyInfo"/> class.
        /// </summary>
        public WarrantyInfo()
        {
            Provider = "NoProvider";
            PurchaseDate = DateTime.MinValue;
            ExpirationDate = DateTime.MinValue;
            IsExpired = false;
        }

        /// <summary>
        /// The provider that handles the warranty information. Normally the manufacture.
        /// </summary>
        public string Provider { get; set; }

        /// <summary>
        /// Date the device was purchased on.
        /// </summary>
        public DateTime PurchaseDate { get; set; }

        /// <summary>
        /// Date the device warranty expires on.
        /// </summary>
        public DateTime ExpirationDate { get; set; }

        /// <summary>
        /// Gets a value indicating whether the warranty is expired.
        /// </summary>
        public bool IsExpired { get; set; }
    }
}
