using Microsoft.Data.Sqlite;
using System;
using StockWorm.Utils;
using System.Data;
using System.Data.Common;

namespace StockWorm.Repository.Context
{
    public class SqliteDatabaseContext:DatabaseContext
    {
        public SqliteDatabaseContext()
        {
            connectionString = AppSetting.GetInstance().GetSqliteConnectionString();
            if(string.IsNullOrEmpty(connectionString)) throw new ArgumentException("数据库连接不能为空");
        }
        protected override void OpenConnection()
        {
            if(connection == null)
            {
                connection = new SqliteConnection(connectionString);
                connection.Open();
            }
        }
        protected override DbParameter CreateDbParameter(string name,DbType dbType,object value)
        {
            return new SqliteParameter(){ ParameterName=name,DbType = dbType,Value = value };
        }
    }
}