using RSE4Ever.DataTablesMapping.Lib;
using RSE4Ever.QueryBuilder.Lib.QueryBuilderExpressions;
using System.Reflection;
using RSE4Ever.DataTablesMapping.Lib.Attributes;
using DataTablesMapping.Lib.Attributes;
using DataTablesMapping.Lib.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSE4Ever.QueryBuilder.Lib.QueryBuilderExpressions.Expression
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class UpdateExpression<T> : IQueryExpression<T>
        where T:IBaseEntity
    {
        public StringBuilder sb;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_s"></param>
        /// <returns></returns>
        public string execute(params string[] _s)
        {
            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="_s"></param>
        /// <returns></returns>
        public string execute(T entity, params string[] _s)
        {
            sb = new StringBuilder();
            sb.Append("UPDATE "+ _s[0]);
            int i = 0;

            List<string> properties = new List<string>();

            if (EntityObserver.Instance.EOMs.Where(p => p.BaseEntity.GUID == entity.GUID).Count() > 0)
            {
                properties = EntityObserver.Instance.EOMs.Where(p => p.BaseEntity.GUID == entity.GUID).First().ChangedField;
            }

            foreach( PropertyInfo p in entity.GetType().GetProperties())
            {
                if(properties.Where(o => o == p.Name).Count() > 0)
                {
                    var sna = p.GetCustomAttributes(typeof(SourceNamesAttribute), true);

                    if (sna != null)
                    {
                        if (sna.Count() > 0)
                        {
                            SourceNamesAttribute snao = (SourceNamesAttribute)sna.First();
                            string propname = snao.ColumnNames[0];
                            string value = "";

                            string affectation = "'";

                            if (p.GetValue(entity).GetType() == typeof(int)
                                || p.GetType() == typeof(double)
                                || p.GetType() == typeof(float))
                            {
                                affectation = "";
                            }

                            if (p.GetValue(entity).GetType() == typeof(string))
                            {
                                if (p.GetValue(entity) == null)
                                {
                                    value = "";
                                }
                            }

                            value = p.GetValue(entity).ToString();

                            if (p.GetValue(entity).GetType() == typeof(bool) || p.GetValue(entity).GetType() == typeof(Boolean))
                            {
                                value = "1";
                                if ((Boolean)p.GetValue(entity) == false)
                                {
                                    value = "0";
                                }
                                affectation = "";
                            }

                           
                            sb.Append(((i == 0) ? " SET " : " ") + propname + " = " + affectation + value + affectation +  ((i == properties.Count()-1)?" ":", ") );
                            
                            i++;
                        }
                    }
                }
            }
            return sb.ToString();
        }
    }
}