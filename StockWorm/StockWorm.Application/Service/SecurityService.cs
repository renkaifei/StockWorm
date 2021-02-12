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
            SecurityRepository securityRepo = new SecurityRepository(sqliteDb);
            SecurityTaskRepository securityTaskRepository = new SecurityTaskRepository(sqliteDb);

            #region 新增证券
            List<SecurityDomain> securitiesFromSSE = securityRepo.GetSecuritiesFromSSE();
            List<SecurityDomain> securitiesFromDB = securityRepo.GetSecuritiesFromDB();
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
            #endregion
            
            #region 保持证券，证券任务到数据库
            sqliteDb.BeginTransaction();
            SecurityTaskDomain securityTask;
            foreach(SecurityDomain security in securities)
            {
                securityRepo.InsertIntoDB(security);
                securityTask = security.BuildStartTask();
                securityTaskRepository.InsertIntoDB(securityTask);
            }
            sqliteDb.CommitTransaction();
            #endregion
        }

    }

}
