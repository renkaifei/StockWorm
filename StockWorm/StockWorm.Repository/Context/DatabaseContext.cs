using System;
using System.Data;
using System.Data.Common;
using StockWorm.Utils;

namespace StockWorm.Repository.Context
{
    public abstract class DatabaseContext
    {
        protected string connectionString;
        private DbTransaction transaction;
        protected DbConnection connection;
        public DatabaseContext()
        {
            
        }
        private bool InTrans()
        {
            return connection.State == ConnectionState.Open && transaction != null;
        }
        protected abstract void OpenConnection();
        protected abstract DbParameter CreateDbParameter(string name,DbType dbType,object value);
        private void CloseConnection()
        {
            if(connection.State == ConnectionState.Open)
            {
                connection.Close();
                connection.Dispose();
                connection = null;
            }
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
                CloseConnection();
            }
        }
        public void RollbackTransaction()
        {
            try
            {
                transaction.Rollback();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                transaction.Dispose();
                transaction = null;
                CloseConnection();
            }
        }
        public void ExecuteDataReader(Action<DbDataReader> action, string sql, params DbParameter[] DbParams)
        {
            try
            {
                OpenConnection();
                using(DbCommand command = connection.CreateCommand())
                {
                    command.CommandText = sql;
                    if(DbParams != null) command.Parameters.AddRange(DbParams);
                    if(InTrans()) command.Transaction = transaction;
                    DbDataReader reader = command.ExecuteReader();
                    action(reader);
                    command.Parameters.Clear();
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
        public void ExecuteNoQuery(string sql, params DbParameter[] DbParams)
        {
            try
            {
                OpenConnection();
                using(DbCommand command = connection.CreateCommand())
                {
                    command.CommandText = sql;
                    if(DbParams != null) command.Parameters.AddRange(DbParams);
                    if(InTrans()) command.Transaction = transaction;
                    command.ExecuteNonQuery();
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
        public abstract void CreateDatabase(string source,string databaseName,string userID,string pwd,string databasePath);
    }
}