using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tr.mustaliscl.dataclient
{
    public class Default
    {

        private string _table = string.Empty;
        public string Table
        {
            get { return _table; }
            set { _table = value; }
        }

        private string _column = string.Empty;
        public string Column
        {
            get { return _column; }
            set { _column = value; }
        }

        private string _defaultValue;
        public string DefaultValue
        {
            get { return _defaultValue; }
            set { _defaultValue = value; }
        }
    }
}
