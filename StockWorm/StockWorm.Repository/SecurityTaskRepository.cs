using StockWorm.Domain;
using StockWorm.Repository.Context;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.Sqlite;
using StockWorm.Domain.Factory;
using System;

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

        public SecurityTaskDomain InsertIntoDB(SecurityTaskDomain securityTask)
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

        public SecurityTaskDomain GetOneUnFinishedTask()
        {
            SecurityTaskDomain securityTask = securityTaskFactory.Create();
            string sql = "SELECT TaskID,SecurityCode,BeginDate,EndDate,IsFinished FROM SecurityTask where IsFinished = 0 LIMIT 1";
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

        public List<SecurityTaskDomain> GetListUnFinished(int count,DateTime date)
        {
            SecurityTaskDomain securityTask;
            List<SecurityTaskDomain> lst = new List<SecurityTaskDomain>();
            string sql = string.Format("SELECT TaskID,SecurityCode,ExchangeMarket,BeginDate,EndDate,IsFinished FROM SecurityTask where IsFinished = 0 and BeginDate < @BeginDate LIMIT {0}",count);
            SqliteParameter prmBeginDate = new SqliteParameter("@BeginDate",DbType.DateTime){ Value = date };
            sqliteDb.ExecuteDataReader(reader =>{
                while(reader.Read())
                {
                    securityTask = new SecurityTaskDomain();
                    securityTask.TaskID = reader.GetInt64(0);
                    securityTask.SecurityCode = reader.GetString(1);
                    securityTask.ExchangeMarket = reader.GetString(2);
                    securityTask.BeginDate = reader.GetDateTime(3);
                    securityTask.EndDate = reader.GetDateTime(4);
                    securityTask.IsFinished = false;
                    lst.Add(securityTask);
                }
            },sql,prmBeginDate);
            return lst;
        }

        public void InsertIntoDB(List<SecurityTaskDomain> securityTasks)
        {
            foreach(SecurityTaskDomain securityTask in securityTasks)
            {
                InsertIntoDB(securityTask);
            }
        }
    
        public void UpdateTaskStatus(SecurityTaskDomain securityTask)
        {
            string sql = "Update SecurityTask SET IsFinished = @IsFinished Where SecurityCode = @SecurityCode";
            SqliteParameter prmSecurityCode = new SqliteParameter("@SecurityCode", DbType.String) { Value = securityTask.SecurityCode };
            SqliteParameter prmIsFinished = new SqliteParameter("@IsFinished",DbType.Int32){ Value = securityTask.IsFinished ? 1: 0};
            sqliteDb.ExecuteNoQuery(sql, prmSecurityCode, prmIsFinished);
        }
    }
}
