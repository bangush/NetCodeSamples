using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;


namespace tr.mustaliscl.Derle
{
    public class CalisanKlasor
    {
        /// <summary>
        /// @returns System.Environment.CurrentDirectory ile geri döndürülen yoldur.
        /// </summary>
       public static string MevcutKlasor() {
            try
            {
                return System.Environment.CurrentDirectory;
            }
            catch (Exception) {
                return String.Empty;
            }

        }
        
        /// <summary>
        /// @returns System.IO.Directory.GetCurrentDirectory() ile geri döndürülen yoldur.
        /// </summary>
        public static string MevcutKlasor2(){
            try
            {
                return System.IO.Directory.GetCurrentDirectory();
            }
            catch (Exception) {
                return String.Empty;
            }
            }
        
        /// <summary>
        /// @returns System.AppDomain.CurrentDomain.BaseDirectory ile geri döndürülen yoldur.
        /// </summary>
        public static string BazKlasor()
        {
            try
            {
                return System.AppDomain.CurrentDomain.BaseDirectory;
            }
            catch (Exception) {
                return String.Empty;
            }
        }

        /// <summary>
        /// @returns System.Windows.Forms.Application.StartupPath ile geri döndürülen yoldur.
        /// </summary>
        public static string CalismaKlasoru()
        {
            try
            {
                return System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.StartupPath);
            }
            catch (Exception)
            {
                return String.Empty;
            } 
        }

        /// <summary>
        /// @returns System.Windows.Forms.Application.ExecutablePath ile geri döndürülen yoldur.
        /// </summary>
        public static string CalisanDosya(){

            try
            {
                return System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
            }
            catch (Exception)
            {
                return String.Empty;
            }
        }
    }
}
