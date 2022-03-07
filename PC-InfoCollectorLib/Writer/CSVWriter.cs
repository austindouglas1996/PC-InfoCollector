 using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PcInfoCollector.Writer
{
    public class CSVWriter
    {
        private Settings Settings;

        public CSVWriter(Settings settings)
        {
            this.Settings = settings;
        }

        public string Write()
        {
            List<IPCInformation> PCs = new List<IPCInformation>();
            foreach (var file in Directory.GetFiles(Settings.WaitingToBeProcessedDir))
            {
                PCs.Add(PCInformation.Load(file));
            }

            // Make it look nice.
            PCs.OrderBy(p => p.IPAddress);

            StringBuilder sb = new StringBuilder();
            foreach (var PC in PCs)
            {
                sb.Append(ToCSV(PC));
            }

            return sb.ToString();
        }

        public static string ToCSV(IPCInformation pcInfo)
        {
            Dictionary<string, string[]> props = new Dictionary<string, string[]>();
            props.Add("Computer Name", new string[] { pcInfo.ComputerName });
            props.Add("Last Known User", new string[] { pcInfo.LastKnownUser });
            props.Add("Manufacture", new string[] { pcInfo.Manufacture });
            props.Add("OS Name", new string[] { pcInfo.OSName });
            props.Add("IPv4 Address", new string[] { pcInfo.IPAddress });
            props.Add("MAC Address", new string[] { pcInfo.MACAddress });
            props.Add("Processor", new string[] { pcInfo.Processor });
            props.Add("Motherboard Model", new string[] { pcInfo.MotherboardModel });
            props.Add("Memory in GB", new string[] { pcInfo.MemoryInGB });
            props.Add("Graphics Card", new string[] { pcInfo.GraphicsCard });

            List<string> diskCaptions = new List<string>();
            List<string> diskSerials = new List<string>();
            List<string> diskSpace = new List<string>();

            pcInfo.Disks.ForEach(disk => diskCaptions.Add(disk.Caption));
            pcInfo.Disks.ForEach(disk => diskSerials.Add(disk.SerialNumber));
            pcInfo.Disks.ForEach(disk => diskSpace.Add(disk.TotalSpace));

            props.Add("Disks Caption", diskCaptions.ToArray());
            props.Add("Disks Serial Number", diskSerials.ToArray());
            props.Add("Disks Total Space", diskSpace.ToArray());

            string result = CommaString(props);
            return result;
        }

        private static string CommaString(Dictionary<string, string[]> val)
        {
            List<Tuple<int, int, string>> processed = new List<Tuple<int, int, string>>();
            StringBuilder sb = new StringBuilder();

            int maxColumns = val.Keys.ToList().Count();
            int maxRows = 0;

            // Grab the length of the longest array.
            foreach (string[] vals in val.Values)
                if (vals.Length > maxRows)
                    maxRows = vals.Length;

            // Keys (Columns)
            for (int c = 0; c < maxColumns; c++)
            {
                // Grab the string of values
                string[] data = val.Values.ToList()[c];

                // Values (Rows)
                for (int v = 0; v < maxRows; v++)
                {
                    string dataValue = "";

                    if (data.Length > v)
                        dataValue = data[v];

                    // Make sure the data is not empty.
                    if (string.IsNullOrEmpty(dataValue))
                        dataValue = "";

                    // Clean the data of any issues.
                    dataValue = dataValue.Replace(",", "");

                    processed.Add(new Tuple<int, int, string>(c, v, dataValue));
                }
            }

            // Process the date into a CSV format.
            for (int v = 0; v < maxRows; v++)
            {
                string rowData = "";

                for (int c = 0; c < maxColumns; c++)
                {
                    // Grab all the data from this row.
                    var selectedRow = processed.FirstOrDefault(p => p.Item1 == c && p.Item2 == v);

                    if (selectedRow == null)
                        rowData += "";
                    else
                    {
                        // Write the row data or write nothing for an empty row.
                        rowData += string.IsNullOrWhiteSpace(selectedRow.Item3) ? "" : selectedRow.Item3;
                    }

                    // Append the next line.
                    if (c < maxColumns -1)
                        rowData += ",";
                }

                sb.AppendLine(rowData);
            }

            return sb.ToString();
        }
    }
}
