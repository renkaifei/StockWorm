using StockWorm.Repository.Context;

namespace StockWorm.Application.Service
{
    public class SecurityDatabaseService
    {
        public void CreateDatabase(string source,string databaseName,string userID,string pwd,string databasePath,string dbType)
        {
            DatabaseContext dbContext = DatabaseContextFactory.GetInstance().CreateDatabaseContext(dbType);
            dbContext.CreateDatabase(source,databaseName,userID,pwd,databasePath);
        }
    }
}