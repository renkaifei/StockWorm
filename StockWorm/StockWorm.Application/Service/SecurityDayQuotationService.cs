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
            SecurityTaskDomain securityTask = GetOneUnFinishedTask();
            while (!securityTask.IsFuture())
            {
                List<SecurityDayQuotationDomain> securityDayQuotations = GetSecurityDayQuotations(securityTask);
                SqliteDatabaseContext sqliteDb = new SqliteDatabaseContext();
                SecurityDayQuotationRepository securityDayQuotationRepository = new SecurityDayQuotationRepository(sqliteDb);
                SecurityTaskRepository securityTaskRepository = new SecurityTaskRepository(sqliteDb);
                sqliteDb.BeginTransaction();
                if(securityDayQuotations.Count > 0)
                {
                    securityDayQuotationRepository.InsertIntoDB(securityDayQuotations);
                }
                securityTask.IsFinished = true;
                securityTaskRepository.UpdateTaskStatus(securityTask);
                SecurityTaskDomain nextSecurityTask = CreateNextTask(securityTask);
                if(!nextSecurityTask.IsLast())
                {
                    securityTaskRepository.InsertIntoDB(nextSecurityTask);
                }
                else
                {
                    SecurityTaskDomain futureSecurityTask = CreateFutureTask(nextSecurityTask);
                    securityTaskRepository.InsertIntoDB(futureSecurityTask);
                }
                sqliteDb.CommitTransaction();
                securityTask = GetOneUnFinishedTask();
            }
        }

        private List<SecurityDayQuotationDomain> GetSecurityDayQuotations(SecurityTaskDomain securityTask)
        {
            SecurityDayQuotationRepository securityDayQuotationRepository = new SecurityDayQuotationRepository();
            List<SecurityDayQuotationDomain> securityDayQuotations = securityDayQuotationRepository.GetSSEDayQuotationFromWangYi(securityTask.SecurityCode, securityTask.BeginDate,
            securityTask.EndDate);
            return securityDayQuotations;
        }

        private SecurityTaskDomain GetOneUnFinishedTask()
        {
            SecurityTaskFactory securityTaskFactory = new SecurityTaskFactory();
            SecurityTaskDomain securityTask = securityTaskFactory.Create();
            SecurityTaskRepository securityTaskRepository = new SecurityTaskRepository();
            securityTask = securityTaskRepository.GetOneUnFinishedTask();
            return securityTask;
        }

        private SecurityTaskDomain CreateNextTask(SecurityTaskDomain securityTask)
        {
            SecurityTaskFactory securityTaskFactory = new SecurityTaskFactory();
            SecurityTaskDomain tempSecurityTask = securityTaskFactory.CreateNextTask(securityTask);
            return tempSecurityTask;
        }

        private SecurityTaskDomain CreateFutureTask(SecurityTaskDomain securityTask)
        {
            SecurityTaskFactory securityTaskFactory = new SecurityTaskFactory();
            SecurityTaskDomain tempSecurityTask = securityTaskFactory.CreateFutureTask(securityTask);
            return tempSecurityTask;
        }

    }
}