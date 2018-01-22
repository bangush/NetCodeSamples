using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Reflection;
using tr.mustaliscl.metinsel;

namespace tr.mustaliscl.xml
{
    public class XML
    {
        #region [Özel tanımlamalar]
        Type[] tipler ={
                      typeof(String),
                      typeof(Int16),
                      typeof(Int32),
                      typeof(Int64),
                      typeof(UInt16),
                      typeof(UInt32),
                      typeof(String),
                      typeof(Double),
                      typeof(Single),
                      typeof(Byte),
                      typeof(Boolean),
                      typeof(Char),
                      typeof(DateTime),
                      typeof(Decimal),
                      typeof(Object),
                      typeof(SByte),
                      typeof(TimeSpan),
                      typeof(DateTimeOffset)
                      };
        #endregion


        #region [DataReader dan XmlDocument ve Xmlstring döndürme]
        string table = "Table";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader">DataReader nesnesi</param>
        /// <param name="tablename">Xml de gösterilecek tablo ismi</param>
        /// <returns>Xml dosyası döndürür.</returns>
        public XmlDocument DataReader2Xml(IDataReader reader, string tablename)
        {
            XmlDocument doc = null;
            try
            {
                doc = new XmlDocument();
                XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
                doc.AppendChild(docNode);
                string tableName = Metin.GecerliMi(tablename) ? tablename : table;
                XmlNode rootNode = doc.CreateElement(tableName + "s");
                doc.AppendChild(rootNode);

                while (reader.Read())
                {
                    XmlNode rowNode = doc.CreateElement(tableName);
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        XmlNode colNode = doc.CreateElement(reader.GetName(i));
                        colNode.InnerText = reader.GetString(i);
                        rowNode.AppendChild(colNode);
                    }

                    rootNode.AppendChild(rowNode);
                }

            }
            catch (Exception)
            {
                doc = null;
            }
            return doc;
        }

