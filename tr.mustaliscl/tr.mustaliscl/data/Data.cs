using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using tr.mustaliscl.data;
using System.Data.SqlClient;
using tr.mustaliscl.math;
using System.ComponentModel;
using tr.mustaliscl.metinsel;

namespace tr.mustaliscl.data
{
    public class Data
    {
        public static DataTable ToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }

        public static IList<T> DataTableToList<T>(DataTable datatable) where T : new()
        {
            try
            {
                /*
            if(properties.Count!=datatable.Columns.Count)
                throw new Exception("Column Count andProperties Count must be Equal.";            
                 */
                List<T> liste = new List<T>();
                foreach (DataRow row in datatable.Rows)
                {
                    T item = new T();
                    PropertyDescriptorCollection properties =
                       TypeDescriptor.GetProperties(typeof(T));
                    foreach (DataColumn col in datatable.Columns)
                    {
                        object obj = row[col.ColumnName];
                        if (null != obj && obj != DBNull.Value)
                        {
                            PropertyDescriptor prop = properties.Find(col.ColumnName, true);
                            prop.SetValue(item, obj);
                        }

                    }
                    liste.Add(item);
                }
                return liste;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public DataSet CreateDataSet<T>(List<T> list)
        {
            //list is nothing or has nothing, return nothing (or add exception handling)
            if (list == null || list.Count == 0) { return null; }

            //get the type of the first obj in the list
            var obj = list[0].GetType();

            //now grab all properties
            var properties = obj.GetProperties();

            //make sure the obj has properties, return nothing (or add exception handling)
            if (properties.Length == 0) { return null; }

            //it does so create the dataset and table
            var dataSet = new DataSet();
            var dataTable = new DataTable();

            //now build the columns from the properties
            var columns = new DataColumn[properties.Length];
            for (int i = 0; i < properties.Length; i++)
            {
                columns[i] = new DataColumn(properties[i].Name, properties[i].PropertyType);
            }

            //add columns to table
            dataTable.Columns.AddRange(columns);

            //now add the list values to the table
            foreach (var item in list)
            {
                //create a new row from table
                var dataRow = dataTable.NewRow();

                //now we have to iterate thru each property of the item and retrieve it's value for the corresponding row's cell
                var itemProperties = item.GetType().GetProperties();

                for (int i = 0; i < itemProperties.Length; i++)
                {
                    dataRow[i] = itemProperties[i].GetValue(item, null) ?? DBNull.Value;
                }

                //now add the populated row to the table
                dataTable.Rows.Add(dataRow);
            }

            //add table to dataset
            dataSet.Tables.Add(dataTable);

            //return dataset
            return dataSet;
        }

        public static DataSet Procedure2DataSet(String ConnStr, String procedureName, params Param[] parameters)
        {
            DataSet dS = new DataSet();
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = procedureName;

                    if (parameters != null && parameters.Length > 0)
                    {
                        for (int i = 0; i < parameters.Length; i++)
                        {
                            if (!string.IsNullOrEmpty(parameters[i].Name))
                            {
                                cmd.Parameters.Add(
                                  parameters[i].Name, parameters[i].Tip).Value =
                                  parameters[i].Value;
                            }
                        }// end for
                    }//end if
                    conn.Open();
                    SqlDataAdapter sqDa = new SqlDataAdapter();
                    sqDa.SelectCommand = cmd;
                    sqDa.Fill(dS);
                }//end sql command
                conn.Close(); SqlConnection.ClearPool(conn);
            }//end connection
            return dS;
        }

        public static DataTable Procedure2DataTable(String ConnStr, String procedureName, params Param[] parameters)
        {
            using (DataSet dS = Procedure2DataSet(ConnStr, procedureName,
                parameters))
            {
                DataTable dT = null;
                try
                {
                    dT = dS.Tables[0];
                }
                catch (Exception)
                {
                    dT = null;
                }
                return dT;
            }

        }



