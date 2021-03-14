using System;

namespace StockWorm.Domain
{
    //实时股票交易
    public class RealTimeStockTransaction
    {
        public string StockName { get;set; }
        public string StockCode { get;set;}
        public double OpenPrice { get;set; }
        public double LastClosePrice { get;set; }
        public double CurrentPrice { get;set;}
        public double HightPrice{ get;set; }
        public double LowPrice { get;set; }
        public double Turnover { get;set;}
        public double Volume {get;set;}
        public double Ask1Volume {get;set;}
        public double Ask1 { get;set; }
        public double Ask2Volume {get;set;}
        public double Ask2 {get;set;}
        public double Ask3Volume {get;set;}
        public double Ask3 {get;set;}
        public double Ask4Volume {get;set;}
        public double Ask4 {get;set;}
        public double Ask5Volume {get;set;}
        public double Ask5 {get;set;}
        public double Bid1Volume {get;set;}
        public double Bid1 {get;set;}
        public double Bid2Volume {get;set;}
        public double Bid2 {get;set;}
        public double Bid3Volume {get;set;}
        public double Bid3 {get;set;}
        public double Bid4Volume {get;set;}
        public double Bid4 {get;set;}
        public double Bid5Volume {get;set;}
        public double Bid5 {get;set;}
        public DateTime Date { get;set;}
        public DateTime Time {get;set;}
    }
}