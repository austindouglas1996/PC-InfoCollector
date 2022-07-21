using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCInfoCollector.Domain.Factory
{
    public class LenovoWarrantyFactory : ILenovoWarrantyFactory
    {
        public LenovoWarrantyInfo CreateRegistryWarranty()
        {
            return new LenovoRegistryWarrantyFactory().CreateWarranty();
        }
    }
}
