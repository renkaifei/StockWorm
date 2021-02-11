using System;

namespace StockWorm.Domain.Factory
{
    public class SecurityTaskFactory
    {
        public SecurityTaskDomain Create()
        {
            SecurityTaskDomain securityTask = new SecurityTaskDomain();
            return securityTask;
        }

        public SecurityTaskDomain CreateNextTask(SecurityTaskDomain securityTask)
        {
            SecurityTaskDomain nextTask = new SecurityTaskDomain();
            nextTask.SecurityCode = securityTask.SecurityCode ;
            nextTask.BeginDate = securityTask.EndDate;
            nextTask.EndDate = securityTask.EndDate.AddMonths(1);
            if(nextTask.EndDate.Date > DateTime.Now.AddDays(-1).Date) nextTask.EndDate = DateTime.Now.AddDays(-1).Date;
            return nextTask;
        }

        public SecurityTaskDomain CreateFutureTask(SecurityTaskDomain securityTask)
        {
            if(securityTask.IsLast()) throw new Exception("创建未来任务的当前任务必须是最后的任务");
            SecurityTaskDomain futureTask = new SecurityTaskDomain(); 
            futureTask.SecurityCode = securityTask.SecurityCode;
            futureTask.BeginDate = securityTask.BeginDate.AddDays(1);
            futureTask.EndDate = securityTask.EndDate.AddDays(1);
            return futureTask;
        }
    }
}