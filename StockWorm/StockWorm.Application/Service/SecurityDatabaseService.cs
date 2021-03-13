using StockWorm.Repository.Context;
using StockWorm.Domain;

namespace StockWorm.Application.Service
{
    public class SecurityDatabaseService
    {
        public void CreateDatabase(DatabaseConfig config)
        {
            DatabaseContextFactory factory = new DatabaseContextFactory();
            DatabaseContext dbContext = factory.CreateDatabaseContext(config.DatabaseType);
            dbContext.CreateDatabase(config);
            config.Save();
        }
    }
}