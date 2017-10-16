using DataTablesMapping.Lib.Attributes;
using DataTablesMapping.Lib.Configuration;
using RSE4Ever.DataTablesMapping.Lib;
using RSE4Ever.DataTablesMapping.Lib.Models;
using RSE4Ever.QueryBuilder.Lib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataTablesMapping.Lib.Mapping
{
    public delegate void EventHandler(object sender, EventArgs e);

    public class Mapper<T> where T : IBaseEntity, new()
    {
        public List<T> Map(DataTable table)
        {
            List<T> entities = new List<T>();

            var columnNames = table.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToList();

            var properties = (typeof(T)).GetProperties()
                .Where(x => x.GetCustomAttributes(typeof(SourceNamesAttribute), true).Any())
                .ToList();

            foreach (DataRow row in table.Rows)
            {
                T entity = new T();
                foreach (var prop in properties)
                {
                    Map(typeof(T), row, prop, entity);
                }
                entities.Add(entity);
            }
            return entities;
        }

        public void Map(Type type, DataRow row, PropertyInfo prop, object entity, int ttl = 1)
        {
            string columnName = MappingHelper.GetSourceNames(type, prop.Name).First();

            if (!String.IsNullOrWhiteSpace(columnName) && row.Table.Columns.Contains(columnName))
            {
                var propertyValue = row[columnName];

                if (propertyValue != DBNull.Value)
                {
                    if(MappingHelper.ParsePrimitive(prop, entity, row[columnName]) == false)
                    {
                        Type valueType = prop.PropertyType;

                        if (valueType.Name == prop.Name)
                        {
                            string nameSpace = EntityMapperConfig.getConfigElement("entity namespace");
                            Type t = Type.GetType(nameSpace + "." + prop.Name);
                            BaseEntity be = (BaseEntity)Activator.CreateInstance(t);
                            be.Id = (int)propertyValue;
                            prop.SetValue(entity, be);
                        }
                    }
                }
            }
        }
    }
}
