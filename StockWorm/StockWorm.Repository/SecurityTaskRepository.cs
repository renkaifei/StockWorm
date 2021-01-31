using StockWorm.Domain;
using StockWorm.Repository.Context;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.Sqlite;
using StockWorm.Repository.Factory;

namespace StockWorm.Repository
{
    public class SecurityTaskRepository
    {
        private SecurityTaskFactory securityTaskFactory;
        private SqliteDatabaseContext sqliteDb;

        public SecurityTaskRepository()
        {
            securityTaskFactory = new SecurityTaskFactory();
            sqliteDb = new SqliteDatabaseContext();
        }

        public SecurityTaskRepository(SqliteDatabaseContext sqliteDb):this()
        {
            this.sqliteDb = sqliteDb;
        }

        public SecurityTaskDomain Create(SecurityTaskDomain securityTask)
        {
            string sql = "INSERT INTO SecurityTask(SecurityCode,ExchangeMarket,BeginDate,EndDate,IsFinished)Values(@SecurityCode,@ExchangeMarket,@BeginDate,@EndDate,@IsFinished)";
            SqliteParameter prmSecurityCode = new SqliteParameter("@SecurityCode", DbType.String) { Value = securityTask.SecurityCode };
            SqliteParameter prmExchangeMarket = new SqliteParameter("@ExchangeMarket",DbType.String){ Value = securityTask.ExchangeMarket };
            SqliteParameter prmBeginDate = new SqliteParameter("@BeginDate", DbType.DateTime) { Value = securityTask.BeginDate };
            SqliteParameter prmEndDate = new SqliteParameter("@EndDate", DbType.DateTime) { Value = securityTask.EndDate };
            SqliteParameter prmIsFinished = new SqliteParameter("@IsFinished",DbType.Int32){ Value = securityTask.IsFinished ? 1: 0};
            sqliteDb.ExecuteNoQuery(sql, prmSecurityCode, prmExchangeMarket,prmBeginDate, prmEndDate,prmIsFinished);
            sql = "SELECT last_insert_rowid() FROM SecurityTask ";
            sqliteDb.ExecuteDataReader(reader =>
            {
                if(reader.HasRows)
                {
                    reader.Read();
                    securityTask.TaskID = reader.GetInt64(0);
                }
            }, sql);
            return securityTask;
        }

        public SecurityTaskDomain GetOneTask()
        {
            SecurityTaskDomain securityTask = securityTaskFactory.Create();
            string sql = "SELECT TaskID,SecurityCode,BeginDate,EndDate,IsFinished FROM SecurityTask LIMIT 1";
            sqliteDb.ExecuteDataReader(reader =>
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    securityTask.TaskID = reader.GetInt64(0);
                    securityTask.SecurityCode = reader.GetString(1);
                    securityTask.BeginDate = reader.GetDateTime(2);
                    securityTask.EndDate = reader.GetDateTime(3);
                    securityTask.IsFinished = reader.GetInt32(4) == 1;
                }
            }, sql);
            return securityTask;
        }

        public void InsertIntoDB(List<SecurityTaskDomain> securityTasks)
        {
            foreach(SecurityTaskDomain securityTask in securityTasks)
            {
                Create(securityTask);
            }
        }
    }
}
