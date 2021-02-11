using StockWorm.Domain;
using StockWorm.Repository;
using System.Collections.Generic;
using StockWorm.Domain.Factory;

namespace StockWorm.Application.Service
{
    public class SecurityTaskService
    {
        private SecurityTaskFactory securityTaskFactory;

        public SecurityTaskService(SecurityTaskFactory securityTaskFactory)
        {
            this.securityTaskFactory = securityTaskFactory ;
        }

        
    }
}