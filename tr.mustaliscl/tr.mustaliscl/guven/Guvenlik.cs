using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Principal;
using System.Windows.Forms;
using System.Management;

namespace tr.mustaliscl.guven
{
   public sealed class Guvenlik
    {
       /// <summary>
        /// Mevcut kullanıcının yönetici olup olmadığı döndüren method.
       /// </summary>
       /// <returns>Mevcut kullanıcının yönetici olup olmadığı döndürür.</returns>
       public bool kullaniciYoneticiMi()
       {
           //bool value to hold our return value
           bool isAdmin;
           try
           {
               //get the currently logged in user
               WindowsIdentity user = WindowsIdentity.GetCurrent();
               WindowsPrincipal principal = new WindowsPrincipal(user);
               isAdmin = principal.IsInRole(WindowsBuiltInRole.Administrator);
           }
           catch (Exception ex)
           {
               isAdmin = false;
               MessageBox.Show(ex.Message);
           }
           return isAdmin;
       }

       public List<string> tumKullaniciHesaplari()
       {
           List<string> accounts = null;

           try
           {
               accounts = new List<string>();
               string machinename = Environment.MachineName;
               machinename = machinename != null ? machinename : string.Empty;
               //set the scope of this search to the local machine
               ManagementScope scope = new ManagementScope(
                string.Format(@"\\{0}\root\cimv2",machinename));
               //connect to the machine
               scope.Connect();

               //use a SelectQuery to tell what we're searching in
               SelectQuery searchQuery = new SelectQuery(
                 string.Format("select * from Win32_UserAccount where Domain='{0}'",machinename));

               //set the search up
               ManagementObjectSearcher searcherObj = new ManagementObjectSearcher(scope, searchQuery);

               //loop through the collection looking for the account name
               foreach (ManagementObject obj in searcherObj.Get())
                   accounts.Add(obj["Name"].ToString());
           }
           catch(Exception)
           {
               accounts = new List<string>();
           }
           return accounts;
       } 


    }
}
