using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockWorm.Repository;
using StockWorm.Domain;
using System.Collections.Generic;

namespace StockWorm.UnitTest
{
    [TestClass]
    public class UnitTestSecurityTask
    {
        [TestMethod]
        public void SyncOneTaskSecurityDayQuotationFromWangYI()
        {
            SecurityTaskRepository repository = new SecurityTaskRepository();
            SecurityTaskDomain securityTask = repository.GetOneUnFinishedTask();
            SecurityDayQuotationRepository dayQuotationrepository = new SecurityDayQuotationRepository();
            
            List<SecurityDayQuotationDomain> lstDayQuotation = dayQuotationrepository.GetSSEDayQuotationFromWangYi(securityTask.SecurityCode,securityTask.BeginDate,securityTask.EndDate);
            dayQuotationrepository.InsertIntoDB(lstDayQuotation);
        }
    }
}