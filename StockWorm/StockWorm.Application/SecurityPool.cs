using StockWorm.Domain;
using System.Collections.Generic;

namespace StockWorm.Application
{
    public class SecurityPool
    {
        private List<SecurityDomain> securities;

        public SecurityPool()
        {
            securities = new List<SecurityDomain>();
        }

        
    }
}