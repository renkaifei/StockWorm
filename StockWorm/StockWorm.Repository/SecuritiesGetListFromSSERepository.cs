using System;
using StockWorm.Repository.Net;
using System.Collections.Generic;
using StockWorm.Domain;
using StockWorm.Domain.Interfaces;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace StockWorm.Repository
{
    public class SecuritiesGetListFromSSERepository:IGetDataBehavior
    {
        private HttpWebClient client;

        private List<SecurityDomain> values;

        public SecuritiesGetListFromSSERepository(List<SecurityDomain> values)
        {
            InitWebClient();
            this.values = values;
        }

        private void InitWebClient()
        {
            client = new HttpWebClient();
            client.URL = "http://query.sse.com.cn/security/stock/getStockListData2.do";
            client.Headers.Add("Accept","*/*");
            client.Headers.Add("Accept-Encoding","gzip, deflate");
            client.Headers.Add("Accept-Language","zh-CN,q=0.9;");
            client.Headers.Add("Connection","keep-alive");
            client.Headers.Add("Host","query.sse.com.cn");
            client.Headers.Add("Referer","http://www.sse.com.cn/");
            client.Headers.Add("User-Agent","Mozilla/5.0 (Windows NT 6.3; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36 Edg/87.0.664.66");
            client.Data = "&jsonCallBack=jsonpCallback66846&isPagination=true&stockCode=&csrcCode=&areaName=&stockType=1&pageHelp.cacheSize=1&pageHelp.beginPage=1&pageHelp.pageSize=10&pageHelp.pageNo=1&_=1608560370126";
        }

        public void GetData()
        {
            string result = client.SendGetRequest();
            Match match = Regex.Match(result,"\"result\":\\[([\\S\\s]*)\\]");
            if(match.Success)
            {
                List<SecurityDomain> lst = JsonConvert.DeserializeObject<List<SecurityDomain>>(string.Format("[{0}]",match.Groups[1].Value));
                values.AddRange(lst);
            }
        }
    }
}