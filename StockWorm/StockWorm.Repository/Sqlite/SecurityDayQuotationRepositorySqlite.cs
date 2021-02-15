using StockWorm.Repository.Context;
using System.Collections.Generic;
using StockWorm.Domain;
using StockWorm.Repository.Net;
using System;
using System.Text;
using Microsoft.Data.Sqlite;
using System.Data;

namespace StockWorm.Repository.Sqlite
{
    public class SecurityDayQuotationRepositorySqlite:SecurityDayQuotationRepository
    {
        private DatabaseContext dbContext;
        public SecurityDayQuotationRepositorySqlite()
        {
            this.dbContext = DatabaseContextFactory.GetInstance().CreateDatabaseContext("sqlite");
        }

        public SecurityDayQuotationRepositorySqlite(DatabaseContext dbContext) : this()
        {
            this.dbContext = dbContext;
        }
        public override void InsertIntoDB(List<SecurityDayQuotationDomain> securityDayQuotations)
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
                dbContext.ExecuteNoQuery(sb.ToString(), prmSecurityCode, prmTxDate, prmClosePrice, prmHighPrice, prmLowPrice, prmOpenPrice, prmLastClosePrice,
                    prmPriceChange, prmChange, prmTurnOver, prmVolumeTurnOver, prmPriceTurnOver, prmMarketValue, prmNegoValue);
            }
        }

        public override void InsertIntoDB(SecurityDayQuotationDomain dayQuotation)
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
            dbContext.ExecuteNoQuery(sb.ToString(), prmSecurityCode, prmTxDate, prmClosePrice, prmHighPrice, prmLowPrice, prmOpenPrice, prmLastClosePrice,
                prmPriceChange, prmChange, prmTurnOver, prmVolumeTurnOver, prmPriceTurnOver, prmMarketValue, prmNegoValue);
        }
    }
}