using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tr.mustaliscl.data
{
    public class ForeignKey
    {

        private string _name = string.Empty;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

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

        private string _referenceTable = string.Empty;
        public string ReferenceTable
        {
            get { return _referenceTable; }
            set { _referenceTable = value; }
        }

        private string _referenceColumn = string.Empty;
        public string ReferenceColumn
        {
            get { return _referenceColumn; }
            set { _referenceColumn = value; }
        }

        private string _database = string.Empty;
        public string Database
        {
            get { return _database; }
            set { _database = value; }
        }
            
    }
}
