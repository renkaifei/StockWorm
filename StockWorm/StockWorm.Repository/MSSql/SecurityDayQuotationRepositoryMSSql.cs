using StockWorm.Repository.Context;
using System.Collections.Generic;
using StockWorm.Domain;
using StockWorm.Repository.Net;
using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace StockWorm.Repository.MSSql
{
    public class SecurityDayQuotationRepositoryMSSql:SecurityDayQuotationRepository
    {
        private DatabaseContext dbContext;
        public SecurityDayQuotationRepositoryMSSql()
        {
            DatabaseContextFactory factory = new DatabaseContextFactory();
            this.dbContext = factory.CreateDatabaseContext("mssql");
        }

        public SecurityDayQuotationRepositoryMSSql(DatabaseContext dbContext) : this()
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
                SqlParameter prmSecurityCode = new SqlParameter("@SecurityCode", SqlDbType.VarChar,10) { Value = dayQuotation.SecurityCode };
                SqlParameter prmTxDate = new SqlParameter("@TxDate", SqlDbType.DateTime) { Value = dayQuotation.TxDate };
                SqlParameter prmClosePrice = new SqlParameter("@ClosePrice",SqlDbType.Float){ Value = dayQuotation.ClosePrice};
                SqlParameter prmHighPrice = new SqlParameter("@HighPrice", SqlDbType.Float) { Value = dayQuotation.HighPrice };
                SqlParameter prmLowPrice = new SqlParameter("@LowPrice", SqlDbType.Float) { Value = dayQuotation.LowPrice };
                SqlParameter prmOpenPrice = new SqlParameter("@OpenPrice", SqlDbType.Float) { Value = dayQuotation.OpenPrice };
                SqlParameter prmLastClosePrice = new SqlParameter("@LastClosePrice", SqlDbType.Float) { Value = dayQuotation.LastClosePrice };
                SqlParameter prmPriceChange = new SqlParameter("@PriceChange", SqlDbType.Float) { Value = dayQuotation.PriceChange };
                SqlParameter prmChange = new SqlParameter("@Change", SqlDbType.Float) { Value = dayQuotation.Change };
                SqlParameter prmTurnOver = new SqlParameter("@TurnOver", SqlDbType.Float) { Value = dayQuotation.TurnOver };
                SqlParameter prmVolumeTurnOver = new SqlParameter("@VolumeTurnOver", SqlDbType.Float) { Value = dayQuotation.VolumeTurnOver };
                SqlParameter prmPriceTurnOver = new SqlParameter("@PriceTurnOver", SqlDbType.Float) { Value = dayQuotation.PriceTurnOver };
                SqlParameter prmMarketValue = new SqlParameter("@MarketValue", SqlDbType.Float) { Value = dayQuotation.MarketValue };
                SqlParameter prmNegoValue = new SqlParameter("@NegoValue", SqlDbType.Float) { Value = dayQuotation.NegoValue };
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
            SqlParameter prmSecurityCode = new SqlParameter("@SecurityCode", SqlDbType.VarChar,10) { Value = dayQuotation.SecurityCode };
            SqlParameter prmTxDate = new SqlParameter("@TxDate", SqlDbType.DateTime) { Value = dayQuotation.TxDate };
            SqlParameter prmClosePrice = new SqlParameter("@ClosePrice",SqlDbType.Float){ Value = dayQuotation.ClosePrice };
            SqlParameter prmHighPrice = new SqlParameter("@HighPrice", SqlDbType.Float) { Value = dayQuotation.HighPrice };
            SqlParameter prmLowPrice = new SqlParameter("@LowPrice", SqlDbType.Float) { Value = dayQuotation.LowPrice };
            SqlParameter prmOpenPrice = new SqlParameter("@OpenPrice", SqlDbType.Float) { Value = dayQuotation.OpenPrice };
            SqlParameter prmLastClosePrice = new SqlParameter("@LastClosePrice", SqlDbType.Float) { Value = dayQuotation.LastClosePrice };
            SqlParameter prmPriceChange = new SqlParameter("@PriceChange", SqlDbType.Float) { Value = dayQuotation.PriceChange };
            SqlParameter prmChange = new SqlParameter("@Change", SqlDbType.Float) { Value = dayQuotation.Change };
            SqlParameter prmTurnOver = new SqlParameter("@TurnOver", SqlDbType.Float) { Value = dayQuotation.TurnOver };
            SqlParameter prmVolumeTurnOver = new SqlParameter("@VolumeTurnOver", SqlDbType.Float) { Value = dayQuotation.VolumeTurnOver };
            SqlParameter prmPriceTurnOver = new SqlParameter("@PriceTurnOver", SqlDbType.Float) { Value = dayQuotation.PriceTurnOver };
            SqlParameter prmMarketValue = new SqlParameter("@MarketValue", SqlDbType.Float) { Value = dayQuotation.MarketValue };
            SqlParameter prmNegoValue = new SqlParameter("@NegoValue", SqlDbType.Float) { Value = dayQuotation.NegoValue };
            dbContext.ExecuteNoQuery(sb.ToString(), prmSecurityCode, prmTxDate, prmClosePrice, prmHighPrice, prmLowPrice, prmOpenPrice, prmLastClosePrice,
                prmPriceChange, prmChange, prmTurnOver, prmVolumeTurnOver, prmPriceTurnOver, prmMarketValue, prmNegoValue);
        }
    }
}