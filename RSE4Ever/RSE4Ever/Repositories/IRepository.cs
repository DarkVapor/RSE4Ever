using RSE4Ever.DataTablesMapping.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RSE4Ever.Repositories
{
    public interface IRepository<T>
        where T:IBaseEntity
    {
        IEnumerable<T> GetList();
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
        T FindById(int Id);
    }
}