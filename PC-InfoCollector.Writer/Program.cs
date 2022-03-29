﻿
using PcInfoCollector;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCInfoCollector.Writer
{
    internal class Program
    {
        public const string OutputName = "WriterOutput.csv";

        public static void Main(string[] args)
        {
            Settings setting = Settings.LoadSettings();
            CSVWriter writer = new CSVWriter(setting);

            if (File.Exists(OutputName))
                File.Delete(OutputName);

            using (FileStream fs = new FileStream(OutputName, FileMode.OpenOrCreate))
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
