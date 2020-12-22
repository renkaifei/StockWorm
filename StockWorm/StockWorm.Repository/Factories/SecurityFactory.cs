using System.Collections.Generic;
using StockWorm.Domain;
using StockWorm.Domain.Interfaces;
using StockWorm.Repository;

namespace StockWorm.Repository.Factories
{
    public class SecurityFactory
    {
        private static SecurityFactory instance;
        private static object obj = new object();

        private SecurityFactory()
        {

        }

        public static SecurityFactory GetInstance()
        {
            if(instance == null)
            {
                lock(obj)
                {
                    if(instance == null)
                    {
                        instance = new SecurityFactory();
                    }
                }
            }
            return instance;
        }

        public IGetDataBehavior BuildSecuritiesGetListFromSSERepository(List<SecurityDomain> values)
        {
            IGetDataBehavior behavior = new SecuritiesGetListFromSSERepository(values);
            return behavior;
        }
    }
}