using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RSE4Ever.DataTablesMapping.Lib.Mapping
{
    public class EntityObservableModel
    {
        public IBaseEntity BaseEntity {get; set;}
        public List<string> ChangedField { get; set; }
    }
}