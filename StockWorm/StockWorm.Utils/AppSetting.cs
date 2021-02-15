using System.Reflection;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

namespace StockWorm.Utils
{
    public class AppSetting
    {
        private static AppSetting setting;
        private static object obj = new object();
        private JObject configObj = null;
        private string sqliteConnectionString = "";
        private string mssqlConnectionString = "";
        private string dbType = "";

        private AppSetting()
        {
            string dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string settingFile = string.Format("{0}\\setting.json",dir);
            if(!File.Exists(settingFile)) throw new FileNotFoundException("系统配置文件不存在");
            string tempJson = "";
            using(FileStream fs = File.Open(settingFile,FileMode.Open,FileAccess.Read,FileShare.None))
            {
                using(StreamReader sr = new StreamReader(fs))
                {
                   tempJson = sr.ReadToEnd();
                }
            }

            tempJson = Regex.Replace(tempJson,"[\\r\\t\\n]","");
            configObj = JsonConvert.DeserializeObject<JObject>(tempJson);
        }

        public static AppSetting GetInstance()
        {
            if (setting == null)
            {
                lock (obj)
                {
                    if (setting == null)
                    {
                        setting = new AppSetting();
                    }
                }
            }
            return setting;
        }

        
        public string GetSqliteConnectionString()
        {
            if(!string.IsNullOrEmpty(sqliteConnectionString))
            {
                return sqliteConnectionString;
            }
            else
            {
                sqliteConnectionString = configObj.SelectToken("sqliteConnectionString").ToObject<string>();
                return sqliteConnectionString;
            } 
        }

        public string GetMSSqlConnectionString()
        {
            if(!string.IsNullOrEmpty(mssqlConnectionString))
            {
                return mssqlConnectionString;
            }
            else
            {
                mssqlConnectionString = configObj.SelectToken("mssqlConnectionString").ToObject<string>();
                return mssqlConnectionString;
            }
        }

        public string GetDatabaseType()
        {
            if(!string.IsNullOrEmpty(dbType))
            {
                return dbType;
            }
            else
            {
                dbType = configObj.SelectToken("dbType").ToObject<string>();
                return dbType;
            } 
        }
    }
}