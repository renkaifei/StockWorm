using StockWorm.Utils;
using System;

namespace StockWorm.Repository.Context
{
    public class DatabaseContextFactory
    {
        private static DatabaseContextFactory instance;
        private static object obj = new object();
        private DatabaseContextFactory() 
        {
            
        }

        public static DatabaseContextFactory GetInstance()
        {
            if(instance == null)
            {
                lock(obj)
                {
                    if(instance == null)
                    {
                        instance = new DatabaseContextFactory();
                    }
                }
            }
            return instance;
        }

        public DatabaseContext CreateDatabaseContext()
        {
            string databaseType = AppSetting.GetInstance().GetDatabaseType();
            if(databaseType == "mssql")
            {
                return new SqlDatabaseContext();
            }
            else if(databaseType == "sqlite")
            {
                return new SqliteDatabaseContext();
            }
            else
            {
                throw new ArgumentException("配置文件中数据库类型不支持");
            }
        }
    
        public DatabaseContext CreateDatabaseContext(string dbType)
        {
            if(dbType == "mssql")
            {
                return new SqlDatabaseContext();
            }
            else if(dbType == "sqlite")
            {
                return new SqliteDatabaseContext();
            }
            else
            {
                throw new ArgumentException("配置文件中数据库类型不支持");
            }
        }
    }
}