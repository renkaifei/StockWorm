using StockWorm.Utils;
using StockWorm.Repository.Sqlite;
using StockWorm.Repository.MSSql;
using System;
using StockWorm.Repository.Context;

namespace StockWorm.Repository.Factory
{
    public class SecurityRepositoryFactory
    {
        private static object obj = new object();
        private static SecurityRepositoryFactory instance;

        private SecurityRepositoryFactory()
        {
            
        }
        public static SecurityRepositoryFactory GetInstance()
        {
            if(instance == null)
            {
                lock(obj)
                {
                    if(instance == null)
                    {
                        instance = new SecurityRepositoryFactory();
                    }
                }
            }
            return instance;
        }

        public SecurityRepository Create()
        {
            string dbType = AppSetting.GetInstance().GetDatabaseType();
            if(dbType == "sqlite")
            {
                return new SecurityRepositorySqlite(); 
            }
            else if(dbType == "mssql")
            {
                return new SecurityRepositoryMSSql();
            }
            else
            {
                throw new ArgumentException("数据库类型暂时不支持");
            }
        }

        public SecurityRepository Create(DatabaseContext dbContext)
        {
            string dbType = AppSetting.GetInstance().GetDatabaseType();
            if(dbType == "sqlite")
            {
                return new SecurityRepositorySqlite(dbContext); 
            }
            else if(dbType == "mssql")
            {
                return new SecurityRepositoryMSSql(dbContext);
            }
            else
            {
                throw new ArgumentException("数据库类型暂时不支持");
            }
        }
    }
}