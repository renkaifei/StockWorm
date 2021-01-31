using Microsoft.Data.Sqlite;
using System;
using StockWorm.Utils;
using System.Data;

namespace StockWorm.Repository.Context
{
    public class SqliteDatabaseContext
    {
        private string connectionString;
        private SqliteTransaction transaction;
        private SqliteConnection connection;

        private bool InTrans()
        {
            return connection.State == ConnectionState.Open && transaction != null;
        }

        public SqliteDatabaseContext()
        {
            connectionString = AppSetting.GetInstance().GetSqliteConnectionString();
            if(string.IsNullOrEmpty(connectionString)) throw new ArgumentException("数据库连接不能为空");
        }

        private void OpenConnection()
        {
            if(connection == null)
            {
                connection = new SqliteConnection(connectionString);
                connection.Open();
            }
        }

        private void CloseConnection()
        {
            connection.Close();
            connection.Dispose();
            connection = null;
        }

        public void BeginTransaction()
        {
            OpenConnection();
            transaction = connection.BeginTransaction();
        }

        public void CommitTransaction()
        {
            try
            {
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
            finally
            {
                transaction.Dispose();
                transaction = null;
            }
        } 

        public void RollbackTransaction()
        {
            transaction.Rollback();
            transaction.Dispose();
            transaction = null;
        }

        public void ExecuteDataReader(Action<SqliteDataReader> action, string sql, params SqliteParameter[] sqliteParams)
        {
            try
            {
                OpenConnection();
                using(SqliteCommand cmd = new SqliteCommand())
                {
                    cmd.CommandText = sql;
                    cmd.Connection = connection;
                    if(InTrans()) cmd.Transaction = transaction;
                    if (sqliteParams != null) cmd.Parameters.AddRange(sqliteParams);
                    using(SqliteDataReader reader = cmd.ExecuteReader())
                    {
                        action(reader);
                    }
                }
                if (!InTrans()) CloseConnection();
            }
            catch(Exception ex)
            {
                if(InTrans()) RollbackTransaction();
                CloseConnection();
                throw ex;
            }
        }

        public void ExecuteNoQuery(string sql, params SqliteParameter[] sqliteParams)
        {
            try
            {
                OpenConnection();
                using(SqliteCommand cmd = new SqliteCommand())
                {
                    cmd.CommandText = sql;
                    cmd.Connection = connection;
                    if(InTrans()) cmd.Transaction = transaction;
                    if(sqliteParams != null) cmd.Parameters.AddRange(sqliteParams);
                    cmd.ExecuteNonQuery();
                }
                if(!InTrans()) CloseConnection();
            }
            catch(Exception ex) 
            {
                if(InTrans()) RollbackTransaction();
                CloseConnection();
                throw ex;
            }
        }
    }
}