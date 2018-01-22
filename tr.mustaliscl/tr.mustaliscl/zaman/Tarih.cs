using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tr.mustaliscl.zaman
{
    public class Tarih
    {

        public Tarih() 
        {
        
        }

        public Tarih(int yil,int ay,int gun)
        {

        }

        public readonly static List<String> Gunler = new List<string>() { 
        "Pazartesi", "Salı", "Çarşamba", "Perşembe", "Cuma", "Cumartesi", "Pazar"
        };

        private uint _yil;
        public uint Yil
        {
            get { return _yil; }
            set { _yil = value; }
        }

        private uint _ay=1;
        public uint Ay
        {
            get { return _ay; }
            set { _ay = value>0 && value<13 ? value : 1; }
        }

        private uint _gun;
        public uint Gun
        {
            get { return _gun; }
            set { _gun = value>0 && value<32 ? value : 1; }
        }


    }
}
