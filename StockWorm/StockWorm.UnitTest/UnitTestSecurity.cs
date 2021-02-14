using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockWorm.Domain;
using System.Collections.Generic;
using StockWorm.Application.Service;
using System;
using StockWorm.Domain.Interfaces;

namespace StockWorm.UnitTest
{
    [TestClass]
    public class UnitTestSecurity
    {
        [TestInitialize]
        public void SetUp()
        {
            
        }

        [TestMethod]
        public void SyncSecuritiesFromSSE()
        {
            try
            {
                SecurityService securityService = new SecurityService();
                securityService.SyncSecuritiesFromSSE();
                //SecurityDayQuotationService dayQuotationService = new SecurityDayQuotationService();
                //dayQuotationService.SyncSSEDayQuotationFromWangYI();
            }
            catch(Exception ex)
            {
                Assert.IsTrue(1 == 0 ,ex.Message);
            }   
        }
    }
}
