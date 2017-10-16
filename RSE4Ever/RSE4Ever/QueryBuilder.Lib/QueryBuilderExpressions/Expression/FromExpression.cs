using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace RSE4Ever.QueryBuilder.Lib.QueryBuilderExpressions.Expression
{
    public class FromExpression<T> : IQueryExpression<T>
    {
        public String execute(params string[] _s)
        {
            return " FROM " + _s[0];
        }
    }
}