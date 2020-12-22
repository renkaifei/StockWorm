using Microsoft.Data.Sqlite;
using System;
using StockWorm.Repository.Interfaces;

namespace StockWorm.Repository.Context
{
    public class SqliteDatabaseContext:ITransaction
    {
        private string connectionString = "Data Source='C:\\renkf\\projects\\database\\Stock.db';";
        private SqliteTransaction transaction;

        public SqliteTransaction Transaction{ get{ return transaction; } }

        private bool IsInTrans()
        {
            return transaction != null;
        }

        public SqliteDatabaseContext()
        {
            transaction = null;
        }
    
        public void BeginTransaction()
        {
            SqliteConnection connection = new SqliteConnection(connectionString);
            connection.Open();
            transaction = connection.BeginTransaction();
        }

        public void JoinTrans(SqliteTransactionRepository transRepo)
        {
            transaction = transRepo.Transaction;
        }

        public void Commit()
        {
            try
            {
                transaction.Commit();
            }
            catch(Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
        }

        public void Rollback()
        {
            transaction.Rollback();
        }

        public void ExecuteDataReader(Action<SqliteDataReader> action, string sql,params SqliteParameter[] sqliteParams)
        {
            using(SqliteConnection conn = new SqliteConnection(connectionString))
            {
                conn.Open();
                using(SqliteCommand cmd = new SqliteCommand(sql,conn))
                {
                    if(sqliteParams != null) cmd.Parameters.AddRange(sqliteParams);
                    using(SqliteDataReader reader = cmd.ExecuteReader())
                    {
                        action(reader);
                    }
                }
            }
        }

        public void ExecuteNoQuery(string sql,params SqliteParameter[] sqliteParams)
        {
            if(IsInTrans())
            {
                ExecuteNoQueryInTransaction(sql,sqliteParams);
            }
            else
            {
                ExecuteNoQueryNotInTransaction(sql,sqliteParams);
            }
        }

        private void ExecuteNoQueryNotInTransaction(string sql,params SqliteParameter[] sqliteParams)
        {
            using(SqliteConnection conn = new SqliteConnection())
            {
                conn.Open();
                using(SqliteCommand cmd  = new SqliteCommand(sql,conn))
                {
                    cmd.Parameters.AddRange(sqliteParams);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        private void ExecuteNoQueryInTransaction(string sql,params SqliteParameter[] sqliteParams)
        {
            SqliteCommand command = new SqliteCommand(sql,transaction.Connection);
            command.Parameters.AddRange(sqliteParams);
            command.Transaction = transaction;
            command.ExecuteNonQuery();
        }
    
    }
}