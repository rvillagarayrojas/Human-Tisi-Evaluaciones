using Microsoft.Practices.EnterpriseLibrary.Data;


namespace Siscom.Data.Interface
{
    public abstract class BaseAbstractDA
    {
        protected static Database db = null;

        protected BaseAbstractDA()
        {
            DatabaseFactory.ClearDatabaseProviderFactory();
            DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory());
            db = DatabaseFactory.CreateDatabase();
        }
    }
}
