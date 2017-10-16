using RSE4Ever.DataTablesMapping.Lib;
using RSE4Ever.QueryBuilder.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RSE4Ever.Repositories
{
    public class BaseRepository<T> : IRepository<T>
        where T:IBaseEntity, new()
    {
        Builder<T> builder;
        /// <summary>
        /// 
        /// </summary>
        public BaseRepository()
        {
            builder = new Builder<T>();
        }

        public IEnumerable<T> GetList()
        {
            return builder.Select("*").GetEntityList();
        }   

        public void Add(T entity)
        {
            builder.Insert(entity).Execute();
        }

        public void Delete(T entity)
        {
            builder.Delete().Where("id = " + entity.Id).Execute();
        }

        public T FindById(int Id)
        {
            return builder.Select("*").Where(" id = "+ Id).GetEntity();
        }

        public void Update(T entity)
        {
            builder.Update(entity).Where("id = " + entity.Id).Execute();
        }
    }
}