using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Xml;

namespace tr.mustaliscl.xml
{
    public static class XmlManager
    {

        static string satirNoAd = "SatirNo";
        static public XmlDocument DataTable2XML(this DataTable dT)
        {
            XmlDocument doc = new XmlDocument();
            try
            {
                XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
                doc.AppendChild(docNode);
                string tablename = dT.TableName;
                XmlNode anaNode = doc.CreateElement(tablename + "s");
                doc.AppendChild(anaNode);
                XmlNode satirNode;

                foreach (DataRow row in dT.Rows)
                {
                    satirNode = XMLHelper.AddElement(tablename, null, anaNode);
                    foreach (DataColumn col in dT.Columns)
                    {
                        XMLHelper.AddElement(col.ColumnName, row[col].ToString(), satirNode);
                    }
                }

            }
            catch (Exception)
            {
                doc = null;
            }

            return doc;
        }


        static public XmlDocument DataReader2XML(this IDataReader oku)
        {
            XmlDocument doc = new XmlDocument();
            try
            {
                XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
                doc.AppendChild(docNode);
                XmlNode anaNode = doc.CreateElement("Table");
                doc.AppendChild(anaNode);
                XmlNode satirNode;
                int kolon = oku.FieldCount;
                int satir = 1;
                while (oku.Read())
                {
                    satirNode = XMLHelper.AddElement("Satir", null, anaNode);
                    XMLHelper.AddAttribute(satirNoAd, (satir++).ToString(), satirNode);
                    for (int i = 0; i < kolon; i++)
                        XMLHelper.AddElement(oku.GetName(i), oku.GetString(i), satirNode);
                }
            }
            catch (Exception) { doc = null; }
            return doc;
        }
        
        static public XmlDocument DataReader2XML(OleDbDataReader oku)
        {
            XmlDocument doc = new XmlDocument();
            try
            {
                XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
                doc.AppendChild(docNode);
                XmlNode anaNode = doc.CreateElement("Table");
                doc.AppendChild(anaNode);
                XmlNode satirNode;
                int kolon = oku.FieldCount;
                int satir = 1;
                while (oku.Read())
                {
                    satirNode = XMLHelper.AddElement("Satir", null, anaNode);
                    XMLHelper.AddAttribute(satirNoAd, (satir++).ToString(), satirNode);
                    for (int i = 0; i < kolon; i++)
                        XMLHelper.AddElement(oku.GetName(i), oku.GetString(i), satirNode);
                }
            }
            catch (Exception) { doc = null; }
            return doc;
        }

        public static XmlDocument DataReader2XML(this SqlDataReader oku)
        {
            XmlDocument doc = new XmlDocument();
            try
            {
                XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
                doc.AppendChild(docNode);
                XmlNode anaNode = doc.CreateElement("Table");
                doc.AppendChild(anaNode);
                XmlNode satirNode;
                int kolon = oku.FieldCount;
                int satir = 1;
                while (oku.Read())
                {
                    satirNode = XMLHelper.AddElement("Satir", null, anaNode);
                    XMLHelper.AddAttribute(satirNoAd, (satir++).ToString(), satirNode);
                    for (int i = 0; i < kolon; i++)
                        XMLHelper.AddElement(oku.GetName(i), oku.GetString(i), satirNode);
                }
            }
            catch (Exception)
            {
                doc = null;
            }
            return doc;
        }

    }
}
