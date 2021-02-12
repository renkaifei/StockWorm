using StockWorm.Repository.Context;

namespace StockWorm.Repository
{
    public class DatabaseFactory
    {
        private static object obj = new object();
        private static DatabaseFactory instance;

        public static DatabaseFactory GetInstance()
        {
            if(instance == null)
            {
                lock(obj)
                {
                    if(instance == null)
                    {
                        instance = new DatabaseFactory();
                    }
                }
            }
            return instance;
        }

        public SqliteDatabaseContext CreateSqlite()
        {
            return new SqliteDatabaseContext();
        }
    }
}