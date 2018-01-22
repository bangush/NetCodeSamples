using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tr.mustaliscl.metinsel
{
    public sealed class Metin
    {
        public static bool GecerliMi(string str) 
        {
            return !String.IsNullOrEmpty(str) && str.Trim().Length > 0;
        }

        public static int IcindeKactaneVar(string word, char letter)
        {
            char[] split = word.ToCharArray(); // split the word into a character array

            return split.Where(c => c.Equals(letter)).Count();
        }


       public static String MetniCevir(string metin)
        {
            return ReverseString(metin, metin.Length);
        }

        private static string ReverseString(string text, int len)
        {
            //if the provided string is a single character then return it
            if (len == 1)
                return text;
            else
                //otherwise use recursion to reverse the string
                return ReverseString(text.Substring(len - (text.Length - 1), text.Length - 1), len - 1) + text[0].ToString();
        }
    }
}
