using System;
using System.Xml;
using System.Reflection;
using System.IO;

namespace StockWorm.Domain
{
    public class SqliteDatabaseConfig:DatabaseConfig
    {
        public SqliteDatabaseConfig()
        {
            DatabaseType = "sqlite";
        }
        
        public override void Save()
        {
            XmlDocument doc = new XmlDocument();
            XmlElement rootElem = doc.CreateElement("Database");
            doc.AppendChild(rootElem);
            XmlElement dataSourceElem = doc.CreateElement("DataSource");
            dataSourceElem.InnerText = DataSource ;
            rootElem.AppendChild(dataSourceElem);
            string filePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\" + ConfigFileName;
            doc.Save(filePath);
        }
    }
}
