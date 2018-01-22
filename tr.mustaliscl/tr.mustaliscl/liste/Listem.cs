using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tr.mustaliscl.liste
{
    public sealed class Listem
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int GetIndexOf<T>(T[] array, T value)
        {
            if (array == null) { return -4; } //throw new System.InvalidOperationException("Dizi geçersiz..."); }
            else if (array.Length == 0) { return -3; }// throw new System.InvalidOperationException("Dizi boyutu sıfır..."); }
            else if (value == null) { return -2; }//throw new System.InvalidOperationException("aranan nesne geçersiz..."); }
            else
            {
                for (int index = 0; index < array.Length; index++)
                    if (array[index].Equals(value))
                        return index;
                return -1;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int GetIndexOf<T>(System.Collections.Generic.List<T> array, T value)
        {
            if (array == null) { throw new System.InvalidOperationException("Dizi geçersiz..."); }
            else if (array.Count == 0) { throw new System.InvalidOperationException("Dizi boyutu sıfır..."); }
            else if (value == null) { throw new System.InvalidOperationException("aranan nesne geçersiz..."); }
            else
            {
                for (int index = 0; index < array.Count; index++)
                    if (array[index].Equals(value))
                        return index;
                return -1;
            }
        }
    }
}
