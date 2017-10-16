namespace RSE4Ever.DataTablesMapping.Lib
{
    public interface IEntityObserver
    {
        void Update(string property, IBaseEntity entity);
        void AddEntity(IBaseEntity entity);
    }
}