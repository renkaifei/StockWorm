
namespace StockWorm.Domain
{
    public abstract class DatabaseConfig
    {
        public int ID { get; set; }
        public string DataSource { get; set; }
        public string DatabaseType { get;set; }
        public readonly string ConfigFileName = "dbConfig.xml";
        public abstract void Save();
    }
}