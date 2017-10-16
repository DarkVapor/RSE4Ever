
using RSE4Ever.DataTablesMapping.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace RSE4Ever.QueryBuilder.Lib.QueryBuilderExpressions.Expression
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SelectExpression<T> : IQueryExpression<T>
        where T:IBaseEntity
    {
       /// <summary>
       /// 
       /// </summary>
       /// <param name="_s"></param>
       /// <returns></returns>
        public string execute(params string[] _s)
        {

            StringBuilder sb = new StringBuilder();
            int i = 0;
            foreach(string label in _s)
            {
                if(_s.Length-1 > i)
                {
                    sb.Append(label).Append(",");
                }
                else
                {
                    sb.Append(label);
                }
                i++;
            }
            return "SELECT " + sb.ToString();
        }
    }
}