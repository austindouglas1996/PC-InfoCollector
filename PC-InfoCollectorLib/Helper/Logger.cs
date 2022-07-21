using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCInfoCollector.Helper
{
    public static class Logger
    {
        public static void Log(Exception e)
        {
            Write(e.Message);
        }

        public static void Log(string message)
        {
            Write(message);
        }

        private static void Write(string message)
        {
            using (FileStream fs = new FileStream("ErrorLog.txt", FileMode.Append))
            using (StreamWriter sw = new StreamWriter(fs))
            {
                sw.WriteLine(message);
            }
        }
    }
}
