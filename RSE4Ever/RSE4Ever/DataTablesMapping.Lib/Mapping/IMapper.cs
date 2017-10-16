using RSE4Ever.DataTablesMapping.Lib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTablesMapping.Lib.Mapping
{
    public interface IMapper<T> where T : IBaseEntity, new()
    {
        List<T> Map(DataTable table);
    }
}
