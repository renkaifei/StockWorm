using StockWorm.Utils;
using StockWorm.Repository.Sqlite;
using StockWorm.Repository.MSSql;
using System;
using StockWorm.Repository.Context;

namespace StockWorm.Repository.Factory
{
    public class SecurityDayQuotationRepositoryFactory
    {
        private static object obj = new object();
        private static SecurityDayQuotationRepositoryFactory instance;

        private SecurityDayQuotationRepositoryFactory()
        {

        }

        public static SecurityDayQuotationRepositoryFactory GetInstance()
        {
            if(instance == null)
            {
                lock(obj)
                {
                    if(instance == null)
                    {
                        instance = new SecurityDayQuotationRepositoryFactory();
                    }
                }
            }
            return instance;
        }

        public SecurityDayQuotationRepository Create()
        {
            string dbType = AppSetting.GetInstance().GetDatabaseType();
            if(dbType == "sqlite")
            {
                return new SecurityDayQuotationRepositorySqlite();
            }
            else if(dbType == "mssql")
            {
                return new SecurityDayQuotationRepositoryMSSql();
            }
            else
            {
                throw new ArgumentException("数据库类型暂不支持");
            }
        }

         public SecurityDayQuotationRepository Create(DatabaseContext dbContext)
        {
            string dbType = AppSetting.GetInstance().GetDatabaseType();
            if(dbType == "sqlite")
            {
                return new SecurityDayQuotationRepositorySqlite(dbContext);
            }
            else if(dbType == "mssql")
            {
                return new SecurityDayQuotationRepositoryMSSql(dbContext);
            }
            else
            {
                throw new ArgumentException("数据库类型暂不支持");
            }
        }


    }
}