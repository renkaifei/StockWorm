using System.Data.SqlClient;
using System.Data;
using StockWorm.Utils;
using System;
using System.Data.Common;

namespace StockWorm.Repository.Context
{
    public class SqlDatabaseContext:DatabaseContext
    {
        public SqlDatabaseContext()
        {
            connectionString = AppSetting.GetInstance().GetMSSqlConnectionString();
            if(string.IsNullOrEmpty(connectionString)) throw new ArgumentException("数据库连接不能为空");
        }

        protected override void OpenConnection()
        {
            if(connection == null)
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
            }
        }

        protected override DbParameter CreateDbParameter(string name,DbType dbType,object value)
        {
            return new SqlParameter(){ ParameterName=name,DbType = dbType,Value = value };
        }
    }
}