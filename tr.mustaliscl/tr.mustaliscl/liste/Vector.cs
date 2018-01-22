using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace tr.mustaliscl.liste
{
    public class Vector<T> : IDisposable
    {
        private T[] tD = new T[0];
        private int boyut = 0;

        public Vector()
        {
        }//end constructor


        public T this[int index] {
            get 
            {
                return IndekstekiniGetir(index);
            }
            set 
            {
                if (index < boyut && index < 0)
                {
                    if (object.ReferenceEquals(value.GetType(), typeof(T)))
                    {
                        tD[index] = value;
                    }
                    else
                        throw new InvalidCastException("Tip dönüşümü hatası");
                }
                else
                    throw new InvalidOperationException("Indeks boyuttan büyük ya da sıfırdan küçük olamaz.");
            }
        }
        /// 
        /// </summary>
        /// <param name="t"></param>
        public Vector(T t)
        {
            tD = new T[1]; tD[0] = t; boyut = 1;
        }//end constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        public void Ekle(T t)
        {
            int tmp_size = boyut + 1;
            switch (boyut)
            {
                case 0:
                    tD = new T[1]; tD[0] = t;
                    break;
                default:
                    T[] tmpT = new T[tmp_size];
                    for (int i = 0; i < boyut; i++)
                    {
                        tmpT[i] = tD[i];
                    }
                    tmpT[boyut] = t;
                    tD = tmpT;
                    break;
            }//end  switch
            ++boyut;
        }//end Ekle(T t)

        public void CikarIndekstekini(int x)
        {
            if (x > boyut && x < 0)
                throw new InvalidOperationException("Indeks numarası, Listenin boyutundan büyük yada sıfırdan küçük olamaz.");
            else
            {
                //var query = tD.Where<T>(t => t != null);
                switch (boyut)
                {
                    case 0:
                        throw new InvalidOperationException("Listede zaten eleman yok.");
                    default:
                        T[] tmp = new T[boyut - 1];
                        for (int i = 0; i < boyut; i++)
                        {
                            if (i < x)
                            {
                                tmp[i] = tD[i];
                            }
                            else if (i > x)
                            {
                                tmp[i] = tD[i - 1];
                            }
                            else { continue; }
                        }// end for
                        tD = tmp;
                        break;
                }//end switch
                boyut = tD.Length;
            }// end else
        }

        public T IndekstekiniGetir(int ix)
        {
            if (ix < boyut)
            {
                return tD[ix];
            }
            else
                throw new InvalidOperationException("Eleman Mevcut değil yada İndeks Numarası fazla.");

        }

        public int IlkIndeksi(T t)
        {
            Int32 it = -1;
            switch (boyut)
            {
                case 0:
                    break;
                default:
                    for (int i = 0; i < boyut; i++)
                    {
                        if (t.Equals(tD[i]))
                        { it = i; break; }
                    }
                    break;
            }
            return it;
        }

        public Int32 SonIndeksi(T t)
        {
            Int32 it = -1;
            switch (boyut)
            {
                case 0:
                    break;
                default:
                    for (int i = boyut - 1; i >= 0; i--)
                    {
                        if (t.Equals(tD[i]))
                        { it = i; break; }
                    }
                    break;
            }
            return it;
        }

        public T Ilk()
        {
            switch (boyut)
            {
                case 0:
                    throw new InvalidOperationException("Eleman mevcut değil....");
                default:
                    return tD[0];
            }
        }

        public T Son()
        {
            switch (boyut)
            {
                case 0:
                    throw new InvalidOperationException("Eleman mevcut değil....");
                default:
                    return tD[boyut - 1];
            }
        }
        public void TersCevir()
        {
            switch (boyut)
            {
                case 0:
                    return;
                default:
                    for (int i = 0; i < boyut / 2; i++)
                    {
                        T tmp = tD[i];
                        tD[i] = tD[boyut - i - 1];
                        tD[boyut - i - 1] = tmp;
                    }
                    break;
            }
        }

        public String Yazdir()
        {
            StringBuilder sB = new StringBuilder();
            switch (boyut)
            {
                case 0:
                    sB.Append(""); break;
                default:
                    foreach (T t in tD)
                    {
                        sB.Append(t.ToString());
                    }
                    break;
            }
            return sB.ToString();
        }

        public Int32 Boyut { get { return boyut; } }

        public void Temizle()
        {
            tD = new T[0];
            boyut = 0;
        }

        public T[] toArray()
        {
            return tD;
        }

        public List<T> ToListe() {
            return tD.ToList<T>();            
        }

        ~Vector()
        {
            tD = null; boyut = 0;
            System.GC.Collect();
        }

        public void Dispose()
        {
            tD = null; boyut = 0;
            System.GC.Collect();
        }
    }
}
