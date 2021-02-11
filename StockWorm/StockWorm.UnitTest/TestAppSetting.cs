using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockWorm.Utils;
using System;

namespace StockWorm.UnitTest
{
    [TestClass]
    public class TestAppSetting
    {
        [TestMethod]
        public void ConnectionString()
        {
            try
            {
                string connectionString = AppSetting.GetInstance().GetSqliteConnectionString();
                Assert.IsTrue(!string.IsNullOrEmpty(connectionString),"连接字符串为空");
            }
            catch(Exception ex)
            {
                Assert.IsTrue(1 == 0,ex.Message);
            }
            
        }
    }
}