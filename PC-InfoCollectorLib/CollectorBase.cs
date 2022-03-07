using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PcInfoCollector
{
    public abstract class CollectorBase<T>
        where T : IPCInformation
    {
        public abstract string GetLog { get; }
        public abstract T Entity { get; protected set; }
        public abstract void Collect();
    }
}
