using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using tr.mustaliscl.liste;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace tr.mustaliscl.data
{
    public class MDBC
    {
        //public  static string DboConnStr { get { return ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString; } }

        public static List<Vector<Object>> SelectQuery(String ConnStr, String tableName, params String[] columnNames)
        {
            String cmdString = String.Empty;
            if (columnNames != null)
            {
                StringBuilder sB = new StringBuilder();
                sB.Append("SELECT ");
                for (int i = 0; i < columnNames.Length - 1; i++)
                {
                    sB.Append(String.Format("{0},", columnNames[i]));
                }
                sB.Append(columnNames[columnNames.Length - 1]);
                sB.Append(String.Format(" FROM {0};", tableName));
            }
            else
            {
                cmdString = String.Format("SELCT * FROM {0};", tableName);
            }
            return selectQuery(ConnStr, cmdString);
        }
        

        public List<String> ColumnNamesofATable(String ConnStr, String tableName)
        {
            List<String> liste = new List<String>();
            try
            {
                using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(ConnStr))
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = String.Format("SELECT * FROM {0};", tableName);
                        using (SqlDataReader oku = cmd.ExecuteReader())
                        {
                            for (int i = 0; i < oku.FieldCount; i++)
                            {
                                liste.Add(oku.GetName(i));
                            }
                            oku.Close();
                        }//end oku sqldatareader                    
                    }//end sql cmd
                    conn.Close();
                }//end sqconnection conn
            }
            catch (Exception) { }
            return liste;
        }

        /*
        public List<String> columnNamesofATable(String connectionString, String tableName)
        {
            List<String> liste = new List<String>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = String.Format("SELECT * FROM {0};", tableName);
                        using (SqlDataReader oku = cmd.ExecuteReader())
                        {
                            for (int i = 0; i < oku.FieldCount; i++)
                            {
                                liste.Add(oku.GetName(i));
                            }
                            oku.Close();
                        }//end oku sqldatareader                    
                    }//end sql cmd
                    conn.Close();
                }//end sqconnection conn
            }
            catch (Exception) { }
            return liste;
        }
        */
        public static List<Vector<Object>> selectQuery(String ConnStr, String commandString)
        {
            List<Vector<Object>> liste = new List<Vector<object>>();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnStr))
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = commandString;
                        using (SqlDataReader oku = cmd.ExecuteReader())
                        {
                            if (oku.HasRows)
                            {
                                int x = oku.FieldCount;
                                while (oku.Read())
                                {
                                    using (Vector<Object> v = new Vector<Object>())
                                    {
                                        for (int i = 0; i < x; i++)
                                        {
                                            v.Ekle(oku[i]);
                                        }//end for
                                        liste.Add(v);
                                    }// end using Vector<object>
                                }//end while
                            }//end if
                            oku.Close();
                        }//end oku sqldatareader                    
                    }//end sql cmd
                    conn.Close();
                }//end sqconnection conn
            }
            catch (Exception)
            {
                liste = null;
            }

            return liste;
        }

        /*
        public static List<Vector<Object>> selectQuery(String connectionString, String commandString)
        {
            List<Vector<Object>> liste = new List<Vector<object>>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = commandString;
                        using (SqlDataReader oku = cmd.ExecuteReader())
                        {
                            if (oku.HasRows)
                            {
                                int x = oku.FieldCount;
                                while (oku.Read())
                                {
                                    using (Vector<Object> v = new Vector<Object>())
                                    {
                                        for (int i = 0; i < x; i++)
                                        {
                                            v.Ekle(oku[i]);
                                        }//end for
                                        liste.Add(v);
                                    }//end Vector<object> v
                                }//end while
                            }//end if
                            oku.Close();
                        }//end oku sqldatareader                    
                    }//end sql cmd
                    conn.Close();
                }//end sqconnection conn
            }
            catch (Exception) { }
            return liste;
        }
        */
        public static List<Vector<Object>> SelectQueryWithSelectedCols(String ConnStr, String tableName, params String[] columnNames)
        {
            try
            {
                String cmdString = String.Empty;
                if (columnNames != null)
                {
                    StringBuilder sB = new StringBuilder();
                    sB.Append("SELECT ");
                    for (int i = 0; i < columnNames.Length - 1; i++)
                    {
                        sB.Append(String.Format("{0},", columnNames[i]));
                    }
                    sB.Append(columnNames[columnNames.Length - 1]);
                    sB.Append(String.Format(" FROM {0};", tableName));
                }
                else
                {
                    cmdString = String.Format("SELCT * FROM {0};", tableName);
                }
                return selectQuery(ConnStr, cmdString);
            }
            catch (Exception) { return null; }
        }


    }
}
