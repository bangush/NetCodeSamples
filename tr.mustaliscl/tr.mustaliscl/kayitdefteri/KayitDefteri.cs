using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.Collections;

namespace tr.mustaliscl.kayitdefteri
{
    public class KayitDefteri
    {
        static List<String> TumKurulmusProgramlar()
        {
            //Declare the string to hold the list:
            List<String> list = new List<String>();

            //The registry key:
            string SoftwareKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
            using (RegistryKey rk = Registry.LocalMachine.OpenSubKey(SoftwareKey))
            {
                //Let's go through the registry keys and get the info we need:
                foreach (string skName in rk.GetSubKeyNames())
                {
                    using (RegistryKey sk = rk.OpenSubKey(skName))
                    {
                        try
                        {
                            //If the key has value, continue, if not, skip it:
                            if (!(sk.GetValue("DisplayName") == null))
                            {
                                //Is the install location known?
                                if (sk.GetValue("InstallLocation") == null)
                                    list.Add(sk.GetValue("DisplayName") + " - Install path not known\n"); //Nope, not here.
                                else
                                    list.Add(sk.GetValue("DisplayName") + " - " + sk.GetValue("InstallLocation") + "\n"); //Yes, here it is...
                            }//end if(sk.GetValue)
                        }
                        catch (Exception)
                        {
                            //No, that exception is not getting away... :P
                        }
                    }//end using RegKey sk;
                }//end foreach;
            }//end RegistryKey rk;

            return list;
        }

        public static List<string> TumDosyaUzantilari()
        {
            //get into the HKEY_CLASSES_ROOT
            RegistryKey root = Registry.ClassesRoot;

            //generic list to hold all the subkey names
            List<string> subKeys = new List<string>();

            //IEnumerator for enumerating through the subkeys
            IEnumerator enums = root.GetSubKeyNames().GetEnumerator();

            //make sure we still have values
            while (enums.MoveNext())
            {
                //all registered extensions start with a period (.) so
                //we need to check for that
                if (enums.Current.ToString().StartsWith("."))
                    //valid extension so add it
                    subKeys.Add(enums.Current.ToString());
            }
            return subKeys;
        }//end Get All RegisteredFile Extensions

        public static string GetSystemDefaultBrowser()
        {
            string name = string.Empty;
            RegistryKey regKey = null;

            try
            {
                //set the registry key we want to open
                regKey = Registry.ClassesRoot.OpenSubKey(@"HTTP\shell\open\command", false);

                //get rid of the enclosing quotes
                name = regKey.GetValue(null).ToString().ToLower().Replace("" + (char)34, "");

                //check to see if the value ends with .exe (this way we can remove any command line arguments)
                if (!name.EndsWith("exe"))
                    //get rid of all command line arguments (anything after the .exe must go)
                    name = name.Substring(0, name.LastIndexOf(".exe") + 4);

            }
            catch (Exception ex)
            {
                name = string.Format(
                    @"ERROR: An exception of type: {0} occurred in method: {1} 
                        in the following module: {2}",
                    ex.GetType(), ex.TargetSite, ex.HelpLink);
            }
            finally
            {
                //check and see if the key is still open, if so
                //then close it
                if (regKey != null)
                    regKey.Close();
            }
            //return the value
            return name;

        }
    }
}
