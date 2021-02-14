using System;
using System.Collections.Generic;
using StockWorm.Repository.Net;
using StockWorm.Repository.Context;
using StockWorm.Domain;
using Microsoft.Data.Sqlite;
using System.Data;
using System.Text;

namespace StockWorm.Repository
{
    public class SecurityDayQuotationRepository
    {
        private SqliteDatabaseContext sqliteDb;

        public SecurityDayQuotationRepository()
        {
            this.sqliteDb = new SqliteDatabaseContext();
        }

        public SecurityDayQuotationRepository(SqliteDatabaseContext sqliteDb) : this()
        {
            this.sqliteDb = sqliteDb;
        }

        public List<SecurityDayQuotationDomain> GetSSEDayQuotationFromWangYi(string securityCode, DateTime startDate, DateTime endDate)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            HttpWebClient client = new HttpWebClient();
            client.Encoding = System.Text.Encoding.GetEncoding("GB2312");
            //http://quotes.money.163.com/service/chddata.html?code=0601857&start=20200101&end=20200224&fields=TCLOSE;HIGH;LOW;TOPEN;LCLOSE;CHG;PCHG;TURNOVER;VOTURNOVER;VATURNOVER;TCAP;MCAP
            client.URL = "http://quotes.money.163.com/service/chddata.html";
            client.Data = string.Format("code=0{0}&start={1}&end={2}&fields=TCLOSE;HIGH;LOW;TOPEN;LCLOSE;CHG;PCHG;TURNOVER;VOTURNOVER;VATURNOVER;TCAP;MCAP"
            , securityCode, startDate.ToString("yyyyMMdd"), endDate.ToString("yyyyMMdd"));
            string result = client.SendGetRequest();
            result = result.Replace("'", "");
            string[] arrSecurities = result.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            string[] arrTemp;
            List<SecurityDayQuotationDomain> values = new List<SecurityDayQuotationDomain>();
            SecurityDayQuotationDomain value;
            int count = arrSecurities.Length;
            for (int i = 1; i < arrSecurities.Length - 1; i++)
            {
                arrTemp = arrSecurities[i].Split(',');
                value = new SecurityDayQuotationDomain();
                value.TxDate = Convert.ToDateTime(arrTemp[0]);
                value.SecurityCode = arrTemp[1];
                value.SecurityAbbr = arrTemp[2];
                value.ClosePrice = Convert.ToDouble(arrTemp[3]);
                value.HighPrice = Convert.ToDouble(arrTemp[4]);
                value.LowPrice = Convert.ToDouble(arrTemp[5]);
                value.OpenPrice = Convert.ToDouble(arrTemp[6]);
                value.LastClosePrice = Convert.ToDouble(arrTemp[7]);
                value.PriceChange = arrTemp[8] == "None" ? -10000 : Convert.ToDouble(arrTemp[8]);
                value.Change = arrTemp[9] == "None" ? -10000: Convert.ToDouble(arrTemp[9]);
                value.TurnOver = arrTemp[10] == "None" ? -10000: Convert.ToDouble(arrTemp[10]);
                value.VolumeTurnOver = Convert.ToDouble(arrTemp[11]);
                value.PriceTurnOver = Convert.ToDouble(arrTemp[12]);
                value.MarketValue = Convert.ToDouble(arrTemp[13]);
                value.NegoValue = Convert.ToDouble(arrTemp[14]);
                values.Add(value);
            }
            return values;
        }

        public void InsertIntoDB(List<SecurityDayQuotationDomain> securityDayQuotations)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO SecurityDayQuotation");
            sb.Append("(");
            sb.Append("SecurityCode,TxDate,ClosePrice,HighPrice,LowPrice,OpenPrice,LastClosePrice,PriceChange,Change,TurnOver,VolumeTurnOver,PriceTurnOver,MarketValue,NegoValue");
            sb.Append(")");
            sb.Append("VALUES");
            sb.Append("(");
            sb.Append("@SecurityCode,@TxDate,@ClosePrice,@HighPrice,@LowPrice,@OpenPrice,@LastClosePrice,@PriceChange,@Change,@TurnOver,@VolumeTurnOver,@PriceTurnOver,@MarketValue,@NegoValue");
            sb.Append(")");

