using RSE4Ever.DataTablesMapping.Lib;
using RSE4Ever.DataTablesMapping.Lib.Attributes;
using RSE4Ever.QueryBuilder.Lib.QueryBuilderExpressions;
using RSE4Ever.QueryBuilder.Lib.QueryBuilderExpressions.Exceptions;
using RSE4Ever.QueryBuilder.Lib.QueryBuilderExpressions.Expression;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;

namespace RSE4Ever.QueryBuilder.Lib
{

    /// <summary>
    /// 
    /// </summary>
    public enum QueryConditionsEnum
    {
        Equals,
        NotEquals,
        BiggerThan,
        BiggerEqualThan,
        MinerEqualThan,
        MinerThan
    };
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Builder<T>
        where T : IBaseEntity, new()
    {
        /// <summary>
        /// 
        /// </summary>
        private Dictionary<QueryConditionsEnum, string> QueryConditions;
        /// <summary>
        /// 
        /// </summary>
        public string TableName;
        /// <summary>
        /// 
        /// </summary>
        public StringBuilder QueryString;
        /// <summary>
        /// 
        /// </summary>
        public QueryExecutor<T> _qe;
        /// <summary>
        /// 
        /// </summary>
        private int suidCount = 0;
        /// <summary>
        /// 
        /// </summary>
        private int whereCount = 0;
        /// <summary>
        /// 
        /// </summary>
        private int fromCount = 0;
        /// <summary>
        /// 
        /// </summary>
        public Builder()
        {
            if (typeof(T).GetCustomAttributes(typeof(Table), true).Count() > 0)
            {
                TableName = ((Table)typeof(T).GetCustomAttributes(typeof(Table), true).First()).TableName;
            }
            else
            {
                throw new BuilderException("Inccorect Entity Mapping For entity " + typeof(T).Name + "Missing Table Annotation.");
            }

            QueryString = new StringBuilder();
            QueryConditions = new Dictionary<QueryConditionsEnum, string>();
            QueryConditions[QueryConditionsEnum.Equals] = "=";
            QueryConditions[QueryConditionsEnum.NotEquals] = "<>";
            QueryConditions[QueryConditionsEnum.BiggerThan] = ">";
            QueryConditions[QueryConditionsEnum.BiggerEqualThan] = ">=";
            QueryConditions[QueryConditionsEnum.MinerEqualThan] = "=<";
            QueryConditions[QueryConditionsEnum.MinerThan] = "<";
        }

        public void refresh()
        {
            whereCount = 0;
            fromCount = 0;
            suidCount = 0;
            QueryString.Clear();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_e"></param>
        /// <param name="_s"></param>
        private void Append(IQueryExpression<T> _e, params string[] _s)
        {
            QueryString.Append(_e.execute(_s)).Append(" ");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_s"></param>
        /// <returns></returns>
        public Builder<T> Select(params string[] _s)
        {
            if (suidCount == 0)
            {
                Append(new SelectExpression<T>(), _s);
                Append(new FromExpression<T>(), TableName);
                suidCount++;
                return this;
            }
            throw new BuilderException("Unique Expression already used");

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_s"></param>
        /// <returns></returns>
        private Builder<T> From(string _s)
        {
            if (fromCount == 0)
            {
                Append(new FromExpression<T>(), _s);
                return this;
            }
            throw new BuilderException("Unique Expression already used");

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_s"></param>
        /// <param name="_entity"></param>
        /// <returns></returns>
        public Builder<T> Update(T _entity)
        {
            if (suidCount == 0)
            {

                QueryString.Append(new UpdateExpression<T>().execute(_entity, TableName));
                suidCount++;
                return this;
            }
            throw new BuilderException("Unique Expression already used");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_s"></param>
        /// <param name="_entity"></param>
        /// <returns></returns>
        public Builder<T> Insert(T _entity)
        {
            if (suidCount == 0)
            {
                QueryString.Append(new InsertExpression<T>().execute(_entity, TableName));
                suidCount++;
                return this;
            }
            throw new BuilderException("Unique Expression already used");

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_s"></param>
        /// <returns></returns>
        public Builder<T> Delete()
        {
            if (suidCount == 0)
            {
                Append(new DeleteExpression<T>(), TableName);
                suidCount++;
                return this;
            }
            throw new BuilderException("Unique Expression already used");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_s"></param>
        /// <param name="condition"></param>
        /// <param name="_s2"></param>
        /// <returns></returns>
        public Builder<T> Where(string _s, QueryConditionsEnum condition, string _s2)
        {
            if (_s != "" && _s2 != "")
            {
                if (whereCount == 0)
                {
                    whereCount++;
                    string[] _string = new string[3];

                    _string[0] = _s;
                    _string[1] = QueryConditions[condition];
                    _string[2] = _s2;

                    Append(new WhereExpression<T>(), _string);
                    return this;
                }
                throw new BuilderException("Expression Where already used");
            }
            throw new BuilderException("Missing one or many Element in Where condition");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_s"></param>
        /// <returns></returns>
        public Builder<T> Where(string _s)
        {
            if (_s != "")
            {
                if (whereCount == 0)
                {
                    whereCount++;
                    Append(new WhereExpression<T>(), _s);
                    return this;
                }
                throw new BuilderException("Expression Where already used");
            }
            throw new BuilderException("Where condition can not be empty ... Please type the condition in string parameter [_s] ");


        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_s"></param>
        /// <returns></returns>
        public Builder<T> Join(params string[] _s)
        {
            if (_s[0] != "")
            {
                Append(new JoinExpression<T>(), _s);
                return this;
            }
            throw new BuilderException("Join clause need a table name in string parameter [_s]");

        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private StringBuilder getQueryStringBuilder()
        {
            return QueryString;
        }

        public List<T> Execute()
        {
            try
            {
                QueryExecutor<T> QE = QueryExecutor<T>.Instance;

                List<T> list = QE.Execute(QueryString);
                refresh();
                return list;

            }
            catch (QueryExecutorException e)
            {
                throw new BuilderException(e.Message);
            }
        }
        public T GetEntity()
        {

            T entity = Execute().First();
            return entity;

        }
        public List<T> GetEntityList()
        {
            return Execute();
        }
        public DataRow GetDataRow()
        {
            DataRow dr;
            try
            {
                dr = QueryExecutor<T>.Instance.GetDataTable(QueryString).Rows[0];

            }
            catch (QueryExecutorException e)
            {
                throw new BuilderException(e.Message);
            }

            refresh();
            return dr;
        }
        public DataTable GetDataTable()
        {
            DataTable dt;
            try
            {
                dt = QueryExecutor<T>.Instance.GetDataTable(QueryString);

            }
            catch (QueryExecutorException e)
            {
                throw new BuilderException(e.Message);
            }
            refresh();
            return dt;
        }

        public Builder<T> SetQueryString(string queryString)
        {
            QueryString.Clear();
            QueryString.Append(queryString);
            return this;
        }
    }
}