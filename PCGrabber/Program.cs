using PcInfoCollector;
using PcInfoCollector.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PCGrabber
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            // Grab user settings.
            Settings settings = Settings.LoadSettings();

            // Create a collector and collect system information.
            var collector = new CollectorWMI(); 
            collector.Collect();

            // Write the log if it's not empty.
            if (collector.GetLog != "")
                WriteLog(collector.GetLog);

            // Save system information to the waiting to be processed directory.
            PCInformation.Save(settings.WaitingToBeProcessedDir, collector.Entity);

            CSVWriter wr = new CSVWriter(settings);
            wr.Write();
        }

        /// <summary>
        ///  Write the error log to a local file.
        /// </summary>
        /// <param name="errorLog"></param>
        private static void WriteLog(string errorLog)
        {
            using (FileStream fs = new FileStream("ErrorLog.txt", FileMode.OpenOrCreate))
                using (StreamWriter sw = new StreamWriter(fs))
                    sw.WriteLine(sw);
        }
    }
}
