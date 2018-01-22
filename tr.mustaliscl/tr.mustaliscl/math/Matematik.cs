using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tr.mustaliscl.math
{
    public class Matematik
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index">sıra numarası</param>
        /// <returns>
        /// index;
        /// sıfırdan küçükse -1,
        /// sıfırsa 0,
        /// birse 1,
        /// bunların dışında o sıradaki fibonacci sayısını döndürür.
        /// </returns>
        public static long GetNthFibonacciNumber(int index)
        {
            if (index < 0)
                return -1L;
            switch (index)
            {
                case 0:
                    return 0L;
                case 1:
                    return 1L;
                default:
                    return GetNthFibonacciNumber(index - 1) + GetNthFibonacciNumber(index - 2);
            }//end switch
        }// end GetNthFibonacciNumber


        /// <summary>
        /// Gelen üs sayısına göre o dereceden katsayıları döndüren method.
        /// </summary>
        /// <param name="us">üs sayısı</param>
        /// <returns>Gelen üs sayısına göre o dereceden katsayıları döndürür.</returns>
        public static int[] getPascalTriangle(int us)
        {
            if (us < 0)
                throw new InvalidOperationException("Üs sıfırdan küçük olamaz...");
            else
            {
                switch (us)
                {
                    case 0:
                        return new int[] { 1 };
                    case 1:
                        return new int[] { 1, 1 };
                    default:
                        int[] retArray = new int[us + 1];
                        retArray[0] = 1; retArray[us] = 1;
                        int[] tmpArray = getPascalTriangle(us - 1);
                        for (int i = 1; i < us; i++)
                        {
                            retArray[i] = tmpArray[i] + tmpArray[i - 1];
                        }//end for
                        return retArray;
                }//end switch
            }//end else
        } //end getPascalTriangle

        /// <summary>
        /// 
        /// </summary>
        /// <param name="side1">1. kenar</param>
        /// <param name="side2">2. kenar</param>
        /// <param name="side3">3.kenar</param>
        /// <returns>Üçgen geçerli olup olmadığını bool tipinde değişken döndürür.</returns>
        public static bool isTriangleValid(int side1, int side2, int side3)
        {
            return ((side1 > 0) && (side2 > 0) && (side2 > 0) &&
                ((side1 + side2 - side3) * (side1 - side2 + side3) *
                (side3 + side2 - side1) > 0));
        }//end isTriangleValid

        public static Int32 Convert2Integer(string str)
        {
            int i;
            Int32.TryParse(str, out i);
            return i;
        }

        public static Double Convert2Double(string str)
        {
            double d;
            Double.TryParse(str, out d);
            return d;
        }


        public static Int64 Convert2Long(string str)
        {
            long l;
            Int64.TryParse(str, out l);
            return l;
        }


    }
}
