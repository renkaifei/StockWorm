using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockWorm.Application.Service;
using System;

namespace StockWorm.UnitTest
{
    [TestClass]
    public class UnitTestSecurityDatabase
    {
        [TestMethod]
        public void CreateDataBase()
        {
            try
            {
                SecurityDatabaseService service= new SecurityDatabaseService();
                service.CreateDatabase("172_17_0_13","testSecurity","sa","798046819@qqcom","C:\\renkf\\database","mssql");
                Assert.IsTrue(1 ==1,"MSSQL数据库创建成功");
                service.CreateDatabase("","testSecurity","","798046819@qqcom","C:\\renkf\\database","sqlite");
                Assert.IsTrue(1 ==1,"Sqlite数据库创建成功");
            }
            catch(Exception ex)
            {
                Assert.IsTrue(1 ==0,ex.Message);
            }
        }
    }
}