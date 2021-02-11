using System;
using System.Collections.Generic;
using StockWorm.Repository;
using StockWorm.Domain;
using StockWorm.Domain.Factory;
using StockWorm.Repository.Context;


namespace StockWorm.Application.Service
{
    public class SecurityService
    {
        public void SyncSecuritiesFromSSE()
        {
            SqliteDatabaseContext sqliteDb = new SqliteDatabaseContext();
            SecurityRepository repo = new SecurityRepository(sqliteDb);
            List<SecurityDomain> securitiesFromSSE = repo.GetSecuritiesFromSSE();
            List<SecurityDomain> securitiesFromDB = repo.GetSecuritiesFromDB();
            List<SecurityDomain> securities = new List<SecurityDomain>();
            Dictionary<string,string> dicStockDB = new Dictionary<string, string>();
            foreach(SecurityDomain security in securitiesFromDB)
            {
                dicStockDB.Add(security.SecurityCode,"");
            }

            for(int i =0;i < securitiesFromSSE.Count;i++)
            {
                if(!dicStockDB.ContainsKey(securitiesFromSSE[i].SecurityCode))
                {
                    securities.Add(securitiesFromSSE[i]);
                }
            }

            SecurityTaskDomain securityTask;
            List<SecurityTaskDomain> securityTasks = new List<SecurityTaskDomain>();
            SecurityTaskFactory taskFactory = new SecurityTaskFactory();
            foreach(SecurityDomain security in securities)
            {
                securityTask = taskFactory.Create();
                securityTask.SecurityCode = security.SecurityCode;
                securityTask.ExchangeMarket = "SSE";
                securityTask.BeginDate = security.ListingDate;
                securityTask.EndDate = security.ListingDate.AddMonths(1);
                if(securityTask.EndDate.Date > DateTime.Now.AddDays(-1).Date)
                {
                    securityTask.EndDate = DateTime.Now.AddDays(-1).Date;
                }
                securityTasks.Add(securityTask);
            }
            SecurityTaskRepository securityTaskRepository = new SecurityTaskRepository(sqliteDb);
            sqliteDb.BeginTransaction();
            repo.InsertIntoDB(securities);
            securityTaskRepository.InsertIntoDB(securityTasks);
            sqliteDb.CommitTransaction();
        }

    }

}
