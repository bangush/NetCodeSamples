using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.DirectoryServices;
using System.Net.NetworkInformation;

namespace tr.mustaliscl.net
{
    public class InternetCS
    {
        public static Boolean AgErisilirMi() {
            return System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();
        }

        public static List<string> AgdakiTumBilgisayarlar()
        {
            List<string> list = new List<string>();
            using (DirectoryEntry root = new DirectoryEntry("WinNT:"))
            {
                foreach (DirectoryEntry computers in root.Children)
                {
                    foreach (DirectoryEntry computer in computers.Children)
                    {
                        if ((computer.Name != "Schema"))
                        {
                            list.Add(computer.Name);
                        }
                    }
                }
            }
            return list;
        }

        public static PhysicalAddress GetMacAddress()
        {
            NetworkInterface ni = GetNetworkInterface();
            if (ni == null)
                return null;
            return ni.GetPhysicalAddress();
        }

        private static NetworkInterface GetNetworkInterface()
        {
            IPGlobalProperties computerProperties = IPGlobalProperties.GetIPGlobalProperties();
            if (computerProperties == null)
                return null;

            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            if (nics == null || nics.Length < 1)
                return null;

            NetworkInterface best = null;
            foreach (NetworkInterface adapter in nics)
            {
                if (adapter.NetworkInterfaceType == NetworkInterfaceType.Loopback || adapter.NetworkInterfaceType == NetworkInterfaceType.Unknown)
                    continue;
                if (!adapter.Supports(NetworkInterfaceComponent.IPv4))
                    continue;
                if (best == null)
                    best = adapter;
                if (adapter.OperationalStatus != OperationalStatus.Up)
                    continue;

                // A computer could have several adapters (more than one network card)
                // here but just return the first one for now...
                return adapter;
            }
            return best;
        }

    }
}
