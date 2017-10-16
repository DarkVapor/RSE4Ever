using DataTablesMapping.Lib.Attributes;
using AttributeTable = RSE4Ever.DataTablesMapping.Lib.Attributes.Table;
using RSE4Ever.QueryBuilder.Lib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Linq;
using System.Linq;
using System.Web;

namespace RSE4Ever.DataTablesMapping.Lib
{
    public abstract class BaseEntity : IBaseEntity
    {

        private IEntityObserver entityObserver;

        public string GUID { get; set; }

        [SourceNames("id", "id")]
        public int Id { get; set; }
        [SourceNames("update_date", "updateDate")]
        public DateTime UpdateDate { get; set; }
        [SourceNames("insert_date", "insertDate")]
        public DateTime InsertDate { get; set; }
        [SourceNames("enable", "enable")]
        public Boolean Enable { get; set;}
        [SourceNames("delete", "delete")]
        public Boolean SoftDelete{ get; set; }
        /// <summary>
        /// 
        /// </summary>
        public BaseEntity()
        {
            entityObserver = EntityObserver.Instance;
            GUID = Guid.NewGuid().ToString();
            entityObserver.AddEntity(this);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="property"></param>
        public void Notify(string property)
        {
            entityObserver.Update(property, this);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="outTable"></param>
        /// <returns></returns>
        public List<T> GetManyToMany<T>(string outTable)
            where T:IBaseEntity, new()
        {
            Builder<T> builder = new Builder<T>();

            String tablename = ((AttributeTable)this.GetType().GetCustomAttributes(typeof(AttributeTable), true).First()).TableName;

            return builder.Select("*")
                .Join(tablename + outTable , tablename + outTable+"."+outTable +"_id", outTable+".id")
                .Join(tablename , tablename+".id" , tablename + outTable +"."+tablename+"_id")
                .Where( tablename+".id = "+Id )
                .GetEntityList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="outTable"></param>
        /// <returns></returns>
        public List<T> GetOneToMany<T>(string outTable)
           where T : IBaseEntity, new()
        {
            Builder<T> builder = new Builder<T>();

            String tablename = ((AttributeTable)this.GetType().GetCustomAttributes(typeof(AttributeTable), true).First()).TableName;

            return builder.SetQueryString(" SELECT * FROM " + outTable + " WHERE "+ tablename + "_id = " + Id).GetEntityList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="outTable"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetOneToOne<T>(string outTable, int id)
         where T : IBaseEntity, new()
        {
            Builder<T> builder = new Builder<T>();

            String tablename = ((AttributeTable)this.GetType().GetCustomAttributes(typeof(AttributeTable), true).First()).TableName;

            T be =  builder.SetQueryString(" SELECT * FROM "+ outTable+ " WHERE id = "+id ).GetEntity();
            return be; 
        }
    }
}