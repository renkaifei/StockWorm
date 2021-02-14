using StockWorm.Domain;
using StockWorm.Repository;
using System.Collections.Generic;
using StockWorm.Domain.Factory;
using StockWorm.Repository.Context;

namespace StockWorm.Application.Service
{
    public class SecurityDayQuotationService
    {

        public SecurityDayQuotationService()
        {
            
        }

        public void SyncSSEDayQuotationFromWangYI()
        {
            SqliteDatabaseContext sqliteDb = new SqliteDatabaseContext();
            SecurityDayQuotationRepository securityDayQuotationRepository = new SecurityDayQuotationRepository(sqliteDb);
            SecurityTaskRepository securityTaskRepository = new SecurityTaskRepository(sqliteDb);
            SecurityTaskEngine securityTaskEngine = new SecurityTaskEngine();
            SecurityTaskDomain securityTask = securityTaskEngine.Pop();
            List<SecurityDayQuotationDomain> lst = new List<SecurityDayQuotationDomain>();
            SecurityTaskDomain nextSecurityTask;
            while(!securityTask.IsEmpty())
            {
                lst = securityDayQuotationRepository.GetSSEDayQuotationFromWangYi(securityTask.SecurityCode,securityTask.BeginDate,securityTask.EndDate);
                sqliteDb.BeginTransaction();
                foreach(SecurityDayQuotationDomain securityDayQuotation in lst)
                {
                    securityDayQuotationRepository.InsertIntoDB(securityDayQuotation);
                }
                securityTask.IsFinished = true;
                securityTaskRepository.UpdateTaskStatus(securityTask);
                nextSecurityTask = securityTask.BuildNextTask();
                securityTaskRepository.InsertIntoDB(nextSecurityTask);

                sqliteDb.CommitTransaction();
                securityTask = securityTaskEngine.Pop();
            }
        }
    }
}