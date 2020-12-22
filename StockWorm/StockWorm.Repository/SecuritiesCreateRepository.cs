using System.Collections.Generic;
using StockWorm.Domain;
using StockWorm.Domain.Interfaces;
using System.Text;
using Microsoft.Data.Sqlite;
using System.Data;
using StockWorm.Repository.Context;
using StockWorm.Repository.Interfaces;

namespace StockWorm.Repository
{
    internal class SecuritiesCreateRepository:SqliteDatabaseContext,ICreateDataBehavior
    {
        private List<SecurityDomain> values;
        public SecuritiesCreateRepository(List<SecurityDomain> values)
        {
            this.values = values;
        }

        public void CreateData()
        {
            StringBuilder sb = new StringBuilder();
            int i =0;
            List<SqliteParameter> lstParam = new List<SqliteParameter>();
            string sql = "";
            foreach(SecurityDomain security in values)
            {
                sql = string.Format("insert into Security(CompanyCode,CompanyAbbr,SecurityCode,SecurityAbbr,ListingDate)values(@CompanyCode_{0},@CompanyAbbr_{0},@SecurityCode_{0},@SecurityAbbr_{0},@ListingDate_{0});",i);
                lstParam.Add(new SqliteParameter(string.Format("@CompanyCode_{0}",i),DbType.String){ Value = security.CompanyCode });
                lstParam.Add(new SqliteParameter(string.Format("@CompanyAbbr_{0}",i),DbType.String){ Value = security.CompanyAbbr });
                lstParam.Add(new SqliteParameter(string.Format("@SecurityCode_{0}",i),DbType.String){ Value = security.SecurityCode });
                lstParam.Add(new SqliteParameter(string.Format("@SecurityAbbr_{0}",i),DbType.String){ Value = security.SecurityAbbr });
                lstParam.Add(new SqliteParameter(string.Format("@ListingDate_{0}",i),DbType.DateTime){ Value = security.ListingDate });
                ExecuteNoQuery(sql,lstParam.ToArray());
                i++;
                lstParam.Clear();
            }
        }
    

    }
}