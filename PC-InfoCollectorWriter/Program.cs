
using PcInfoCollector;
using PcInfoCollector.Writer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PC_InfoCollectorWriter
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Settings setting = Settings.LoadSettings();
            WriterBase writer = new CSVWriter(setting);

            if (File.Exists("Output.txt"))
                File.Delete("Output.txt");

            using (FileStream fs = new FileStream("Output.txt", FileMode.OpenOrCreate))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    string result = writer.Write();
                    sw.WriteLine(result);
                }
            }
        }
    }
}
