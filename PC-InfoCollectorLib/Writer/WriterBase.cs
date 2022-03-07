using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PcInfoCollector
{
    public abstract class WriterBase
    {
        private Settings _settings;

        public WriterBase(Settings settings)
        {
            this._settings = settings;
        }

        public Settings Settings
        {
            get { return _settings; }
            set { _settings = value; }
        }

        public abstract string Write();
    }
}