        public String DataReader2XmlString(IDataReader reader, string tablename)
        {
            StringBuilder strBuilder = new StringBuilder();
            try
            {
                string tableName = Metin.GecerliMi(tablename) ? tablename : table;
                strBuilder.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
                strBuilder.AppendFormat("<{0}>\n", tableName + "s");
                while (reader.Read())
                {
                    strBuilder.AppendFormat("<{0}>\n", tableName);
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        strBuilder.AppendFormat("<{0}>{1}</{2}>\n",
                            reader.GetName(i), reader.GetString(i), reader.GetName(i));
                    }
                    strBuilder.AppendFormat("</{0}>\n", tableName);
                }
                strBuilder.AppendFormat("</{0}>\n", tableName + "s");

            }
            catch (Exception)
            {
                strBuilder = new StringBuilder();
                strBuilder.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            }
            return strBuilder.ToString();
        }
        #endregion

        
        #region [List den XmlDocument ve XmlString döndürme.]
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public XmlDocument CreateXmlDocument<T>(List<T> list)
        {
            XmlDocument doc = null;

            try
            {
                doc = new XmlDocument();
                XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
                doc.AppendChild(docNode);
                string listname = list[0].GetType().Name;
                XmlNode rootNode = doc.CreateElement(listname + "s");
                doc.AppendChild(rootNode);

                foreach (var item in list)
                {
                    XmlNode rowNode = doc.CreateElement(listname);

                    foreach (PropertyInfo prop in item.GetType().GetProperties())
                    {
                        XmlNode colNode = doc.CreateElement(prop.Name);
                        colNode.InnerText = prop.GetValue(item, null).ToString();
                        rowNode.AppendChild(colNode);

                    }

                    rootNode.AppendChild(rowNode);
                }

            }
            catch (Exception)
            {
                doc = null;
            }

            return doc;
        }// end CreateXmlDocument

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public String CreateXmlString<T>(List<T> list)
        {
            StringBuilder strBuilder = new StringBuilder();
            try
            {
                strBuilder.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
                string listname = list[0].GetType().Name;
                strBuilder.AppendFormat("<{0}>\n", listname + "s");

                foreach (var item in list)
                {
                    strBuilder.AppendFormat("<{0}>\n", listname);

                    foreach (PropertyInfo prop in item.GetType().GetProperties())
                    {
                        object val = prop.GetValue(item, null) ?? DBNull.Value;
                        strBuilder.AppendFormat("<{0}>{1}</{2}>\n",
                            prop.Name, val.ToString(), prop.Name);

                    }

                    strBuilder.AppendFormat("</{0}>\n", listname);
                }

                strBuilder.AppendFormat("</{0}>\n", listname + "s");
            }
            catch (Exception)
            {
                strBuilder = new StringBuilder();
                strBuilder.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            }

            return strBuilder.ToString();
        }
        #endregion


        #region [DataTable nesnesinden XmlDocuyment ve XmlString döndürme.]
        /// <summary>
        /// 
        /// </summary>
        /// <param name="datatable"></param>
        /// <returns></returns>
        public XmlDocument CreateXmlDocument(DataTable datatable)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.AppendChild(docNode);
            string tablename = datatable.TableName;
            XmlNode rootNode = doc.CreateElement(tablename + "s");
            doc.AppendChild(rootNode);
            try
            {
                foreach (DataRow row in datatable.Rows)
                {
                    XmlNode rowNode = doc.CreateElement(tablename);
                    foreach (DataColumn col in datatable.Columns)
                    {
                        XmlNode columnNode = doc.CreateElement(col.ColumnName);
                        columnNode.InnerText = row[col].ToString();
                        rowNode.AppendChild(columnNode);
                    }
                    rootNode.AppendChild(rowNode);
                }
            }
            catch (Exception)
            {
                doc = new XmlDocument();
            }
            return doc;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="datatable"></param>
        /// <returns></returns>
        public String CreateXmlString(DataTable datatable) 
        {
            StringBuilder strBuilder = new StringBuilder();
            try
            {
                strBuilder.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
                string listname = datatable.TableName;

                strBuilder.AppendFormat("<{0}>\n", listname + "s");

                foreach (DataRow row in datatable.Rows)
                {
                    strBuilder.AppendFormat("<{0}>\n", listname);

                    foreach (DataColumn col in datatable.Columns)
                    {
                        strBuilder.AppendFormat("<{0}>{1}</{2}>\n",
                            col.ColumnName, row[col].ToString(), col.ColumnName);

                    }

                    strBuilder.AppendFormat("</{0}>\n", listname);
                }

                strBuilder.AppendFormat("</{0}>\n", listname + "s");
            }
            catch (Exception)
            {
                strBuilder = new StringBuilder();
                strBuilder.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            }
            return strBuilder.ToString();
        }

        #endregion

        public static void XML2TreeView(ref TreeView tv, string xmlPath)
        {
            tv.Nodes.Clear();
            FileStream fs = null;
            XmlDocument xDoc = null;
            try
            {
                fs = new FileStream(xmlPath, FileMode.Open);
                xDoc = new XmlDocument();
                xDoc.Load(fs);
                TreeNode firstNode = new TreeNode(xmlPath);
                doNodes(xDoc, firstNode.Nodes);
                tv.Nodes.Add(firstNode);
            }
            catch (Exception)
            {
            }
            finally
            {
                xDoc = null;
                fs.Close();
            }

        }//end XML2TreeView

        static void doNodes(XmlNode xNode, TreeNodeCollection tNodeColl)
        {
            foreach (XmlNode child in xNode.ChildNodes)
            {
                TreeNode tn = null;
                if (!child.HasChildNodes && !(child.Value == null))
                {
                    tn = tNodeColl.Add(child.Value);
                }
                else
                {
                    tn = tNodeColl.Add(child.Name);
                    doNodes(child, tn.Nodes);
                }//end else of if(xnode)
            }// end foreach
        }//end doNodes

    }
}
