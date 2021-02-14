using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using StockWorm.Domain.Interfaces;
using System.Collections.Generic;

namespace StockWorm.Domain
{
    public class SecurityDomain
    {
        private long securityID = 0;
        [Description("证券ID")]
        public long SecurityID
        {
            get { return securityID; }
            set 
            {
                if(securityID  == value) return ;
                securityID = value;
            }
        }

        private string companyAbbr = "";
        [Description("公司简称")]
        [Required(ErrorMessage = "公司简称不能为空")]
        [JsonProperty("COMPANY_ABBR")]
        public string CompanyAbbr
        {
            get  { return companyAbbr; }
            set
            {
                if(companyAbbr == value) return;
                companyAbbr = value;
            }
        }

        private string companyCode;
        [Description("公司代码")]
        [Required(ErrorMessage="公司代码不能为空")]
        [JsonProperty("COMPANY_CODE")]
        public string CompanyCode
        {
            get{ return companyCode; }
            set
            {
                if(companyCode == value) return;
                companyCode = value;
            }
        }

        private string securityAbbr = "";
        [Description("股票简称")]
        [Required(ErrorMessage = "股票简称不能为空")]
        [JsonProperty("SECURITY_ABBR_A")]
        public string SecurityAbbr
        {
            get{ return securityAbbr; }
            set
            {
                if(securityAbbr == value) return;
                securityAbbr = value;
            }
        }

        private string securityCode = "";
        [Description("股票代码")]
        [Required(ErrorMessage="股票代码不能为空")]
        [JsonProperty("SECURITY_CODE_A")]
        public string SecurityCode
        {
            get{ return securityCode; }
            set
            {
                if(securityCode == value) return;
                securityCode = value;
            }
        }

        private DateTime listingDate = DateTime.MinValue ;
        [Description("上市时间")]
        [JsonProperty("LISTING_DATE")]
        public DateTime ListingDate
        {
            get{ return listingDate; }
            set
            {
                if(listingDate == value) return;
                listingDate = value;
            }
        }
    
        private string exchangeMarket = "";
        [Description("交易市场")]
        public string ExchangeMarket
        {
            get { return exchangeMarket; }
            set
            {
                if(exchangeMarket == value) return;
                exchangeMarket = value;
            }
        }

        public SecurityTaskDomain BuildStartTask()
        {
            SecurityTaskDomain tempTask = new SecurityTaskDomain();
            tempTask.SecurityCode = securityCode;
            tempTask.ExchangeMarket = exchangeMarket;
            tempTask.BeginDate = listingDate;
            tempTask.EndDate = listingDate.AddMonths(1);
            if(tempTask.EndDate.Date >= DateTime.Now.Date)
            {
                tempTask.EndDate = DateTime.Now.AddDays(-1).Date;
            }
            if(tempTask.BeginDate > tempTask.EndDate)
            {
                tempTask.EndDate = tempTask.BeginDate;
            }
            return tempTask;
        }

    }
}
