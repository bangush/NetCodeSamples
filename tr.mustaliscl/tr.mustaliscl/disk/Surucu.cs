using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tr.mustaliscl.Disk
{
    public class Surucu
    {
        public static System.Collections.Generic.IEnumerable<System.IO.DriveInfo> CDDVDveCikarilabilirHazirSuruculer()
        {
            return System.IO.DriveInfo.GetDrives().
                Where(d => d.DriveType == System.IO.DriveType.Removable
                || d.DriveType == System.IO.DriveType.CDRom && d.IsReady);
        }

        public static System.Collections.Generic.IEnumerable<System.IO.DriveInfo> CDDVDSuruculeri()
        {
            return System.IO.DriveInfo.GetDrives().
                Where(d => d.DriveType == System.IO.DriveType.CDRom);
        }

        public static System.Collections.Generic.IEnumerable<System.IO.DriveInfo> CikarilabilirSuruculer()
        {
            return System.IO.DriveInfo.GetDrives().
                Where(d => d.DriveType == System.IO.DriveType.Removable);
        }

        /// <summary>
        /// Yüklü olan Cd ROM ların bilgisini getiren method.
        /// </summary>
        /// <returns>Yüklü olan Cd ROM ların bilgisini Sürücü İsmi:volumename;
        /// \nSürücü Seri No:volumeserialnumber; formatında getirir.</returns>
        public List<String> YukluCDROMBilgileri()
        {
            List<string> cdProperties = new List<string>();

            //create a query to searech the system for a drive type of 5 (CDROM)
            System.Management.SelectQuery query =
                new System.Management.SelectQuery("select * from win32_logicaldisk where drivetype=5");
            //use the System.Management Namespace to execute the query using the
            //ManagementObjectSearcher Object
            System.Management.ManagementObjectSearcher moSearcher =
                new System.Management.ManagementObjectSearcher(query);

            //now loop through all items returned from the query
            foreach (System.Management.ManagementObject drives in moSearcher.Get())
            {
                //check for a volumename and serial number property
                if ((drives["volumename"] != null) && (drives["volumeserialnumber"] != null))
                {
                    cdProperties.Add(String.Format("Sürücü İsmi:{0};\nSürücü Seri No:{1};", 
                        drives["volumename"].ToString(), drives["volumeserialnumber"].ToString()));                    
                }
            }
            return cdProperties;
        } 
    }
}