        protected static String SinifAdi(Object o)
        {
            return o.GetType().Name;
        }

        protected static Int32 Identity(String ConnStr, String tableName)
        {
            int retInt = 0;
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT IDENT_CURRENT(@TableName)";
                    cmd.Parameters.AddWithValue("@TableName", tableName);
                    conn.Open();
                    Object o = cmd.ExecuteScalar();
                    retInt = o.ToString().ToInt();
                    cmd.Parameters.Clear();
                    conn.Close();
                }//end sql command
                SqlConnection.ClearPool(conn);
            }//end sql connection
            return retInt;
        }

        /// <summary>
        /// Tablo ismini kullanıp son Id numarasını döndürem fonksiyon.
        /// </summary>
        /// <param name="ConnStr">Bağlantı metni</param>
        /// <param name="table">Tablo isminin olduğu sınıfı #new Tablo()# şeklinde girmeniz gerekmektedir...</param>
        /// <returns>/// Tablo ismini kullanıp son Id numarasını döndürür.</returns>
        public static Int32 GetIdentity(String ConnStr, Object table)
        {
            return Identity(ConnStr, SinifAdi(table));
        }


        /// <summary>
        /// Tek tabloda silme işlemini yapan kod.
        /// </summary>
        /// <param name="ConnStr">Bağlantı metni</param>
        /// <param name="tablo_adi">tablo adı</param>
        /// <param name="kolon_adi">kolon adı</param>
        /// <param name="deger">sorgulanan değer</param>
        /// <returns>silinen satır sayısı döner, İstisna oluşursa
        /// ArgumentNullException da -4,
        /// FormatException da -3,
        /// SqlException da -2,
        /// InvalidOperationException da -1 dönecektir.
        /// </returns>
        public static int Sil(String ConnStr, string tablo_adi, string kolon_adi, object deger)
        {
            Int32 retInt = 0;
            try
            {
                using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(ConnStr))
                {
                    using (System.Data.SqlClient.SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = String.Format("DELETE FROM {0} WHERE {1}=@deger;", tablo_adi, kolon_adi);
                        cmd.Parameters.AddWithValue("@deger", deger);
                        conn.Open();
                        retInt = cmd.ExecuteNonQuery();
                        conn.Close();
                        cmd.Parameters.Clear();
                    }
                    SqlConnection.ClearPool(conn);
                }
            }
            catch (System.ArgumentNullException) { retInt = -4; }
            catch (System.FormatException) { retInt = -3; }
            catch (System.Data.SqlClient.SqlException) { retInt = -2; }
            catch (System.InvalidOperationException) { retInt = -1; }
            return retInt;
        }



        public static DataSet Query2DataSet(String ConnStr, String query, params Param[] parameters)
        {
            DataSet dS = new DataSet();
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = query;

                    if (parameters != null && parameters.Length > 0)
                    {
                        for (int i = 0; i < parameters.Length; i++)
                        {
                            if (!string.IsNullOrEmpty(parameters[i].Name))
                            {
                                cmd.Parameters.Add(
                                  parameters[i].Name, parameters[i].Tip).Value =
                                  parameters[i].Value;
                            }
                        }// end for
                    }//end if
                    conn.Open();
                    SqlDataAdapter sqDa = new SqlDataAdapter();
                    sqDa.SelectCommand = cmd;
                    sqDa.Fill(dS);
                }//end sql command
                conn.Close(); SqlConnection.ClearPool(conn);
            }//end connection
            return dS;
        }

        public static DataTable Query2DataTable(String ConnStr, String query, params Param[] parameters)
        {
            using (DataSet dS = Query2DataSet(ConnStr, query,
                parameters))
            {
                DataTable dT = null;
                try
                {
                    dT = dS.Tables[0];
                }
                catch (Exception)
                {
                    dT = null;
                }
                return dT;
            }

        }
    }
}
