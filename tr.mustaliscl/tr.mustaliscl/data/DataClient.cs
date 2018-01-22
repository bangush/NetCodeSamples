using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace tr.mustaliscl.data
{
    /// <summary>
    /// 
    /// </summary>
    public class DataClient
    {

        #region [Get Procedure Names]
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connStr">Bağlantı Metni</param>
        /// <returns>Bağlantı metni içerisindeki datbase de bulunan Prosedürleri 
        /// String içerikli List olarak döndürür.</returns>
        public static List<String> GetProcNames(string connStr)
        {
            List<String> pros = new List<string>();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = @"select name from sys.objects where type='P';";
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                pros.Add(reader.GetString(0));
                            }
                        }
                        reader.Close();
                        conn.Close();
                    }
                    //cmd.Parameters.Clear();                
                }

            }
            return pros;
        }// end GetProcNames
        #endregion


        #region [Get Function Names]
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connStr">Bağlantı Metni</param>
        /// <returns>Bağlantı metni içerisindeki datbase de bulunan Fonksiyonları 
        /// String içerikli List olarak döndürür.</returns>
        public static List<String> GetFuncNames(string connStr)
        {
            List<String> pros = new List<string>();
            using (SqlConnection conn = new SqlConnection(connStr))
            {

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = @"select name from sys.objects where type_desc LIKE '%FUNCTION%';";
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                pros.Add(reader.GetString(0));
                            }
                        }
                        reader.Close();
                        conn.Close();
                    }
                    //cmd.Parameters.Clear();                
                }
            }
            return pros;
        }// end GetFuncNames
        #endregion


        #region [Get Script of A Procedure or Function]
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connStr">
        /// Bağlantı kurulacak Veri Kaynağı;
        /// </param>
        /// <param name="NameOfProcOrFunc">
        /// Prosedür yada Fonksiyon Ismi
        /// </param>
        /// <returns>Prosedür içeriğini string nesneye yükleyip geri döndürür.</returns>
        public static String getProcOrFuncScript(string connStr, string NameOfProcOrFunc)
        {
            StringBuilder sB = new StringBuilder("");
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_helptext";
                    cmd.Parameters.Add("@objname", SqlDbType.VarChar).Value =
                        NameOfProcOrFunc;
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                sB.AppendLine(reader.GetString(0));//["Text"] as String);
                            }
                        }
                        reader.Close();
                        conn.Close();
                    }
                    cmd.Parameters.Clear();
                }
            }
            return sB.ToString();
        }
        #endregion


        #region [Get All Constraints]
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connStr">Bağlantı Metni</param>
        /// <returns>Bağlantı metninde bulunan veritabanına bağlı 
        /// Zorlama (Constraint) ları Constraint nesne tipindeki List içinde getirir.</returns>
        public static List<Constraint> GetAllContraints(string connStr)
        {
            List<Constraint> constraints = new List<Constraint>();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = @"SELECT OBJECT_NAME(OBJECT_ID) AS NameofConstraint,
                    SCHEMA_NAME(schema_id) AS SchemaName,
                    OBJECT_NAME(parent_object_id) AS TableName,
                    type_desc AS ConstraintType
                    FROM sys.objects
                    WHERE type_desc like '%CONSTRAINT%' ORDER BY TableName, NameofConstraint;";
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                constraints.Add(
                                    new Constraint()
                                    {
                                        Name = reader.GetString(0),
                                        Schema = reader.GetString(1),
                                        Table = reader.GetString(2),
                                        Database = conn.Database
                                    }
                                    );
                            }
                        }
                        reader.Close();
                        conn.Close();
                    }
                    //cmd.Parameters.Clear();                
                }
            }
            return constraints;
        }// end GetAllconstraints
        #endregion


        #region [Get All Primary Keys]
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connStr">Bağlantı Metni</param>
        /// <returns>Bağlantı metninde bulunan veritabanına bağlı 
        /// Birincil Anahtar(Primary Key) ları PrimaryKey nesne tipindeki List içinde getirir.</returns>
        public static List<PrimaryKey> GetAllPrimaryKeys(string connStr)
        {
            List<PrimaryKey> primes = new List<PrimaryKey>();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = @"SELECT i.name AS IndexName,
                    OBJECT_NAME(ic.OBJECT_ID) AS TableName,
                    COL_NAME(ic.OBJECT_ID,ic.column_id) AS ColumnName
                    FROM sys.indexes AS i
                    INNER JOIN sys.index_columns AS ic
                    ON i.OBJECT_ID = ic.OBJECT_ID
                    AND i.index_id = ic.index_id
                    WHERE i.is_primary_key = 1;";
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                primes.Add(
                                    new PrimaryKey()
                                    {
                                        Name = reader.GetString(0),
                                        Table = reader.GetString(1),
                                        Column = reader.GetString(2),
                                        Database = conn.Database
                                    }
                                    );
                            }
                        }
                        reader.Close();
                        conn.Close();
                    }
                    //cmd.Parameters.Clear();                
                }
            }
            return primes;
        }// end GetAllconstraints
        #endregion


        #region [Get All Foreign Keys]
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connStr">Bağlantı Metni</param>
        /// <returns>Bağlantı metninde bulunan veritabanına bağlı 
        /// Yabancıl Anahtar(Foreign Key) ları ForeignKey nesne tipindeki List içinde getirir.</returns>
        public static List<ForeignKey> GetAllForeignKeys(string connStr)
        {
            List<ForeignKey> foreigns = new List<ForeignKey>();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = @"SELECT f.name AS ForeignKey,
                    OBJECT_NAME(f.parent_object_id) AS TableName,
                    COL_NAME(fc.parent_object_id,
                    fc.parent_column_id) AS ColumnName,
                    OBJECT_NAME (f.referenced_object_id) AS ReferenceTableName,
                    COL_NAME(fc.referenced_object_id,
                    fc.referenced_column_id) AS ReferenceColumnName
                    FROM sys.foreign_keys AS f
                    INNER JOIN sys.foreign_key_columns AS fc
                    ON f.OBJECT_ID = fc.constraint_object_id;";
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                foreigns.Add(
                                    new ForeignKey()
                                    {
                                        Name = reader.GetString(0),
                                        Table = reader.GetString(1),
                                        Column = reader.GetString(2),
                                        ReferenceTable = reader.GetString(3),
                                        ReferenceColumn = reader.GetString(4),
                                        Database = conn.Database
                                    }
                                    );
                            }
                        }
                        reader.Close();
                        conn.Close();
                    }
                    //cmd.Parameters.Clear();                
                }
            }
            return foreigns;
        }// end GetAllconstraints
        #endregion


        #region [Get Default Values of Columns]

        #endregion
    }
}
