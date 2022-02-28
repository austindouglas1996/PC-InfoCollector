using CrownCollector.Domain;
using CrownCollector.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace CrownCollector
{
    /// <summary>
    /// Collects system information based on <see cref="https://docs.microsoft.com/en-us/windows/win32/cimwin32prov/win32-provider"/> instances.
    /// </summary>
    public class CollectorWMI : CollectorBase<PCInformation>
    {
        /*
         * Queries to access certain information.
         */
        private static readonly string Computer = "select * from Win32_ComputerSystem";
        private static readonly string BaseBoard = "select * from Win32_BaseBoard";
        private static readonly string Disks = "select * from Win32_DiskDrive";
        private static readonly string Memory = "select * from Win32_PhysicalMemory";
        private static readonly string Processor = "select * from Win32_Processor";
        private static readonly string OperatingSystem = "select * from Win32_OperatingSystem";
        private static readonly string Video = "select * from Win32_VideoController";

        /// <summary>
        /// Don't allow the user to grab <see cref="Entity"/> if <see cref="Collect"/> has not be called yet.
        /// </summary>
        private bool _CollectCalled = false;

        /// <summary>
        /// Holds an array of system network interfaces.
        /// </summary>
        private NetworkInterface[] nics;

        private PCInformation _Entity;
        private StringBuilder _Log = new StringBuilder();

        /// <summary>
        /// Initializes a new instance of the <see cref="CollectorWMI"/> class.
        /// </summary>
        public CollectorWMI()
        {
            nics = NetworkInterface.GetAllNetworkInterfaces();
        }

        /// <summary>
        /// Returns the errors occured when grabbing system information.
        /// </summary>
        public override string GetLog
        {
            get { return _Log.ToString(); }
        }

        /// <summary>
        /// Returns the created entity.
        /// </summary>
        public override PCInformation Entity
        {
            get
            {
                if (_CollectCalled == false)
                    throw new InvalidOperationException("Collect() must be called first.");
                return _Entity;
            }

            protected set
            {
                _Entity = value;
            }
        }

        /// <summary>
        /// Collect system information so <see cref="Entity"/> can be called.
        /// </summary>
        public override void Collect()
        {
            this._CollectCalled = true;

            Entity = new PCInformation();

            // Collect default values that don't require a ManagementCollection.
            Entity.ComputerName = GetAndFormatName();
            Entity.IPAddress = GetIPAddress();
            Entity.MACAddress = BitConverter.ToString(nics[0].GetPhysicalAddress().GetAddressBytes());

            // Get manufacture.
            foreach (ManagementObject mo in GetManagementInfo(CollectorWMI.Computer))
            {
                Entity.LastKnownUser = TryGetManagementValue<string>("Username", mo, ref _Log);
                Entity.Manufacture = TryGetManagementValue<string>("Manufacturer", mo, ref _Log);
            }

            // Get manufacture.
            foreach (ManagementObject bb in GetManagementInfo(CollectorWMI.BaseBoard))
                Entity.MotherboardModel = TryGetManagementValue<string>("Model", bb, ref _Log);

            // Get disk information.
            foreach (ManagementObject mo in GetManagementInfo(CollectorWMI.Disks))
            {
                Disk disk = new Disk();
                disk.Caption = TryGetManagementValue<string>("Caption", mo, ref _Log);
                disk.SerialNumber = TryGetManagementValue<string>("SerialNumber", mo, ref _Log);
                disk.TotalSpace = SizeHelper.FormatBytes((long)Convert.ToUInt64(mo["Size"].ToString()));
                Entity.Disks.Add(disk);
            }

            // Get memory information.
            Entity.MemoryInGB = GetMemory();

            // Get processor information.
            foreach (ManagementObject mo in GetManagementInfo(CollectorWMI.Processor))
                Entity.Processor = TryGetManagementValue<string>("Name", mo, ref _Log);

            // Get operating system.
            foreach (ManagementObject mo in GetManagementInfo(CollectorWMI.OperatingSystem))
                Entity.OSName = TryGetManagementValue<string>("Name", mo, ref _Log);

            // Get graphics card information.
            foreach (ManagementObject mo in GetManagementInfo(CollectorWMI.Video))
                Entity.GraphicsCard = TryGetManagementValue<string>("Name", mo, ref _Log);
        }

        /// <summary>
        /// Get the machine name then format in a way that does not include the installation path.
        /// </summary>
        /// <returns></returns>
        protected string GetAndFormatName()
        {
            string m = Environment.MachineName;

            // Windows 10 lists the installation partition along with the name.
            bool ListsPath = m.Contains("|");
            if (ListsPath)
            {
                int path = m.IndexOf("|");
                m = m.Substring(0, path - 1);
            }

            return m;
        }

        /// <summary>
        /// Get system default network IP address.
        /// </summary>
        /// <returns></returns>
        protected string GetIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }

            return "IP Not Found.";
        }

        /// <summary>
        /// Get system properties.
        /// </summary>
        /// <returns></returns>
        protected string GetMemory()
        {
            UInt64 total = 0;
            foreach (var ram in GetManagementInfo(CollectorWMI.Memory))
            {
                total += (UInt64)ram.GetPropertyValue("Capacity");
            }
            return total / 1073741824 + "GB";
        }

        /// <summary>
        /// Creates a new instance of <see cref="ManagementObjectSearcher"/> based on a query and returns the collection.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        protected virtual ManagementObjectCollection GetManagementInfo(string query)
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);
            return searcher.Get();
        }

        /// <summary>
        /// Tries grabbing a property value from a <see cref="ManagementObject"/>. If the property returns null the default
        /// value of <see cref="T"/> is returned along with the error message when trying to grab the value.
        /// </summary>
        /// <typeparam name="T">Value to be returned as</typeparam>
        /// <param name="prop">Property to grab/</param>
        /// <param name="obj">ManagementObject to reference</param>
        /// <param name="errorLog">ErrorLog to write to.</param>
        /// <returns></returns>
        protected virtual T TryGetManagementValue<T>(string prop, ManagementObject obj, ref StringBuilder errorLog)
        {
            try
            {
                return (T)Convert.ChangeType(obj[prop], typeof(T));
            }
            catch (Exception ex)
            {
                errorLog.AppendLine(ex.Message);
                errorLog.AppendLine(ex.StackTrace);
                return default(T);
            }
        }
    }
}
