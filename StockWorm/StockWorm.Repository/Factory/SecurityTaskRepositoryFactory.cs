using StockWorm.Utils;
using StockWorm.Repository.Sqlite;
using StockWorm.Repository.MSSql;
using System;
using StockWorm.Repository.Context;

namespace StockWorm.Repository.Factory
{
    public class SecurityTaskRepositoryFactory
    {
        private static object obj = new object();
        private static SecurityTaskRepositoryFactory instance;

        private SecurityTaskRepositoryFactory()
        {

        }

        public static SecurityTaskRepositoryFactory GetInstance()
        {
            if(instance == null)
            {
                lock(obj)
                {
                    if(instance == null)
                    {
                        instance = new SecurityTaskRepositoryFactory();
                    }
                }
            }
            return instance;
        }

        public SecurityTaskRepository Create()
        {
            string dbType = AppSetting.GetInstance().GetDatabaseType();
            if(dbType == "sqlite")
            {
                
                return new SecurityTaskRepositorySqlite();
            }
            else if(dbType == "mssql")
            {
                return new SecurityTaskRepositoryMSSql();
            }
            else
            {
                throw new ArgumentException("数据库暂时不支持");
            }
        }

        public SecurityTaskRepository Create(DatabaseContext dbContext)
        {
            string dbType = AppSetting.GetInstance().GetDatabaseType();
            if(dbType == "sqlite")
            {
                
                return new SecurityTaskRepositorySqlite(dbContext);
            }
            else if(dbType == "mssql")
            {
                return new SecurityTaskRepositoryMSSql(dbContext);
            }
            else
            {
                throw new ArgumentException("数据库暂时不支持");
            }
        }
    }
}