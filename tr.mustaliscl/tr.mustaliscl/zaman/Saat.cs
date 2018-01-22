using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tr.mustaliscl.zaman
{
    public class Saat
    {
        public Saat() 
        {
        }

        public Saat(uint saati, uint dakika, uint saniye) 
        {
            _saati = saati;
            _dakika = dakika;
            _saniye = saniye;
        }

        private uint _saniye = 0;
        public uint Saniye
        {
            get { return _saniye; }
            set { _saniye = value % 60; }
        }

        private uint _dakika = 0;
        public uint Dakika
        {
            get { return _dakika; }
            set { _dakika = value % 60; }
        }

        private uint _saati = 0;

        public uint Saati
        {
            get { return _saati; }
            set { _saati = value % 24; }
        }

    }
}
