using StockWorm.Repository.Context;
using StockWorm.Domain;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace StockWorm.Repository.MSSql
{
    //证券仓库
    public class SecurityRepositoryMSSql:SecurityRepository
    {
        private DatabaseContext dbContext;

        public SecurityRepositoryMSSql()
        {
            dbContext = DatabaseContextFactory.GetInstance().CreateDatabaseContext("mssql");
        }

        public SecurityRepositoryMSSql(DatabaseContext dbContext) : this()
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
                SqlParameter prmCompanyCode = new SqlParameter("@CompanyCode", SqlDbType.VarChar,10) { Value = security.CompanyCode };
                SqlParameter prmCompanyAddr = new SqlParameter("@CompanyAbbr", SqlDbType.VarChar,128) { Value = security.CompanyAbbr };
                SqlParameter prmSecurityCode = new SqlParameter("@SecurityCode", SqlDbType.VarChar,128) { Value = security.SecurityCode };
                SqlParameter prmSecurityAbbr = new SqlParameter("@SecurityAbbr", SqlDbType.VarChar,128) { Value = security.SecurityAbbr };
                SqlParameter prmListingDate = new SqlParameter("@ListingDate", SqlDbType.DateTime) { Value = security.ListingDate };
                SqlParameter prmExchangeMarket = new SqlParameter("@ExchangeMarket", SqlDbType.VarChar,10) { Value = security.ExchangeMarket };
                dbContext.ExecuteNoQuery(insertSQL, prmCompanyCode, prmCompanyAddr, prmSecurityCode,
                prmSecurityAbbr, prmListingDate, prmExchangeMarket);
            }
        }

        public override void InsertIntoDB(SecurityDomain security)
        {
            string insertSQL = "INSERT INTO Security(SecurityCode,SecurityAbbr,CompanyAbbr,ListingDate,CompanyCode,ExchangeMarket)VALUES(@SecurityCode,@SecurityAbbr,@CompanyAbbr,@ListingDate,@CompanyCode,@ExchangeMarket)";
            SqlParameter prmCompanyCode = new SqlParameter("@CompanyCode", SqlDbType.VarChar,10) { Value = security.CompanyCode };
            SqlParameter prmCompanyAddr = new SqlParameter("@CompanyAbbr", SqlDbType.VarChar,128) { Value = security.CompanyAbbr };
            SqlParameter prmSecurityCode = new SqlParameter("@SecurityCode", SqlDbType.VarChar,128) { Value = security.SecurityCode };
            SqlParameter prmSecurityAbbr = new SqlParameter("@SecurityAbbr", SqlDbType.VarChar,128) { Value = security.SecurityAbbr };
            SqlParameter prmListingDate = new SqlParameter("@ListingDate", SqlDbType.DateTime) { Value = security.ListingDate };
            SqlParameter prmExchangeMarket = new SqlParameter("@ExchangeMarket", SqlDbType.VarChar,10) { Value = security.ExchangeMarket };
            dbContext.ExecuteNoQuery(insertSQL, prmCompanyCode, prmCompanyAddr, prmSecurityCode,
            prmSecurityAbbr, prmListingDate, prmExchangeMarket);
        }
        public override List<SecurityDomain> GetListInPage(int pageIndex,int pageSize,string exchange)
        {
            List<SecurityDomain> lst = new List<SecurityDomain>();
            SecurityDomain tempSecurity;
            string sql = string.Format("SELECT * FROM (SELECT SecurityID,SecurityCode,SecurityAbbr,CompanyCode,CompanyAbbr,ListingDate,ExchangeMarket,ROW_NUMBER() over(Order by SecurityID) rowNum FROM Security Where exchangeMarket = @ExchangeMarket) A WHERE A.rowNum Between {0} And {1} ",(pageIndex -1) * pageSize + 1,pageIndex * pageSize);
            SqlParameter prmExchange = new SqlParameter("@ExchangeMarket",SqlDbType.VarChar,10){ Value = exchange };
            dbContext.ExecuteDataReader(reader =>{
                while(reader.Read())
                {
                    tempSecurity = new SecurityDomain();
                    tempSecurity.SecurityID = reader.GetInt64(0);
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