using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using Oracle.DataAccess.Client;
using tr.mustaliscl.metinsel;

namespace tr.mustaliscl.data
{
    public class DbManager
    {

        #region [Get DataSet With Given Query]
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionstring">Connection String</param>
        /// <param name="procedurename">Query</param>
        /// <param name="parameters">Paramaters array as Param type</param>
        /// <see cref="tr.mustaliscl.data.Param"/>
        /// <returns>Returns a DataSet with given parameters</returns>
        public DataSet GetDataSetWithGivenQuery(ConnectionTypes conType, string ConnStr, string query, params Param[] parameters)
        {
            DataSet dS = new DataSet();

            using (IDbConnection dbConn = GetConnection(conType))
            {
                dbConn.ConnectionString = ConnStr;
                dbConn.Open();
                using (IDbTransaction dbTrans = dbConn.BeginTransaction())
                {
                    try
                    {
                        using (IDbCommand dbCmd = dbConn.CreateCommand())
                        {
                            dbCmd.Transaction = dbTrans;
                            dbCmd.CommandText = query;
                            dbCmd.CommandType = CommandType.Text;
                            if (parameters != null)
                            {
                                foreach (Param p in parameters)
                                {
                                    if (!Metin.GecerliMi(p.Name))
                                        throw new Exception("Parameter name could not be empty or null.");

                                    IDataParameter dbParameter = GetParameter(conType);
                                    dbParameter.ParameterName = p.Name;
                                    dbParameter.Value = p.Value;
                                    dbCmd.Parameters.Add(dbParameter);
                                }
                            }
                           // dbConn.Open();

                            IDataAdapter dbAdapter = GetDataAdapter(conType,dbCmd);
                            
                            dbAdapter.Fill(dS);

                            dbTrans.Commit();

                            dbCmd.Parameters.Clear();

                        }// end dbcmd
                    }// end try
                    catch (Exception exc)
                    {
                        dbTrans.Rollback();
                        throw exc;
                    }
                    finally
                    {
                        dbConn.Close();
                        GC.Collect();
                    }
                } // end dbTrans
            } // end dbConn

            return dS;
        }
        #endregion


        #region [Get DataSet With Given Procedure And Parameters]
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionstring">Connection String</param>
        /// <param name="procedurename">Query</param>
        /// <param name="parameters">Paramaters array as Param type</param>
        /// <see cref="tr.mustaliscl.data.Param"/>
        /// <returns>Returns a DataSet with given parameters</returns>
        public DataSet GiveDataSetWithGivenProcedureAndParameters(
            ConnectionTypes conType,
            string ConnStr, string procedurename, params Param[] parameters)
        {
            DataSet dS = new DataSet();

            using (IDbConnection dbConn = GetConnection(conType))
            {
                dbConn.ConnectionString = ConnStr;
                using (IDbTransaction dbTrans = dbConn.BeginTransaction())
                {
                    try
                    {
                        using (IDbCommand dbCmd = dbConn.CreateCommand())
                        {
                            dbCmd.Transaction = dbTrans;
                            dbCmd.CommandText = procedurename;
                            dbCmd.CommandType = CommandType.StoredProcedure;
                            foreach (Param p in parameters)
                            {
                                if (!Metin.GecerliMi(p.Name))
                                    throw new Exception("Parameter name could not be empty or null.");

                                IDataParameter dbParameter =
                                    GetParameter(conType);

                                dbParameter.ParameterName = p.Name;
                                dbParameter.Value = p.Value;
                                dbCmd.Parameters.Add(dbParameter);
                            }

                            dbConn.Open();

                            IDataAdapter dbAdapter = GetDataAdapter(conType,dbCmd);
                            dbAdapter.Fill(dS);

                            dbTrans.Commit();

                            dbCmd.Parameters.Clear();

                        }// end dbcmd
                    }// end try
                    catch (Exception exc)
                    {
                        dbTrans.Rollback();
                        throw exc;
                    }
                    finally
                    {
                        dbConn.Close();
                        GC.Collect();
                    }
                } // end dbTrans
            } // end dbConn

            return dS;
        }
        #endregion



