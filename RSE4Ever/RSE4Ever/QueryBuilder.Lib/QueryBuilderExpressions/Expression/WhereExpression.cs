using RSE4Ever.QueryBuilder.Lib.QueryBuilderExpressions;
using System.Text;

namespace RSE4Ever.QueryBuilder.Lib.QueryBuilderExpressions.Expression
{   
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class WhereExpression<T> : IQueryExpression<T>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_s"></param>
        /// <returns></returns>
        public string execute(params string[] _s)
        {
            
            string ret = "";
            if(_s.Length > 1)
            {
                
                if (_s.Length > 3)
                {
                    ret = _s[3] + " " + _s[0] + " " + _s[1] + " " + _s[2];
                }
                else
                {
                    ret = " WHERE " + _s[0] + " " + _s[1] + " " + _s[2];
                }
            }
            else
            {
                ret = " WHERE " + _s[0];
            }
            return ret;
        }


    }
    
}