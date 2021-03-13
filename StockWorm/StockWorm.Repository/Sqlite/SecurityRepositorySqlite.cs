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

namespace StockWorm.Repository.Sqlite
{
    //证券仓库
    public class SecurityRepositorySqlite:SecurityRepository
    {
        private DatabaseContext dbContext;

        public SecurityRepositorySqlite()
        {
            DatabaseContextFactory factory = new DatabaseContextFactory();
            dbContext = factory.CreateDatabaseContext("sqlite");
        }

        public SecurityRepositorySqlite(DatabaseContext dbContext) : this()
        {
            this.dbContext = dbContext;
        }

        public override List<SecurityDomain> GetList()
        {
            List<SecurityDomain> securities = new List<SecurityDomain>();
            SecurityDomain security;
            string sql = "SELECT SecurityCode FROM Security";
            dbContext.ExecuteDataReader(reader =>
            {
                while (reader.Read())
                {
                    security = new SecurityDomain();
                    security.SecurityCode = reader.GetString(0);
                    securities.Add(security);
                }
            }, sql);
            return securities;
        }

        public override void InsertIntoDB(List<SecurityDomain> securities)
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
                dbContext.ExecuteNoQuery(insertSQL, prmCompanyCode, prmCompanyAddr, prmSecurityCode,
                prmSecurityAbbr, prmListingDate, prmExchangeMarket);
            }
        }

        public override void InsertIntoDB(SecurityDomain security)
        {
            string insertSQL = "INSERT INTO Security(SecurityCode,SecurityAbbr,CompanyAbbr,ListingDate,CompanyCode,ExchangeMarket)VALUES(@SecurityCode,@SecurityAbbr,@CompanyAbbr,@ListingDate,@CompanyCode,@ExchangeMarket)";
            SqliteParameter prmCompanyCode = new SqliteParameter("@CompanyCode", DbType.String) { Value = security.CompanyCode };
            SqliteParameter prmCompanyAddr = new SqliteParameter("@CompanyAbbr", DbType.String) { Value = security.CompanyAbbr };
            SqliteParameter prmSecurityCode = new SqliteParameter("@SecurityCode", DbType.String) { Value = security.SecurityCode };
            SqliteParameter prmSecurityAbbr = new SqliteParameter("@SecurityAbbr", DbType.String) { Value = security.SecurityAbbr };
            SqliteParameter prmListingDate = new SqliteParameter("@ListingDate", DbType.DateTime) { Value = security.ListingDate };
            SqliteParameter prmExchangeMarket = new SqliteParameter("@ExchangeMarket", DbType.String) { Value = security.ExchangeMarket };
            dbContext.ExecuteNoQuery(insertSQL, prmCompanyCode, prmCompanyAddr, prmSecurityCode,
            prmSecurityAbbr, prmListingDate, prmExchangeMarket);
        }
    
        public override List<SecurityDomain> GetListInPage(int pageIndex,int pageSize,string exchange)
        {
            List<SecurityDomain> lst = new List<SecurityDomain>();
            SecurityDomain tempSecurity;
            string sql = string.Format("SELECT SecurityID,SecurityCode,SecurityAbbr,CompanyCode,CompanyAbbr,ListingDate,ExchangeMarket FROM Security Where exchangeMarket = @ExchangeMarket Limit {0} Offset {1} ",pageSize,(pageIndex -1) * pageSize);
            SqliteParameter prmExchange = new SqliteParameter("@ExchangeMarket",DbType.String){ Value = exchange };
            dbContext.ExecuteDataReader(reader =>{
                while(reader.Read())
                {
                    tempSecurity = new SecurityDomain();
                    tempSecurity.SecurityID = reader.GetInt32(0);
                    tempSecurity.SecurityCode = reader.GetString(1);
                    tempSecurity.SecurityAbbr = reader.GetString(2);
                    tempSecurity.CompanyCode = reader.GetString(3);
                    tempSecurity.CompanyAbbr = reader.GetString(4);
                    tempSecurity.ListingDate = reader.GetDateTime(5);
                    tempSecurity.ExchangeMarket = reader.GetString(6);
                    lst.Add(tempSecurity);
                }
            },sql,prmExchange);
            return lst;
        }
    }
}