using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using StockWorm.Domain.Interfaces;

namespace StockWorm.Domain
{
    public class SecurityDomain
    {
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
        [Description("公司名称")]
        [Required(ErrorMessage="公司名称不能为空")]
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
    
        private IGetDataBehavior getDataBehavior;
        private ICreateDataBehavior createDataBehavior;

        public void RegisterGetDataBehavior(IGetDataBehavior getDataBehavior)
        {
            this.getDataBehavior = getDataBehavior;
        }

        public void RegisterCreateBehavior(ICreateDataBehavior createDataBehavior)
        {
            this.createDataBehavior = createDataBehavior;
        }

        public void GetData()
        {
            getDataBehavior.GetData();
        }
    }
}
