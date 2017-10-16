using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RSE4Ever.DataTablesMapping.Lib.Attributes
{
    public class Table: Attribute
    {
        public String TableName { get; set; }

        public Table( string _tableName)
        {
            TableName = _tableName;
        }
    }
}