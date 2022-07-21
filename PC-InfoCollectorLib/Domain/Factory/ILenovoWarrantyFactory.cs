using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCInfoCollector.Domain.Factory
{
    public interface ILenovoWarrantyFactory
    {
        LenovoWarrantyInfo CreateRegistryWarranty();
    }
}
