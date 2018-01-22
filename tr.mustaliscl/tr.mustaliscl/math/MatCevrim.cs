using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tr.mustaliscl.math
{
    public static class MatCevrim
    {
        public static Int32 ToInt(this string str)
        {
            int i;
            Int32.TryParse(str, out i);
            return i;
        }

        public static Double ToDouble(this string str)
        {
            double d;
            Double.TryParse(str, out d);
            return d;
        }

        public static Int64 ToLong(this string str)
        {
            long l;
            Int64.TryParse(str, out l);
            return l;
        }
    }
}
