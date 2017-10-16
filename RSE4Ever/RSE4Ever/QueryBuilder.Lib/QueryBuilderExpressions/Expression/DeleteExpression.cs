using RSE4Ever.QueryBuilder.Lib.QueryBuilderExpressions;

namespace RSE4Ever.QueryBuilder.Lib.QueryBuilderExpressions.Expression
{
    internal class DeleteExpression<T> : IQueryExpression<T>
    {
        public string execute(params string[] _s)
        {
            return "DELETE FROM "+_s[0];
        }
    }
}