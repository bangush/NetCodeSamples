using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tr.mustaliscl.data
{
    public abstract class Manager : IManager
    {
        public DbManager dBManager
        {
            get
            { return new DbManager(); }
        }

    }
}
