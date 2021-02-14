using System.ComponentModel;
using System;

namespace StockWorm.Domain
{
    public class SecurityTaskDomain
    {
        private long taskID = 0;
        [Description("任务ID")]
        public long TaskID
        {
            get { return taskID; }
            set
            {
                if(taskID == value) return;
                taskID = value;
            }
        }

        private string securityCode = "";
        [Description("证券代码")]
        public string SecurityCode
        {
            get { return securityCode; }
            set
            {
                if(securityCode == value) return;
                securityCode = value;
            }
        }

        private string exchangeMarket = "";
        [Description("交易市场")]
        public string ExchangeMarket
        {
            get { return exchangeMarket; }
            set
            {
                if(exchangeMarket == value) return;
                exchangeMarket = value;
            }
        }

        private DateTime beginDate = new DateTime(1900,1,1);
        [Description("起始日期")]
        public DateTime BeginDate
        {
            get { return beginDate; }
            set
            {
                if(beginDate == value) return;
                beginDate = value;
            }
        }

        private DateTime endDate = new DateTime(1900,1,1);
        [Description("结束日期")]
        public DateTime EndDate
        {
            get { return endDate; }
            set
            {
                if(endDate == value) return;
                endDate = value;
            }
        }

        private bool isFinished = false;
        [Description("是否完成")]
        public bool IsFinished
        {
            get { return isFinished; }
            set
            {
                if(isFinished == value) return;
                isFinished = value;
            }
        }

        public bool ExistsInDB()
        {
            return taskID != 0;
        }
   
        public bool IsLast()
        {
            return BeginDate == EndDate && BeginDate.Date == DateTime.Now.AddDays(-1).Date;
        }

        public bool IsFuture()
        {
            return BeginDate == EndDate && BeginDate.Date == DateTime.Now.Date;
        }
    
        public bool IsEmpty()
        {
            return taskID == 0 && string.IsNullOrEmpty(securityCode);
        }
    
        public SecurityTaskDomain BuildNextTask()
        {
            SecurityTaskDomain tempTask = new SecurityTaskDomain();
            tempTask.SecurityCode = securityCode;
            tempTask.ExchangeMarket = exchangeMarket;
            tempTask.BeginDate = endDate.AddDays(1);
            tempTask.EndDate = tempTask.BeginDate.AddMonths(1);
            if (tempTask.EndDate >= DateTime.Now.Date)
            {
                tempTask.EndDate = DateTime.Now.AddDays(-1).Date;
            }
            if(tempTask.BeginDate > tempTask.EndDate) tempTask.EndDate = tempTask.BeginDate;
            tempTask.IsFinished = false;
            return tempTask;
        }
    }
}