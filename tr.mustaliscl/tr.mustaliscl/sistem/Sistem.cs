using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;

namespace tr.mustaliscl.sistem
{
    public class Sistem
    {
        public static string WindowsSeriNoGetir()
        {
            string serial = string.Empty;
            try
            {
                string machine = Environment.MachineName;
                //set the scope of this search to the local machine
                ManagementScope scope = new ManagementScope(
                string.Format(@"\\{0}\root\cimv2",machine));
                //connect to the machine
                scope.Connect();

                //use a SelectQuery to tell what we're searching in
                SelectQuery searchQuery = new SelectQuery("select * from Win32_OperatingSystem");

                //set the search up
                ManagementObjectSearcher searcherObj = new ManagementObjectSearcher(scope, searchQuery);

                //get the results into a collection
                using (ManagementObjectCollection obj = searcherObj.Get())
                {
                    //loop through the collection looking for the serial
                    foreach (ManagementObject o in obj)
                        serial = o["SerialNumber"].ToString();
                }
            }
            catch
            {
                serial = string.Empty;
            }
            return serial;
        }//end WindowsSeriNoGetir()

  
        /// <summary>
        /// @example call: GetSystemInfo("Win32_Processor","Caption,Manufacturer" );
        /// </summary>
        /// <param name="strTable"></param>
        /// <param name="strProperties"></param>
        /// <returns></returns>
        public static string SistemBilgisiGetir(string strTable, string strProperties)
        {
            try
            {
                ManagementObjectSearcher mos = new ManagementObjectSearcher();
                mos.Query.QueryString = "SELECT " + strProperties + " FROM " + strTable;
                ManagementObjectCollection moc = mos.Get();
                StringBuilder sBuilder = new StringBuilder();
                foreach (ManagementObject mo in moc)
                    foreach (PropertyData pd in mo.Properties)
                    {
                        sBuilder.AppendFormat("{0},", pd.Value);
                    }
                string str = sBuilder.ToString();
                return str.Substring(0, str.Length - 1);
            }
            catch { return "Invalid table or properties"; }
        }

        public static string SistemBilgisiGetir()
        {
            try
            {
                string strTable="Win32_Processor";
                string strProperties = "Caption,Manufacturer";
                ManagementObjectSearcher mos = new ManagementObjectSearcher();
                mos.Query.QueryString = "SELECT " + strProperties + " FROM " + strTable;
                ManagementObjectCollection moc = mos.Get();
                StringBuilder sBuilder = new StringBuilder();
                foreach (ManagementObject mo in moc)
                    foreach (PropertyData pd in mo.Properties)
                    {
                        sBuilder.AppendFormat("{0},", pd.Value);
                    }
                string str = sBuilder.ToString();
                return str.Substring(0, str.Length - 1);
            }
            catch { return "Invalid table or properties"; }
        }
 

    }
}
