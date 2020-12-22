using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using StockWorm.Repository;
using StockWorm.Domain;
using StockWorm.Domain.Interfaces;
using StockWorm.Repository.Factories;
using StockWorm.Repository.Interfaces;


namespace StockWorm.Application
{
    public class SecurityService
    {
        public List<SecurityDomain> GetListFromSSE()
        {
            List<SecurityDomain> securities = new List<SecurityDomain>();
            int pageIndex =1;
            int pageSize = 1000;
            List<SecurityDomain> tempSecurities = new List<SecurityDomain>();
            IGetDataBehavior getDataBehavior = SecurityFactory.GetInstance().BuildSecuritiesGetListFromSSERepository(tempSecurities,pageIndex,pageSize);
            getDataBehavior.GetData();
            while(tempSecurities.Count > 0)
            {
                securities.AddRange(tempSecurities);
                tempSecurities.Clear();
                pageIndex++;
                getDataBehavior = SecurityFactory.GetInstance().BuildSecuritiesGetListFromSSERepository(tempSecurities,pageIndex,pageSize);
                getDataBehavior.GetData();
            }
            return securities;
        }

        public void SaveSecurities(List<SecurityDomain> securities)
        {
            if(securities.Count == 0) return;
            SqliteTransactionRepository trans = new SqliteTransactionRepository();
            ICreateDataBehavior behavior = SecurityFactory.GetInstance().BuildSecuritiesCreateRepository(securities);
            trans.BeginTransaction();
            ITransaction behaviorTrans = behavior as ITransaction;
            behaviorTrans.JoinTrans(trans);
            behavior.CreateData();
            trans.Commit();
        }
    
        public void SaveSecuritiesFromSSEToSqlite()
        {
            List<SecurityDomain> values = GetListFromSSE();
            SaveSecurities(values);
        }
    }
        
}
