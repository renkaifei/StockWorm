using StockWorm.Domain;
using StockWorm.Repository;
using System.Collections.Generic;
using StockWorm.Repository.Context;
using StockWorm.Repository.Factory;
using StockWorm.Utils;

namespace StockWorm.Application.Service
{
    public class SecurityDayQuotationService
    {

        public SecurityDayQuotationService()
        {
            
        }

        public void SyncSSEDayQuotationFromWangYI()
        {
            DatabaseContext dbContext = DatabaseContextFactory.GetInstance().CreateDatabaseContext();
            SecurityDayQuotationRepository securityDayQuotationRepository = SecurityDayQuotationRepositoryFactory.GetInstance().Create(dbContext);
            SecurityTaskRepository securityTaskRepository = SecurityTaskRepositoryFactory.GetInstance().Create(dbContext);
            SecurityDayQuotationFromWangYIRepository securityDayQuotationFromWangYIRepository = new SecurityDayQuotationFromWangYIRepository();
            SecurityTaskEngine securityTaskEngine = new SecurityTaskEngine();
            SecurityTaskDomain securityTask = securityTaskEngine.Pop();
            List<SecurityDayQuotationDomain> lst = new List<SecurityDayQuotationDomain>();
            SecurityTaskDomain nextSecurityTask;
            while(!securityTask.IsEmpty())
            {
                CancelTokenSingleton.GetInstance().ThrowIfCancellationRequested();
                lst = securityDayQuotationFromWangYIRepository.GetSSEDayQuotationFromWangYi(securityTask.SecurityCode,securityTask.BeginDate,securityTask.EndDate);
                dbContext.BeginTransaction();
                foreach(SecurityDayQuotationDomain securityDayQuotation in lst)
                {
                    securityDayQuotationRepository.InsertIntoDB(securityDayQuotation);
                }
                securityTask.IsFinished = true;
                securityTaskRepository.UpdateTaskStatus(securityTask);
                nextSecurityTask = securityTask.BuildNextTask();
                securityTaskRepository.InsertIntoDB(nextSecurityTask);
                dbContext.CommitTransaction();
                securityTask = securityTaskEngine.Pop();
            }
        }
    }
}