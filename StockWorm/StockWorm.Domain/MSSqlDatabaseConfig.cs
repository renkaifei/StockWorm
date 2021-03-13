using System;
using Newtonsoft.Json;
using System.Xml;
using System.Reflection;
using System.IO;

namespace StockWorm.Domain
{
    public class MSSqlDatabaseConfig : DatabaseConfig
    {
        public MSSqlDatabaseConfig()
        {
            DatabaseType = "mssql";
        }
        public string InitialCatelog { get; set; }
        public string UserID { get; set; }
        public string Password { get; set; }
        public string DatabasePath { get; set; }

        public override void Save()
        {
            XmlDocument doc = new XmlDocument();
            XmlElement rootElem = doc.CreateElement("Database");
            doc.AppendChild(rootElem);
            XmlElement datasourceElem = doc.CreateElement("DataSource");
            datasourceElem.InnerText = DataSource;
            rootElem.AppendChild(datasourceElem);
            XmlElement initialCatelogElem = doc.CreateElement("InitialCalog");
            initialCatelogElem.InnerText = InitialCatelog;
            rootElem.AppendChild(initialCatelogElem);
            XmlElement userIDElem = doc.CreateElement("UserID");
            userIDElem.InnerText = UserID;
            rootElem.AppendChild(userIDElem);
            XmlElement passwordElem = doc.CreateElement("Password");
            passwordElem.InnerText = Password;
            rootElem.AppendChild(passwordElem);
            XmlElement databasePathElem = doc.CreateElement("DatabasePath");
            databasePathElem.InnerText = DatabasePath;
            rootElem.AppendChild(databasePathElem);
            XmlElement databaseTypeElem = doc.CreateElement("Type");
            databaseTypeElem.InnerText = "mssql";
            rootElem.AppendChild(databaseTypeElem);
            string filePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\" + ConfigFileName;
            doc.Save(filePath);
        }
    }
}