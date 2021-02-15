using System;
using System.Collections.Generic;
using StockWorm.Repository.Net;
using StockWorm.Domain;

namespace StockWorm.Repository
{
    public class SecurityDayQuotationFromWangYIRepository
    {
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
                value.MarketValue = Math.Round(Convert.ToDouble(arrTemp[13]) / 100000000,4);
                value.NegoValue = Math.Round(Convert.ToDouble(arrTemp[14]) / 100000000,4);
                values.Add(value);
            }
            return values;
        }
    }
}