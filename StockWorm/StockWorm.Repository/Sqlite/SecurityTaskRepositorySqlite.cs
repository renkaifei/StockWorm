using System;
using StockWorm.Domain;
using Microsoft.Data.Sqlite;
using System.Data;
using StockWorm.Repository.Context;
using System.Collections.Generic;

namespace StockWorm.Repository.Sqlite
{
    public class SecurityTaskRepositorySqlite:SecurityTaskRepository
    {
        private DatabaseContext dbContext;

        public SecurityTaskRepositorySqlite()
        {
            DatabaseContextFactory factory = new DatabaseContextFactory();
            dbContext = factory.CreateDatabaseContext();
        }

        public SecurityTaskRepositorySqlite(DatabaseContext dbContext):this()
        {
            this.dbContext = dbContext;
        }

        public override SecurityTaskDomain InsertIntoDB(SecurityTaskDomain securityTask)
        {
            string sql = "INSERT INTO SecurityTask(SecurityCode,ExchangeMarket,BeginDate,EndDate,IsFinished)Values(@SecurityCode,@ExchangeMarket,@BeginDate,@EndDate,@IsFinished)";
            SqliteParameter prmSecurityCode = new SqliteParameter("@SecurityCode", DbType.String) { Value = securityTask.SecurityCode };
            SqliteParameter prmExchangeMarket = new SqliteParameter("@ExchangeMarket",DbType.String){ Value = securityTask.ExchangeMarket };
            SqliteParameter prmBeginDate = new SqliteParameter("@BeginDate", DbType.DateTime) { Value = securityTask.BeginDate };
            SqliteParameter prmEndDate = new SqliteParameter("@EndDate", DbType.DateTime) { Value = securityTask.EndDate };
            SqliteParameter prmIsFinished = new SqliteParameter("@IsFinished",DbType.Int32){ Value = securityTask.IsFinished ? 1: 0};
            dbContext.ExecuteNoQuery(sql, prmSecurityCode, prmExchangeMarket,prmBeginDate, prmEndDate,prmIsFinished);
            return securityTask;
        }

        public override List<SecurityTaskDomain> GetListUnFinished(int count,DateTime date)
        {
            SecurityTaskDomain securityTask;
            List<SecurityTaskDomain> lst = new List<SecurityTaskDomain>();
            string sql = string.Format("SELECT TaskID,SecurityCode,ExchangeMarket,BeginDate,EndDate,IsFinished FROM SecurityTask where IsFinished = 0 and BeginDate < @BeginDate LIMIT {0}",count);
            SqliteParameter prmBeginDate = new SqliteParameter("@BeginDate",DbType.DateTime){ Value = date };
            dbContext.ExecuteDataReader(reader =>{
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
    
        public override void UpdateTaskStatus(SecurityTaskDomain securityTask)
        {
            string sql = "Update SecurityTask SET IsFinished = @IsFinished Where SecurityCode = @SecurityCode";
            SqliteParameter prmSecurityCode = new SqliteParameter("@SecurityCode", DbType.String) { Value = securityTask.SecurityCode };
            SqliteParameter prmIsFinished = new SqliteParameter("@IsFinished",DbType.Int32){ Value = securityTask.IsFinished ? 1: 0};
            dbContext.ExecuteNoQuery(sql, prmSecurityCode, prmIsFinished);
        }
    }
}