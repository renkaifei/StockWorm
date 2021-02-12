using StockWorm.Repository.Context;
using System;
using StockWorm.Repository.Net;
using StockWorm.Domain;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using System.Data;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System.Text;

namespace StockWorm.Repository
{
    //证券仓库
    public class SecurityRepository
    {
        private SqliteDatabaseContext sqliteDb;

        public SecurityRepository()
        {
            sqliteDb = new SqliteDatabaseContext();
        }

        public SecurityRepository(SqliteDatabaseContext sqliteDb) : this()
        {
            this.sqliteDb = sqliteDb;
        }

        public List<SecurityDomain> GetSecuritiesFromSSE()
        {
            List<SecurityDomain> securities = new List<SecurityDomain>();
            int pageIndex = 1;
            int pageSize = 1000;
            List<SecurityDomain> securitiesInPage = GetSecuritiesInPageFromSSE(pageIndex, pageSize);
            while (securitiesInPage.Count > 0)
            {
                securities.AddRange(securitiesInPage);
                pageIndex++;
                securitiesInPage = GetSecuritiesInPageFromSSE(pageIndex, pageSize);
            }
            return securities;
        }

        public List<SecurityDomain> GetSecuritiesFromDB()
        {
            List<SecurityDomain> securitiesFromDB = new List<SecurityDomain>();
            SecurityDomain security;
            string sql = "SELECT SecurityCode FROM Security";
            sqliteDb.ExecuteDataReader(reader =>
            {
                while (reader.Read())
                {
                    security = new SecurityDomain();
                    security.SecurityCode = reader.GetString(0);
                    securitiesFromDB.Add(security);
                }
            }, sql);
            return securitiesFromDB;
        }

        public void InsertIntoDB(List<SecurityDomain> securities)
        {
            string insertSQL = "INSERT INTO Security(SecurityCode,SecurityAbbr,CompanyAbbr,ListingDate,CompanyCode,ExchangeMarket)VALUES(@SecurityCode,@SecurityAbbr,@CompanyAbbr,@ListingDate,@CompanyCode,@ExchangeMarket)";
            foreach (SecurityDomain security in securities)
            {
                SqliteParameter prmCompanyCode = new SqliteParameter("@CompanyCode", DbType.String) { Value = security.CompanyCode };
                SqliteParameter prmCompanyAddr = new SqliteParameter("@CompanyAbbr", DbType.String) { Value = security.CompanyAbbr };
                SqliteParameter prmSecurityCode = new SqliteParameter("@SecurityCode", DbType.String) { Value = security.SecurityCode };
                SqliteParameter prmSecurityAbbr = new SqliteParameter("@SecurityAbbr", DbType.String) { Value = security.SecurityAbbr };
                SqliteParameter prmListingDate = new SqliteParameter("@ListingDate", DbType.DateTime) { Value = security.ListingDate };
                SqliteParameter prmExchangeMarket = new SqliteParameter("@ExchangeMarket", DbType.String) { Value = security.ExchangeMarket };
                sqliteDb.ExecuteNoQuery(insertSQL, prmCompanyCode, prmCompanyAddr, prmSecurityCode,
                prmSecurityAbbr, prmListingDate, prmExchangeMarket);
            }
        }

        public void InsertIntoDB(SecurityDomain security)
        {
            string insertSQL = "INSERT INTO Security(SecurityCode,SecurityAbbr,CompanyAbbr,ListingDate,CompanyCode,ExchangeMarket)VALUES(@SecurityCode,@SecurityAbbr,@CompanyAbbr,@ListingDate,@CompanyCode,@ExchangeMarket)";
            SqliteParameter prmCompanyCode = new SqliteParameter("@CompanyCode", DbType.String) { Value = security.CompanyCode };
            SqliteParameter prmCompanyAddr = new SqliteParameter("@CompanyAbbr", DbType.String) { Value = security.CompanyAbbr };
            SqliteParameter prmSecurityCode = new SqliteParameter("@SecurityCode", DbType.String) { Value = security.SecurityCode };
            SqliteParameter prmSecurityAbbr = new SqliteParameter("@SecurityAbbr", DbType.String) { Value = security.SecurityAbbr };
            SqliteParameter prmListingDate = new SqliteParameter("@ListingDate", DbType.DateTime) { Value = security.ListingDate };
            SqliteParameter prmExchangeMarket = new SqliteParameter("@ExchangeMarket", DbType.String) { Value = security.ExchangeMarket };
            sqliteDb.ExecuteNoQuery(insertSQL, prmCompanyCode, prmCompanyAddr, prmSecurityCode,
            prmSecurityAbbr, prmListingDate, prmExchangeMarket);
        }

        private bool CheckUnique(SecurityDomain security)
        {
            string sql = "SELECT 1 FROM Security WHERE SecurityCode = @SecurityCode";
            SqliteParameter prmSecurityCode = new SqliteParameter("@SecurityCode", DbType.String) { Value = security.SecurityCode };
            SqliteParameter prmExchangeMarket = new SqliteParameter("@ExchangeMarket", DbType.String) { Value = security.ExchangeMarket };
            bool result = false;
            sqliteDb.ExecuteDataReader(reader =>
            {
                result = !reader.HasRows;
            }, sql, prmSecurityCode);
            return result;
        }

        private List<SecurityDomain> GetSecuritiesInPageFromSSE(int pageIndex, int pageSize)
        {
            HttpWebClient client = new HttpWebClient();
            client.URL = "http://query.sse.com.cn/security/stock/getStockListData2.do";
            client.Headers.Add("Accept", "*/*");
            client.Headers.Add("Accept-Encoding", "gzip, deflate");
            client.Headers.Add("Accept-Language", "zh-CN,q=0.9;");
            client.Headers.Add("Connection", "keep-alive");
            client.Headers.Add("Host", "query.sse.com.cn");
            client.Headers.Add("Referer", "http://www.sse.com.cn/");
            client.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.3; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36 Edg/87.0.664.66");
            Random random = new Random();
            client.Data = string.Format("&jsonCallBack=jsonpCallback{0}&isPagination=true&stockCode=&csrcCode=&areaName=&stockType=1&pageHelp.cacheSize=1&pageHelp.beginPage={1}&pageHelp.pageSize={2}&pageHelp.pageNo={1}&_={3}",
                random.Next(100000), pageIndex, pageSize, DateTime.Now.Subtract(new DateTime(1970, 1, 1, 8, 0, 0)).Ticks);
            string result = client.SendGetRequest();
            Match match = Regex.Match(result, "\"result\":\\[([\\S\\s]*)\\]");
            List<SecurityDomain> values = new List<SecurityDomain>();
            if (match.Success)
            {
                List<SecurityDomain> lst = JsonConvert.DeserializeObject<List<SecurityDomain>>(string.Format("[{0}]", match.Groups[1].Value));
                foreach (SecurityDomain security in lst)
                {
                    security.ExchangeMarket = "SSE";
                }
                values.AddRange(lst);
            }
            return values;
        }
    }
}