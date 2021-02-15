using StockWorm.Repository.Context;
using System.Data;
using System.Data.SqlClient;
using StockWorm.Domain;
using System.Collections.Generic;
using System;

namespace StockWorm.Repository.MSSql
{
    public class SecurityTaskRepositoryMSSql:SecurityTaskRepository
    {
        private DatabaseContext dbContext;
        public SecurityTaskRepositoryMSSql()
        {
            dbContext = DatabaseContextFactory.GetInstance().CreateDatabaseContext("mssql");
        }
        public SecurityTaskRepositoryMSSql(DatabaseContext dbContext):this()
        {
            this.dbContext = dbContext;
        }
        public override SecurityTaskDomain InsertIntoDB(SecurityTaskDomain securityTask)
        {
            string sql = "INSERT INTO SecurityTask(SecurityCode,ExchangeMarket,BeginDate,EndDate,IsFinished)Values(@SecurityCode,@ExchangeMarket,@BeginDate,@EndDate,@IsFinished)";
            SqlParameter prmSecurityCode = new SqlParameter("@SecurityCode", SqlDbType.VarChar,10) { Value = securityTask.SecurityCode };
            SqlParameter prmExchangeMarket = new SqlParameter("@ExchangeMarket",SqlDbType.VarChar,10){ Value = securityTask.ExchangeMarket };
            SqlParameter prmBeginDate = new SqlParameter("@BeginDate", SqlDbType.DateTime) { Value = securityTask.BeginDate };
            SqlParameter prmEndDate = new SqlParameter("@EndDate", SqlDbType.DateTime) { Value = securityTask.EndDate };
            SqlParameter prmIsFinished = new SqlParameter("@IsFinished",SqlDbType.Int){ Value = securityTask.IsFinished ? 1: 0};
            dbContext.ExecuteNoQuery(sql, prmSecurityCode, prmExchangeMarket,prmBeginDate, prmEndDate,prmIsFinished);
            return securityTask;
        }
        public override List<SecurityTaskDomain> GetListUnFinished(int count,DateTime date)
        {
            SecurityTaskDomain securityTask;
            List<SecurityTaskDomain> lst = new List<SecurityTaskDomain>();
            string sql = string.Format("SELECT Top {0} TaskID,SecurityCode,ExchangeMarket,BeginDate,EndDate,IsFinished FROM SecurityTask where IsFinished = 0 and BeginDate < @BeginDate",count);
            SqlParameter prmBeginDate = new SqlParameter("@BeginDate",SqlDbType.DateTime){ Value = date };
            dbContext.ExecuteDataReader(reader =>{
                while(reader.Read())
                {
                    securityTask = new SecurityTaskDomain();
                    securityTask.TaskID = reader.GetInt32(0);
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
            SqlParameter prmSecurityCode = new SqlParameter("@SecurityCode", DbType.String) { Value = securityTask.SecurityCode };
            SqlParameter prmIsFinished = new SqlParameter("@IsFinished",DbType.Int32){ Value = securityTask.IsFinished ? 1: 0};
            dbContext.ExecuteNoQuery(sql, prmSecurityCode, prmIsFinished);
        }
    
    }
}