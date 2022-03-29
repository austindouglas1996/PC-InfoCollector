using PcInfoCollector;
using PCInfoCollector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PC_InfoCollector.LastReboot
{
    public partial class Form1 : Form
    {
        PCInformationCollection _Collection = new PCInformationCollection();

        public Form1()
        {
            InitializeComponent();

            Settings settings = Settings.LoadSettings();
            _Collection.Load(settings.WaitingToBeProcessedDir);

            // Amount of days when a PC should be noted to reboot.
            int daysOut = 15;

            // Collection of PC's out of date.
            var OutOfDate = _Collection.Where(c => IsOutOfRange(c.LastKnownCollection, daysOut))
                .OrderBy(c => c.LastKnownCollection);

            this.listView1.ItemSelectionChanged += ListView1_ItemSelectionChanged;

            foreach (var outItem in OutOfDate)
            {
                OutOfDateItem item = new OutOfDateItem(outItem.LastKnownUser, outItem.ComputerName, outItem.LastRebootTime);

                ListViewItem listitem = new ListViewItem(new string[] { item.MachineName, item.Username, item.CollectionDate.ToShortDateString() });
                listitem.Tag = item;

                this.listView1.Items.Add(listitem);
            }
        }

        private void EnterInformation(OutOfDateItem item)
        {
            this.textBox1.Text = item.Username;

            string data = File.ReadAllText("./Email.txt");
            this.richTextBox1.Text = String.Format(data, item.CollectionDate, item.MachineName);
        }

        private void ListView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.ItemIndex != -1)
            {
                OutOfDateItem item = (OutOfDateItem)e.Item.Tag;
                EnterInformation(item);
            }
        }

        private bool IsOutOfRange(DateTime collectionDate, int maxDays)
        {
            int days = (collectionDate - DateTime.Today).Days;
            bool result = Math.Abs(days) > maxDays;

            return result;
        }

        private struct OutOfDateItem
        {
            public OutOfDateItem(string username, string machineName, DateTime collectionDate)
                : this()
            {
                if (username.Contains("CROWNBATTERY\\"))
                    username = username.Replace("CROWNBATTERY\\", "");

                this.MachineName = machineName;
                this.Username = username + "@crownbattery.com";
                this.CollectionDate = collectionDate;
            }

            public string MachineName { get; set; }
            public string Username { get; set; }
            public DateTime CollectionDate { get; set; }
        }
    }
}
