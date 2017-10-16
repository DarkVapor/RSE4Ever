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
{   /// <summary>
    ///     
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class JoinExpression<T> : IQueryExpression<T>
        where T : IBaseEntity
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_s"></param>
        /// <returns></returns>
        public string execute(params string[] _s)
        {
            string ret = "";
            ret = "  JOIN " + _s[0]  + " ON " + _s[1] + " = " + _s[2];
            return ret;
        }
       
    }
}