using System.ComponentModel;
using StockWorm.Domain.Interfaces;
using System;
using Newtonsoft.Json;

namespace StockWorm.Domain
{
    public class SecurityDayQuotationDomain
    {
        private string securityCode = "";
        [Description("股票代码")]
        public string SecurityCode 
        {
            get { return securityCode; }
            set 
            {
                if(securityCode == value) return;
                securityCode = value;
            }
        } 

        private DateTime txDate = DateTime.Now;
        [Description("交易日期")]
        public DateTime TxDate
        {
            get { return txDate; }
            set
            {
                if(txDate == value) return;
                txDate = value;
            }
        }
        private string securityAbbr = "";
        [Description("股票简称")]
        public string SecurityAbbr
        {
            get { return securityAbbr; }
            set
            {
                if(securityAbbr == value) return;
                securityAbbr = value;
            }
        }
        
        private double closePrice = 0;
        [Description("收盘价（元）")]
        public double ClosePrice
        {
            get { return closePrice; }
            set
            {
                if (closePrice == value) return;
                closePrice = value;
            }
        }

        private double highPrice = 0;
        [Description("最高价（元）")]
        public double HighPrice
        {
            get { return highPrice; }
            set
            {
                if(highPrice == value) return;
                highPrice = value;
            }
        }

        private double lowPrice=0;
        [Description("最低价（元）")]
        public double LowPrice
        {
            get { return lowPrice; }
            set
            {
                if(lowPrice == value) return;
                lowPrice = value;
            }
        }
        
        private double openPrice = 0;
        [Description("开盘价（元）")]
        public double OpenPrice
        {
            get{ return openPrice; }
            set
            {
                if(openPrice == value) return;
                openPrice = value;
            }
        }
        
        private double lastClosePrice = 0;
        [Description("昨日收盘价")]
        public double LastClosePrice
        {
            get { return lastClosePrice; }
            set
            {
                if (lastClosePrice == value) return;
                lastClosePrice = value;
            }
        }

        private double priceChange = 0;
        [Description("涨跌额")]
        public double PriceChange
        {
            get{ return priceChange; }
            set
            {
                if(priceChange == value) return;
                priceChange = value;
            }
        }

        private double change = 0;
        [Description("涨跌幅（%）")]
        public double Change
        {
            get { return change; }
            set
            {
                if (change == value) return;
                change = value;
            }
        }

        private double turnOver = 0;
        [Description("换手率")]
        public double TurnOver
        {
            get { return turnOver; }
            set
            {
                if(turnOver == value) return;
                turnOver = value;
            }
        }
    
        private double volumeTurnOver = 0;
        [Description("成交量（手）")]
        public double VolumeTurnOver
        {
            get { return volumeTurnOver; }
            set
            {
                if(volumeTurnOver == value) return;
                volumeTurnOver = value;
            }
        }

        private double priceTurnOver = 0;
        [Description("成交金额（万元）")]
        public double PriceTurnOver
        {
            get { return priceTurnOver; }
            set
            {
                if (priceTurnOver == value) return;
                priceTurnOver = value;
            }
        }

        private double marketValue = 0;
        [Description("市价总值（万元）")]
        public double MarketValue
        {
            get { return marketValue; }
            set
            {
                if (marketValue == value) return;
                marketValue = value;
            }
        }

        private double negoValue = 0;
        [Description("流通市值（万元）")]
        public double NegoValue
        {
            get { return negoValue; }
            set
            {
                if (negoValue == value) return;
                negoValue = value;
            }
        }

        
    }
}