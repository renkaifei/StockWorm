using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockWorm.Application.Service;
using System;
using StockWorm.Domain;

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
                MSSqlDatabaseConfig mssqlConfig = new MSSqlDatabaseConfig();
                mssqlConfig.DataSource = "172_17_0_13";
                mssqlConfig.InitialCatelog = "testSecurity";
                mssqlConfig.UserID = "sa";
                mssqlConfig.Password = "798046819@qqcom";
                mssqlConfig.DatabasePath = "C:\\renkf\\database";
                service.CreateDatabase(mssqlConfig);
                Assert.IsTrue(1 ==1,"MSSQL数据库创建成功");
                SqliteDatabaseConfig sqliteConfig = new SqliteDatabaseConfig();
                sqliteConfig.DataSource = "C:\\renkf\\database\\text.db";
                service.CreateDatabase(sqliteConfig);
                Assert.IsTrue(1 ==1,"Sqlite数据库创建成功");
            }
            catch(Exception ex)
            {
                Assert.IsTrue(1 ==0,ex.Message);
            }
        }
    }
}