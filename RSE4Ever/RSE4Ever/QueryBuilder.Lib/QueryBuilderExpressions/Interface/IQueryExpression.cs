namespace RSE4Ever.QueryBuilder.Lib.QueryBuilderExpressions
{
    public interface IQueryExpression<T>
    {
        string execute(params string[] _s);
    }
}