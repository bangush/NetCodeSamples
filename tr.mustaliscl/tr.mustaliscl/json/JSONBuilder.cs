using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using tr.mustaliscl.metinsel;

namespace tr.mustaliscl.json
{
    public class JSONBuilder
    {
        static string json_pure_format = "\"{0}\":\"{1}\",";
        static string json_last_format = "\"{0}\":\"{1}\",";
        static string json_array_format = "[{0}]";

        public static string BuildJSON<T>(List<T> liste) 
        {
            StringBuilder strBuiler = new StringBuilder();
            
            try
            {
                strBuiler.AppendFormat("\"{0}\"",liste.GetType().Name);
                strBuiler.AppendLine(":[{\n");

                for (int i = 0; i < liste.Count; i++)
                {
                    var item = liste.ElementAt(i);
                    PropertyInfo[] props = item.GetType().GetProperties();
                    for (int j = 0; j < props.Length; j++)
                    {
                        string str = "";
                        if(j<props.Length-1)
                            str="\"{0}\":\"{1}\",\n";
                        else
                            str="\"{0}\":\"{1}\"\n";

                        strBuiler.AppendFormat(str, props[j].Name, props[j].GetValue(item, null) ?? DBNull.Value);
                    }

                    if (i < liste.Count - 1)
                        strBuiler.AppendLine("},");
                    else
                        strBuiler.AppendLine("}]");
                }
             }
            catch (Exception)
            {
                strBuiler = new StringBuilder();
            }
            return strBuiler.ToString();
        }
    }
}

