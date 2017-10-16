using System;

namespace RSE4Ever.DataTablesMapping.Lib
{
    public interface IBaseEntity
    {
        string GUID { get; set; }

        int Id { get; }

        DateTime UpdateDate { get; set; }

        DateTime InsertDate { get; set; }

        Boolean Enable { get; set; }

        Boolean SoftDelete { get; set; }

        void Notify(string property);

    }
}