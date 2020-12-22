using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using StockWorm.Repository;
using StockWorm.Domain;
using StockWorm.Domain.Interfaces;
using StockWorm.Repository.Factories;


namespace StockWorm.Application
{
    public class SecurityService
    {
        public List<SecurityDomain> GetListFromSSE()
        {
            List<SecurityDomain> securities = new List<SecurityDomain>();
            IGetDataBehavior getDataBehavior = SecurityFactory.GetInstance().BuildSecuritiesGetListFromSSERepository(securities);
            getDataBehavior.GetData();
            return securities;
        }

        public void SaveSecurities(List<SecurityDomain> securities)
        {
            
        }
    }
        
}
