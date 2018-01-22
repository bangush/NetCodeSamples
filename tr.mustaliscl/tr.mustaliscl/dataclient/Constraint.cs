using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tr.mustaliscl.data
{
   public class Constraint
    {
        private string _name = string.Empty;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _schema = string.Empty;

        public string Schema
        {
            get { return _schema; }
            set { _schema = value; }
        }

        private string _table = string.Empty;

        public string Table
        {
            get { return _table; }
            set { _table = value; }
        }

        private string _database = string.Empty;

        public string Database
        {
            get { return _database; }
            set { _database = value; }
        }
       
    }
}
