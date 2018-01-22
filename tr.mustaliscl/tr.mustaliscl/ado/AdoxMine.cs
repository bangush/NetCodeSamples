using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ADOX;
namespace tr.mustaliscl.ADO
{
    public sealed class AdoxMine
    {


        /// <summary>
        /// Girilen dosyayoluna mdb uzantılı access doyası oluşturan metot.
        /// </summary>
        /// <param name="mdbfilePath">Mdb dosyasının tamyolu ve dosyanın uzantılı ismi</param>
        /// <returns>
        /// 1: Dosya oluşturuldu.
        /// 0: Dosya zaten mevcut olduğundan oluşturulmadı.
        /// -1: Yakalanamayan istisna oluştuğunda gelir.
        /// -2: System.FormatException istisnası.
        /// -3: System.ArgumentException istisnası.
        /// -4: System.Security.SecurityException istisnası.
        /// -5: System.ArgumentNullException istisnası.
        /// -6: System.UnauthorizedAccessException istisnası.
        /// -7: System.IO.PathTooLongException istisnası.
        /// -8: System.NotSupportedException istisnası.
        /// </returns>
        public static Int32 createMDBFile(string mdbfilePath)
        {
            System.Int32 retInt = 0;
            try
            {
                ADOX.CatalogClass cat = new ADOX.CatalogClass();
                System.IO.FileInfo fInfo = new System.IO.FileInfo(mdbfilePath);
                if (!fInfo.Exists)
                {
                    if (fInfo.Extension != "mdb")
                        fInfo = new System.IO.FileInfo(String.Concat(fInfo.FullName, ".mdb"));

                    cat.Create(String.Format(@"Provider=Microsoft.Jet.OLEDB.4.0;
                   Data Source={0};
                   Jet OLEDB:Engine Type=5", mdbfilePath));
                    retInt = 1;
                    cat = null;
                }
                else { retInt = 0; }
                System.GC.Collect();
            }
            catch (System.NotSupportedException) { retInt = -8; }
            catch (System.IO.PathTooLongException) { retInt = -7; }
            catch (System.UnauthorizedAccessException) { retInt = -6; }
            catch (System.ArgumentNullException) { retInt = -5; }
            catch (System.Security.SecurityException) { retInt = -4; }
            catch (System.ArgumentException) { retInt = -3; }
            catch (System.FormatException) { retInt = -2; }
            catch (System.Exception) { retInt = -1; }
            return retInt;
        }

        /// <summary>
        /// Girilen dosyayoluna mdb uzantılı access doyası oluşturan metot.
        /// </summary>
        /// <param name="mdbfilePath">Mdb dosyasının tamyolu ve dosyanın uzantılı ismi</param>
        /// <param name="password">dosyanın şifresi.</param>
        /// <returns>
        /// 1: Dosya oluşturuldu.
        /// 0: Dosya zaten mevcut olduğundan oluşturulmadı.
        /// -1: Yakalanamayan istisna oluştuğunda gelir.
        /// -2: System.FormatException istisnası.
        /// -3: System.ArgumentException istisnası.
        /// -4: System.Security.SecurityException istisnası.
        /// -5: System.ArgumentNullException istisnası.
        /// -6: System.UnauthorizedAccessException istisnası.
        /// -7: System.IO.PathTooLongException istisnası.
        /// -8: System.NotSupportedException istisnası.
        /// </returns>
        public static Int32 createMDBFile(string mdbfilePath, string password)
        {
            Int32 retInt = 0;
            try
            {
                ADOX.CatalogClass cat = new ADOX.CatalogClass();
                System.IO.FileInfo fInfo = new System.IO.FileInfo(mdbfilePath);
                if (!fInfo.Exists)
                {
                    if (fInfo.Extension != "mdb")
                        fInfo = new System.IO.FileInfo(String.Concat(fInfo.FullName, ".mdb"));

                    cat.Create(String.Format(@"Provider=Microsoft.Jet.OLEDB.4.0;
                   Data Source={0};
                   Jet OLEDB:Database Password={1};
                   Jet OLEDB:Engine Type=5", mdbfilePath, password));
                    retInt = 1;
                    cat = null;
                }
                else
                {
                    retInt = 0;
                }
                System.GC.Collect();
            }
            catch (Exception exc)
            {
                throw exc;
            }
            /*
        catch (System.NotSupportedException) { retInt = -8; }
        catch (System.IO.PathTooLongException) { retInt = -7; }
        catch (System.UnauthorizedAccessException) { retInt = -6; }
        catch (System.ArgumentNullException) { retInt = -5; }
        catch (System.Security.SecurityException) { retInt = -4; }
        catch (System.ArgumentException) { retInt = -3; }
        catch (System.FormatException) { retInt = -2; }
        catch (System.Exception) { retInt = -1; }
             */
            return retInt;
        }

        /// <summary>
        /// Girilen dosyayoluna accdb uzantılı access doyası oluşturan metot.
        /// </summary>
        /// <param name="accdbfilepath">Accdb dosyasının tamyolu ve dosyanın uzantılı ismi</param>
        /// <returns>
        /// 1: Dosya oluşturuldu.
        /// 0: Dosya zaten mevcut olduğundan oluşturulmadı.
        /// -1: Yakalanamayan istisna oluştuğunda gelir.
        /// -2: System.FormatException istisnası.
        /// -3: System.ArgumentException istisnası.
        /// -4: System.Security.SecurityException istisnası.
        /// -5: System.ArgumentNullException istisnası.
        /// -6: System.UnauthorizedAccessException istisnası.
        /// -7: System.IO.PathTooLongException istisnası.
        /// -8: System.NotSupportedException istisnası.
        /// </returns>
        public static Int32 createACCDBFile(string accdbfilepath)
        {
            System.Int32 retInt = 0;
            try
            {
                ADOX.CatalogClass cat = new ADOX.CatalogClass();
                System.IO.FileInfo fInfo = new System.IO.FileInfo(accdbfilepath);
                if (!fInfo.Exists)
                {
                    if (fInfo.Extension != "accdb")
                        fInfo = new System.IO.FileInfo(String.Concat(fInfo.FullName, ".accdb"));

                    cat.Create(String.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;
                   Data Source={0};
                   Jet OLEDB:Engine Type=5", accdbfilepath));
                    retInt = 1;
                    cat = null;
                }
                else
                {
                    retInt = 0;
                }
                GC.Collect();
            }
            catch (Exception exc)
            {
                throw exc;
            }
            /*
        catch (System.NotSupportedException) { retInt = -8; }
        catch (System.IO.PathTooLongException) { retInt = -7; }
        catch (System.UnauthorizedAccessException) { retInt = -6; }
        catch (System.ArgumentNullException) { retInt = -5; }
        catch (System.Security.SecurityException) { retInt = -4; }
        catch (System.ArgumentException) { retInt = -3; }
        catch (System.FormatException) { retInt = -2; }
        catch (System.Exception) { retInt = -1; }
             */
            return retInt;
        }

        /// <summary>
        /// Girilen dosyayoluna accdb uzantılı access doyası oluşturan metot.
        /// </summary>
        /// <param name="accdbfilepath">Accdb dosyasının tamyolu ve dosyanın uzantılı ismi</param>
        /// <param name="password">dosyanın şifresi.</param>
        /// <returns>
        /// 1: Dosya oluşturuldu.
        /// 0: Dosya zaten mevcut olduğundan oluşturulmadı.
        /// -1: Yakalanamayan istisna oluştuğunda gelir.
        /// -2: System.FormatException istisnası.
        /// -3: System.ArgumentException istisnası.
        /// -4: System.Security.SecurityException istisnası.
        /// -5: System.ArgumentNullException istisnası.
        /// -6: System.UnauthorizedAccessException istisnası.
        /// -7: System.IO.PathTooLongException istisnası.
        /// -8: System.NotSupportedException istisnası.
        /// </returns>
        public static Int32 createACCDBFile(string accdbfilepath, string password)
        {
            System.Int32 retInt = 0;
            try
            {
                ADOX.CatalogClass cat = new ADOX.CatalogClass();
                System.IO.FileInfo fInfo = new System.IO.FileInfo(accdbfilepath);
                if (!fInfo.Exists)
                {
                    if (fInfo.Extension != "accdb")
                        fInfo = new System.IO.FileInfo(String.Concat(fInfo.FullName, ".accdb"));

                    cat.Create(String.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;
                   Data Source={0};
                   Jet OLEDB:Database Password={1};
                   Jet OLEDB:Engine Type=5", accdbfilepath, password));
                    retInt = 1;
                    cat = null;
                }
                else
                {
                    retInt = 0;
                }
                GC.Collect();
            }
            catch (Exception exc)
            {
                throw exc;
            }
            /*
        catch (System.NotSupportedException) { retInt = -8; }
        catch (System.IO.PathTooLongException) { retInt = -7; }
        catch (System.UnauthorizedAccessException) { retInt = -6; }
        catch (System.ArgumentNullException) { retInt = -5; }
        catch (System.Security.SecurityException) { retInt = -4; }
        catch (System.ArgumentException) { retInt = -3; }
        catch (System.FormatException) { retInt = -2; }
        catch (System.Exception) { retInt = -1; }
             */
            return retInt;
        }

    }
}
