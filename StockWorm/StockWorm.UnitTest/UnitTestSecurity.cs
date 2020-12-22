using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockWorm.Domain;
using System.Collections.Generic;
using StockWorm.Application;

namespace StockWorm.UnitTest
{
    [TestClass]
    public class UnitTestSecurity
    {
        [TestMethod]
        public void GetListFromSSE()
        {
            SecurityService service = new SecurityService();
            List<SecurityDomain> securities = service.GetListFromSSE();
            Assert.IsTrue(securities.Count > 0 ,"没有获取到证券信息");
        }

        [TestMethod]
        public void SaveStockFromSSEToSqlite()
        {
            SecurityService service = new SecurityService();
            service.SaveSecuritiesFromSSEToSqlite();
        }
    }
}