        #region [Execute Query With Given Parameters]
        public Int32 ExecuteQueryWithGivenParameters(ConnectionTypes conType,
           string ConnStr, string query, params Param[] parameters)
        {
            Int32 willBeReturn;

            using (IDbConnection dbConn = GetConnection(conType))
            {
                dbConn.ConnectionString = ConnStr;
                using (IDbTransaction dbTrans = dbConn.BeginTransaction())
                {
                    try
                    {
                        using (IDbCommand dbCmd = dbConn.CreateCommand())
                        {
                            dbCmd.Transaction = dbTrans;
                            dbCmd.CommandText = query;
                            dbCmd.CommandType = CommandType.Text;
                            foreach (Param p in parameters)
                            {
                                if (!Metin.GecerliMi(p.Name))
                                    throw new Exception("Parameter name could not be empty or null.");

                                IDataParameter dbParameter = GetParameter(conType);
                                dbParameter.ParameterName = p.Name;
                                dbParameter.Value = p.Value;
                                dbCmd.Parameters.Add(dbParameter);
                            }

                            dbConn.Open();

                            willBeReturn = dbCmd.ExecuteNonQuery();

                            dbTrans.Commit();

                            dbCmd.Parameters.Clear();
                            dbConn.Close();
                        }// end dbcmd
                    }// end try
                    catch (Exception exc)
                    {
                        dbTrans.Rollback();
                        throw exc;
                    }
                    finally
                    {
                        dbConn.Close();
                        GC.Collect();
                    }
                } // end dbTrans
            } // end dbConn

            return willBeReturn;
        }
        #endregion



        #region [ExecuteProcedureWithGivenParameters]
        public Int32 ExecuteProcedureWithGivenParameters(ConnectionTypes conType,
            string ConnStr, string procedurename, params Param[] parameters)
        {
            Int32 willBeReturn;

            using (IDbConnection dbConn = GetConnection(conType))
            {
                dbConn.ConnectionString = ConnStr;
                using (IDbTransaction dbTrans = dbConn.BeginTransaction())
                {
                    try
                    {
                        using (IDbCommand dbCmd = dbConn.CreateCommand())
                        {
                            dbCmd.Transaction = dbTrans;
                            dbCmd.CommandText = procedurename;
                            dbCmd.CommandType = CommandType.StoredProcedure;
                            foreach (Param p in parameters)
                            {
                                if (!Metin.GecerliMi(p.Name))
                                    throw new Exception("Parameter name could not be empty or null.");

                                IDataParameter dbParameter = GetParameter(conType);
                                dbParameter.ParameterName = p.Name;
                                dbParameter.Value = p.Value;
                                dbCmd.Parameters.Add(dbParameter);
                            }

                            dbConn.Open();

                            willBeReturn = dbCmd.ExecuteNonQuery();

                            dbTrans.Commit();

                            dbCmd.Parameters.Clear();

                        }// end dbcmd
                    }// end try
                    catch (Exception exc)
                    {
                        dbTrans.Rollback();
                        throw exc;
                    }
                    finally
                    {
                        dbConn.Close();
                        GC.Collect();
                    }
                } // end dbTrans
            } // end dbConn

            return willBeReturn;
        }

        #endregion


        #region [Get DataSet with Connection]

        public DataSet GetDataSetWithConnection(IDbConnection Conn,
            CommandType cmdType, string QueryOrProcedure, params Param[] parameters)
        {
            DataSet dS = new DataSet();

            if (Conn.State != ConnectionState.Open)
                Conn.Open();

            using (IDbTransaction dbTrans = Conn.BeginTransaction())
            {

                try
                {
                    using (IDbCommand dbCmd = Conn.CreateCommand())
                    {
                        dbCmd.Transaction = dbTrans;
                        dbCmd.CommandText = QueryOrProcedure;
                        dbCmd.CommandType = cmdType;
                        foreach (Param p in parameters)
                        {
                            if (!Metin.GecerliMi(p.Name))
                                throw new Exception("Parameter name could not be empty or null.");

                            IDataParameter dbParameter =
                                GetParameter(Conn);

                            dbParameter.ParameterName = p.Name;
                            dbParameter.Value = p.Value;
                            dbCmd.Parameters.Add(dbParameter);
                        }

                        

                        IDataAdapter dbAdapter = GetDataAdapter(Conn);
                        dbAdapter.Fill(dS);

                        dbTrans.Commit();

                        dbCmd.Parameters.Clear();

                    }// end dbcmd
                }// end try
                catch (Exception exc)
                {
                    dbTrans.Rollback();
                    throw exc;
                }
                finally
                {
                    Conn.Close();
                    GC.Collect();
                }
            } // end dbTrans


            return dS;
        }
        #endregion



        #region [Get Connection with Connection Type]
        /// <summary>
        /// 
        /// </summary>
        /// <param name="conType">Connection Type</param>
        /// <returns>Returns a IDbConnection object instance</returns>
        private static IDbConnection GetConnection(ConnectionTypes conType)
        {
            switch (conType)
            {
                case ConnectionTypes.Access:
                    return new OleDbConnection();
                case ConnectionTypes.FireBird:
                    throw new NotSupportedException("FireBirdSQL sürücüsü eklenmedi.");
                case ConnectionTypes.NoSQL:
                    throw new NotSupportedException("NoSQL sürücüsü eklenmedi.");
                case ConnectionTypes.OpenAccess:
                    throw new NotSupportedException("Open Access sürücüsü eklenmedi.");
                case ConnectionTypes.Oracle:
                    return new OracleConnection();
                case ConnectionTypes.SqlExpress:
                    return new SqlConnection();
                case ConnectionTypes.SQLite:
                    throw new NotSupportedException("SQLite sürücüsü eklenmedi.");
                case ConnectionTypes.SqlServer:
                    return new SqlConnection();
                case ConnectionTypes.MySQL:
                    throw new NotSupportedException("MySQL sürücüsü eklenmedi.");
                default:
                    return new SqlConnection();
            }
        }
        #endregion