            foreach (SecurityDayQuotationDomain dayQuotation in securityDayQuotations)
            {
                SqliteParameter prmSecurityCode = new SqliteParameter("@SecurityCode", DbType.String) { Value = dayQuotation.SecurityCode };
                SqliteParameter prmTxDate = new SqliteParameter("@TxDate", DbType.DateTime) { Value = dayQuotation.TxDate };
                SqliteParameter prmClosePrice = new SqliteParameter("@ClosePrice", dayQuotation.ClosePrice);
                SqliteParameter prmHighPrice = new SqliteParameter("@HighPrice", DbType.Single) { Value = dayQuotation.HighPrice };
                SqliteParameter prmLowPrice = new SqliteParameter("@LowPrice", DbType.Single) { Value = dayQuotation.LowPrice };
                SqliteParameter prmOpenPrice = new SqliteParameter("@OpenPrice", DbType.Single) { Value = dayQuotation.OpenPrice };
                SqliteParameter prmLastClosePrice = new SqliteParameter("@LastClosePrice", DbType.Single) { Value = dayQuotation.LastClosePrice };
                SqliteParameter prmPriceChange = new SqliteParameter("@PriceChange", DbType.Single) { Value = dayQuotation.PriceChange };
                SqliteParameter prmChange = new SqliteParameter("@Change", DbType.Single) { Value = dayQuotation.Change };
                SqliteParameter prmTurnOver = new SqliteParameter("@TurnOver", DbType.Single) { Value = dayQuotation.TurnOver };
                SqliteParameter prmVolumeTurnOver = new SqliteParameter("@VolumeTurnOver", DbType.Single) { Value = dayQuotation.VolumeTurnOver };
                SqliteParameter prmPriceTurnOver = new SqliteParameter("@PriceTurnOver", DbType.Single) { Value = dayQuotation.PriceTurnOver };
                SqliteParameter prmMarketValue = new SqliteParameter("@MarketValue", DbType.Single) { Value = dayQuotation.MarketValue };
                SqliteParameter prmNegoValue = new SqliteParameter("@NegoValue", DbType.Single) { Value = dayQuotation.NegoValue };
                sqliteDb.ExecuteNoQuery(sb.ToString(), prmSecurityCode, prmTxDate, prmClosePrice, prmHighPrice, prmLowPrice, prmOpenPrice, prmLastClosePrice,
                    prmPriceChange, prmChange, prmTurnOver, prmVolumeTurnOver, prmPriceTurnOver, prmMarketValue, prmNegoValue);
            }
        }

        public void InsertIntoDB(SecurityDayQuotationDomain dayQuotation)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO SecurityDayQuotation");
            sb.Append("(");
            sb.Append("SecurityCode,TxDate,ClosePrice,HighPrice,LowPrice,OpenPrice,LastClosePrice,PriceChange,Change,TurnOver,VolumeTurnOver,PriceTurnOver,MarketValue,NegoValue");
            sb.Append(")");
            sb.Append("VALUES");
            sb.Append("(");
            sb.Append("@SecurityCode,@TxDate,@ClosePrice,@HighPrice,@LowPrice,@OpenPrice,@LastClosePrice,@PriceChange,@Change,@TurnOver,@VolumeTurnOver,@PriceTurnOver,@MarketValue,@NegoValue");
            sb.Append(")");
            SqliteParameter prmSecurityCode = new SqliteParameter("@SecurityCode", DbType.String) { Value = dayQuotation.SecurityCode };
            SqliteParameter prmTxDate = new SqliteParameter("@TxDate", DbType.DateTime) { Value = dayQuotation.TxDate };
            SqliteParameter prmClosePrice = new SqliteParameter("@ClosePrice", dayQuotation.ClosePrice);
            SqliteParameter prmHighPrice = new SqliteParameter("@HighPrice", DbType.Single) { Value = dayQuotation.HighPrice };
            SqliteParameter prmLowPrice = new SqliteParameter("@LowPrice", DbType.Single) { Value = dayQuotation.LowPrice };
            SqliteParameter prmOpenPrice = new SqliteParameter("@OpenPrice", DbType.Single) { Value = dayQuotation.OpenPrice };
            SqliteParameter prmLastClosePrice = new SqliteParameter("@LastClosePrice", DbType.Single) { Value = dayQuotation.LastClosePrice };
            SqliteParameter prmPriceChange = new SqliteParameter("@PriceChange", DbType.Single) { Value = dayQuotation.PriceChange };
            SqliteParameter prmChange = new SqliteParameter("@Change", DbType.Single) { Value = dayQuotation.Change };
            SqliteParameter prmTurnOver = new SqliteParameter("@TurnOver", DbType.Single) { Value = dayQuotation.TurnOver };
            SqliteParameter prmVolumeTurnOver = new SqliteParameter("@VolumeTurnOver", DbType.Single) { Value = dayQuotation.VolumeTurnOver };
            SqliteParameter prmPriceTurnOver = new SqliteParameter("@PriceTurnOver", DbType.Single) { Value = dayQuotation.PriceTurnOver };
            SqliteParameter prmMarketValue = new SqliteParameter("@MarketValue", DbType.Single) { Value = dayQuotation.MarketValue };
            SqliteParameter prmNegoValue = new SqliteParameter("@NegoValue", DbType.Single) { Value = dayQuotation.NegoValue };
            sqliteDb.ExecuteNoQuery(sb.ToString(), prmSecurityCode, prmTxDate, prmClosePrice, prmHighPrice, prmLowPrice, prmOpenPrice, prmLastClosePrice,
                prmPriceChange, prmChange, prmTurnOver, prmVolumeTurnOver, prmPriceTurnOver, prmMarketValue, prmNegoValue);
        }
    }
}