using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockWorm.Domain;
using System.Collections.Generic;
using StockWorm.Application.Service;
using System;

namespace StockWorm.UnitTest
{
    [TestClass]
    public class UnitTestRealTimeStockTransaction
    {
        [TestMethod]
        public void GetList()
        {
            try
            {
                string codes = "sh600000";
                RealTimeStockTransactionService service = new RealTimeStockTransactionService();
                List<RealTimeStockTransaction> lst = service.GetList(codes);
                Assert.IsTrue(lst.Count > 0,"没有查询到对应的信息");
            }
            catch(Exception ex)
            {
                Assert.IsTrue(1 == 0,ex.Message);
            }
            
        }
    }
}