        #region [Get Data Parameter with Connection]
        /// <summary>
        /// 
        /// </summary>
        /// <param name="conType">Connection Type</param>
        /// <returns>Returns a IDataParameter object instance</returns>
        private static IDataParameter GetParameter(ConnectionTypes conType)
        {
            switch (conType)
            {
                case ConnectionTypes.Access:
                    return new OleDbParameter();
                case ConnectionTypes.FireBird:
                    throw new NotSupportedException("FireBirdSQL sürücüsü eklenmedi.");
                case ConnectionTypes.NoSQL:
                    throw new NotSupportedException("NoSQL sürücüsü eklenmedi.");
                case ConnectionTypes.OpenAccess:
                    throw new NotSupportedException("Open Access sürücüsü eklenmedi.");
                case ConnectionTypes.Oracle:
                    return new OracleParameter();
                case ConnectionTypes.SqlExpress:
                    return new SqlParameter();
                case ConnectionTypes.SQLite:
                    throw new NotSupportedException("SQLite sürücüsü eklenmedi.");
                case ConnectionTypes.SqlServer:
                    return new SqlParameter();
                case ConnectionTypes.MySQL:
                    throw new NotSupportedException("MySQL sürücüsü eklenmedi.");
                default:
                    return new SqlParameter();
            }
        }
        #endregion

        #region [Get Data Parameter with Connection]
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Conn">Connection</param>
        /// <returns>Returns a IDataParameter object instance</returns>
        private static IDataParameter GetParameter(IDbConnection Conn)
        {
            Type type = Conn.GetType();

            if (Object.ReferenceEquals(type, typeof(SqlConnection)))
                return new SqlParameter();

            if (Object.ReferenceEquals(type, typeof(OracleConnection)))
                return new OracleParameter();

            if (Object.ReferenceEquals(type, typeof(OleDbConnection)))
                return new OleDbParameter();

            return new SqlParameter();

        }
        #endregion

        #region [Get Data Adapter with Connection Type]
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Conn">Connection</param>
        /// <returns>Returns a IDataAdapter object instance</returns>
        private static IDataAdapter GetDataAdapter(IDbConnection Conn)
        {
            Type type = Conn.GetType();

            if (Object.ReferenceEquals(type, typeof(SqlConnection)))
                return new SqlDataAdapter();

            if (Object.ReferenceEquals(type, typeof(OracleConnection)))
                return new OracleDataAdapter();

            if (Object.ReferenceEquals(type, typeof(OleDbConnection)))
                return new OleDbDataAdapter();

            return new SqlDataAdapter();
        }
        #endregion


        #region [Get Data Adapter with Connection Type]
        /// <summary>
        /// 
        /// </summary>
        /// <param name="conType">Connection Type</param>
        /// <returns>Returns a IDataAdapter object instance</returns>
        private static IDataAdapter GetDataAdapter(ConnectionTypes conType, IDbCommand dbcmd)
        {
            switch (conType)
            {
                case ConnectionTypes.Access:
                    return new OleDbDataAdapter((OleDbCommand) dbcmd);
                case ConnectionTypes.FireBird:
                    throw new NotSupportedException("FireBirdSQL sürücüsü eklenmedi.");
                case ConnectionTypes.NoSQL:
                    throw new NotSupportedException("NoSQL sürücüsü eklenmedi.");
                case ConnectionTypes.OpenAccess:
                    throw new NotSupportedException("NoSQL sürücüsü eklenmedi.");
                case ConnectionTypes.Oracle:
                    return new OracleDataAdapter((OracleCommand) dbcmd);
                case ConnectionTypes.SqlExpress:
                    return new SqlDataAdapter((SqlCommand) dbcmd);
                case ConnectionTypes.SQLite:
                    throw new NotSupportedException("NoSQL sürücüsü eklenmedi.");
                case ConnectionTypes.SqlServer:
                    return new SqlDataAdapter((SqlCommand)dbcmd);
                case ConnectionTypes.MySQL:
                    throw new NotSupportedException("NoSQL sürücüsü eklenmedi.");
                default:
                    return new SqlDataAdapter((SqlCommand)dbcmd);
            }
        }
        #endregion


    }

    public enum ConnectionTypes : byte
    {
        SqlExpress = 0,
        SqlServer = 1,
        Oracle = 2,
        MySQL = 3,
        Access = 4,
        SQLite = 5,
        OpenAccess = 6,
        NoSQL = 7,
        FireBird = 8
    };

}
