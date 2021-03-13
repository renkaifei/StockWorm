using System;

namespace StockWorm.Domain.Factory
{
    public class DatabaseConfigFactory
    {
        public DatabaseConfig Create(string databaseType)
        {
            if(databaseType == "mssql")
            {
                return new MSSqlDatabaseConfig();
            }
            else if(databaseType == "sqlite")
            {
                return new SqliteDatabaseConfig();
            }
            else
            {
                throw new ArgumentException(string.Format("数据库类型[{0}]暂不支持",databaseType));
            }
        }
    }
}