using System;
using System.Collections.Generic;
using StockWorm.Domain;
using StockWorm.Repository.Net;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace StockWorm.Repository
{
    public class SecurityFromSSERepository
    {
        public List<SecurityDomain> GetSecuritiesFromSSE()
        {
            List<SecurityDomain> securities = new List<SecurityDomain>();
            int pageIndex = 1;
            int pageSize = 1000;
            List<SecurityDomain> securitiesInPage = GetSecuritiesInPageFromSSE(pageIndex, pageSize);
            while (securitiesInPage.Count > 0)
            {
                securities.AddRange(securitiesInPage);
                pageIndex++;
                securitiesInPage = GetSecuritiesInPageFromSSE(pageIndex, pageSize);
            }
            return securities;
        }

        private List<SecurityDomain> GetSecuritiesInPageFromSSE(int pageIndex, int pageSize)
        {
            HttpWebClient client = new HttpWebClient();
            client.URL = "http://query.sse.com.cn/security/stock/getStockListData2.do";
            client.Headers.Add("Accept", "*/*");
            client.Headers.Add("Accept-Encoding", "gzip, deflate");
            client.Headers.Add("Accept-Language", "zh-CN,q=0.9;");
            client.Headers.Add("Connection", "keep-alive");
            client.Headers.Add("Host", "query.sse.com.cn");
            client.Headers.Add("Referer", "http://www.sse.com.cn/");
            client.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.3; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36 Edg/87.0.664.66");
            Random random = new Random();
            client.Data = string.Format("&jsonCallBack=jsonpCallback{0}&isPagination=true&stockCode=&csrcCode=&areaName=&stockType=1&pageHelp.cacheSize=1&pageHelp.beginPage={1}&pageHelp.pageSize={2}&pageHelp.pageNo={1}&_={3}",
                random.Next(100000), pageIndex, pageSize, DateTime.Now.Subtract(new DateTime(1970, 1, 1, 8, 0, 0)).Ticks);
            string result = client.SendGetRequest();
            Match match = Regex.Match(result, "\"result\":\\[([\\S\\s]*)\\]");
            List<SecurityDomain> values = new List<SecurityDomain>();
            if (match.Success)
            {
                List<SecurityDomain> lst = JsonConvert.DeserializeObject<List<SecurityDomain>>(string.Format("[{0}]", match.Groups[1].Value));
                foreach (SecurityDomain security in lst)
                {
                    security.ExchangeMarket = "SSE";
                }
                values.AddRange(lst);
            }
            return values;
        }
    }
}