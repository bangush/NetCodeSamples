using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace tr.mustaliscl.data
{
    public class Param
    {
        public string Name { get; set; }

        public object Value { get; set; }

        public SqlDbType Tip { get; set; }
    }
}
