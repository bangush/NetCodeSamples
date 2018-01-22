using System;
using System.Data;
using System.Data.SqlClient;
using tr.mustaliscl.metinsel;
using tr.mustaliscl.math;

namespace tr.mustaliscl.data
{
    /// <summary>
    /// SQL Manager class for Sql Operations.
    /// </summary>
    public class SqlManager : Manager
    {
        
        public SqlManager()
        {        
        }


        #region [Give DataSet With given Query]
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionstring">Connection String</param>
        /// <param name="procedurename">Query</param>
        /// <param name="parameters">Paramaters array as Param type</param>
        /// <see cref="tr.mustaliscl.data.Param"/>
        /// <returns>Returns a DataSet with given parameters</returns>
        public DataSet GetDataSetWithGivenQuery2(string ConnStr, string query, params Param[] parameters)
        {
            try
            {
                return dBManager.GetDataSetWithGivenQuery(
                    ConnectionTypes.SqlServer,
                    ConnStr, query, parameters);
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
        #endregion



        #region [Get DataSet With Given Query]
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionstring">Connection String</param>
        /// <param name="procedurename">Query</param>
        /// <param name="parameters">Paramaters array as Param type</param>
        /// <see cref="tr.mustaliscl.data.Param"/>
        /// <returns>Returns a DataSet with given parameters</returns>
        public DataSet GetDataSetWithGivenQuery(string ConnStr, string query, params Param[] parameters)
        {
            DataSet dS = new DataSet();

            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.CommandText = query;
                            cmd.CommandType = CommandType.Text;
                            cmd.Transaction = trans;

                            if (parameters != null)
                            {
                                foreach (Param p in parameters)
                                {
                                    if (Metin.GecerliMi(p.Name))
                                    {
                                        cmd.Parameters.AddWithValue(p.Name, p.Value);
                                    }
                                    else
                                        throw new Exception();
                                }
                            }
                            //conn.Open();
                            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                            adapter.Fill(dS);
                            trans.Commit();
                            cmd.Parameters.Clear();
                        } // end sql cmd
                    }
                    catch (Exception exc)
                    {
                        trans.Rollback();
                        throw exc;
                    }
                    finally
                    {
                        conn.Close();
                        SqlConnection.ClearPool(conn);
                        GC.Collect();
                    }
                }// end trans
            }
            return dS;

        }// end GetDataSetWithGivenQuery method
        #endregion



        #region [Get DataSet With Given Procedure And Parameters]
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ConnStr">Connection String</param>
        /// <param name="procedurename">Procedure Name will be used</param>
        /// <param name="parameters">Paramaters array as Param type</param>
        /// <see cref="tr.mustaliscl.data.Param"/>
        /// <returns>Returns a DataSet with given parameters</returns>
        public DataSet GetDataSetWithGivenProcedureAndParameters(string ConnStr, string procedurename, params Param[] parameters)
        {
            DataSet dS = new DataSet();


            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.CommandText = procedurename;
                            cmd.CommandType = CommandType.StoredProcedure;

                            foreach (Param p in parameters)
                            {
                                if (Metin.GecerliMi(p.Name))
                                {
                                    cmd.Parameters.AddWithValue(p.Name, p.Value);
                                }
                                else
                                    throw new Exception();
                            }

                            conn.Open();
                            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                            adapter.Fill(dS);
                            trans.Commit();
                            adapter.Dispose();
                            cmd.Parameters.Clear();
                        } // end sql cmd
                    }
                    catch (Exception exc)
                    {
                        trans.Rollback();
                        throw exc;
                    }
                    finally
                    {
                        conn.Close();
                        GC.Collect();
                    }
                }// end trans
                SqlConnection.ClearPool(conn);
            }
            return dS;

        } // end GiveDataSetWithGivenProcedureAndParameters method
        #endregion



        #region [Give DataSet With Given Procedure And Parameters]
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ConnStr">Connection String</param>
        /// <param name="procedurename">Procedure Name will be used</param>
        /// <param name="parameters">Paramaters array as Param type</param>
        /// <see cref="tr.mustaliscl.data.Param"/>
        /// <returns>Returns a DataSet with given parameters</returns>
        public DataSet GetDataSetWithGivenProcedureAndParameters2(string ConnStr, string procedurename, params Param[] parameters)
        {
            try
            {
                return dBManager.GiveDataSetWithGivenProcedureAndParameters(
                    ConnectionTypes.SqlServer, ConnStr, procedurename, parameters);
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
        #endregion



        #region [Identity of a Table]
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ConnStr">Connection String</param>
        /// <param name="tableName">Table Name</param>
        /// <returns>Returns Identity Number of Table</returns>
        public Int32 Identity(String ConnStr, String tableName)
        {
            try
            {
                return dBManager.ExecuteQueryWithGivenParameters(
                    ConnectionTypes.SqlServer,
                    ConnStr, "SELECT IDENT_CURRENT(@TableName);",
                    new Param()
                    {
                        Name = "@TableName",
                        Value = tableName
                    });
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
        #endregion

        #region [Delete From A Table]
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
            catch (Exception exc)
            {
                throw exc;
            }
            return retInt;
        }
        #endregion

    }
}
