using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockWorm.Application.Service;
using System;

[TestClass]
public class UnitTestSecurityDayQuotation
{
    [TestMethod]
    public void SyncSecurityDayQutationFromWangYI()
    {
        try
        {
            SecurityDayQuotationService service = new SecurityDayQuotationService();
            service.SyncSSEDayQuotationFromWangYI();
        }
        catch(Exception ex)
        {
            Assert.IsTrue(1 == 0,ex.Message);
        }
        
    }
}