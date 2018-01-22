using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tr.mustaliscl.zaman
{
    /// <summary>
    /// 
    /// </summary>
    public class Zaman
    {

        public Zaman() : this(new Tarih(),new Saat())
        {        
        }

        public Zaman(Tarih tarih, Saat saat) 
        {
            _tarih = tarih;
            _saat = saat;
        }

        private Saat _saat;

        public Saat Saat
        {
            get { return _saat; }
            set { _saat = value; }
        }

        private Tarih _tarih;

        public Tarih Tarih
        {
            get { return _tarih; }
            set { _tarih = value; }
        }

        public DateTime toDateTime() 
        {
            return new DateTime();
        }
    }
}
