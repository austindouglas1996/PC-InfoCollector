using PCInfoCollector;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCInfoCollector
{
    public class PCInformationCollection : List<PCInformation>
    {
        public void Load(string dirpath)
        {
            foreach (string file in Directory.GetFiles(dirpath))
            {
                this.Add(PCInformation.Load(file));
            }
        }

        public void Save(string dirpath)
        {
            foreach (var item in this)
            {
                PCInformation.Save(dirpath, item);
            }
        }
    }
}
