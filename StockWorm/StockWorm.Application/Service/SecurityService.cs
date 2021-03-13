using System;
using System.Collections.Generic;
using StockWorm.Repository;
using StockWorm.Domain;
using StockWorm.Repository.Context;
using StockWorm.Repository.Factory;

namespace StockWorm.Application.Service
{
    public class SecurityService
    {
        public void SyncSecuritiesFromSSE()
        {
            DatabaseContextFactory factory = new DatabaseContextFactory();
            DatabaseContext dbContext = factory.CreateDatabaseContext();
            SecurityRepository securityRepo = SecurityRepositoryFactory.GetInstance().Create(dbContext);
            SecurityTaskRepository securityTaskRepository = SecurityTaskRepositoryFactory.GetInstance().Create(dbContext);
            SecurityFromSSERepository securityFromSSERepository = new SecurityFromSSERepository();
            #region 新增证券
            List<SecurityDomain> securitiesFromSSE = securityFromSSERepository.GetSecuritiesFromSSE();
            List<SecurityDomain> securitiesFromDB = securityRepo.GetList();
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
            dbContext.BeginTransaction();
            SecurityTaskDomain securityTask;
            foreach(SecurityDomain security in securities)
            {
                securityRepo.InsertIntoDB(security);
                securityTask = security.BuildStartTask();
                securityTaskRepository.InsertIntoDB(securityTask);
            }
            dbContext.CommitTransaction();
            #endregion
        }

        public List<SecurityDomain> GetListInPage(int pageIndex,int pageSize,string exChange)
        {
            SecurityRepository securityRepo = SecurityRepositoryFactory.GetInstance().Create();
            List<SecurityDomain> lst = securityRepo.GetListInPage(pageIndex,pageSize,exChange);
            return lst;
        }
    }

}
