using StockWorm.Repository.Net;
using System.Collections.Generic;
using StockWorm.Domain;
using System;

namespace StockWorm.Repository
{
    public class SecurityDayQuotationFromSinaRepository
    {
        public List<RealTimeStockTransaction> GetList(string stockCodes)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            HttpWebClient client = new HttpWebClient();
            client.URL = string.Format("http://hq.sinajs.cn/list={0}",stockCodes);
            string result = client.SendGetRequest();
            string[] arrResult = result.Split(';');
            int count = arrResult.Length - 1;
            string[] arrStockCode = stockCodes.Split(',');
            List<RealTimeStockTransaction> lst = new List<RealTimeStockTransaction>();
            RealTimeStockTransaction realTimeStockTransaction;
            for(int i =0;i < count;i++)
            {
                int startIndex = arrResult[i].IndexOf("\"");
                int endIndex = arrResult[i].LastIndexOf("\"");
                string temp = arrResult[i].Substring(startIndex,endIndex - startIndex);
                string[] arr = temp.Split(',');
                realTimeStockTransaction = new RealTimeStockTransaction();
                realTimeStockTransaction.StockName = arr[0];
                realTimeStockTransaction.StockCode = arrStockCode[i];
                realTimeStockTransaction.OpenPrice = Convert.ToDouble(arr[1]);
                realTimeStockTransaction.LastClosePrice = Convert.ToDouble(arr[2]);
                realTimeStockTransaction.CurrentPrice = Convert.ToDouble(arr[3]);
                realTimeStockTransaction.HightPrice = Convert.ToDouble(arr[4]);
                realTimeStockTransaction.LowPrice = Convert.ToDouble(arr[5]);
                realTimeStockTransaction.Volume = Convert.ToDouble(arr[8]);
                realTimeStockTransaction.Turnover = Convert.ToDouble(arr[9]);
                realTimeStockTransaction.Ask1Volume = Convert.ToDouble(arr[10]);
                realTimeStockTransaction.Ask1 = Convert.ToDouble(arr[11]);
                realTimeStockTransaction.Ask2Volume = Convert.ToDouble(arr[12]);
                realTimeStockTransaction.Ask2 = Convert.ToDouble(arr[13]);
                realTimeStockTransaction.Ask3Volume = Convert.ToDouble(arr[14]);
                realTimeStockTransaction.Ask3 = Convert.ToDouble(arr[15]);
                realTimeStockTransaction.Ask4Volume = Convert.ToDouble(arr[16]);
                realTimeStockTransaction.Ask4 = Convert.ToDouble(arr[17]);
                realTimeStockTransaction.Ask5Volume = Convert.ToDouble(arr[18]);
                realTimeStockTransaction.Ask5 = Convert.ToDouble(arr[19]);
                realTimeStockTransaction.Bid1Volume = Convert.ToDouble(arr[20]);
                realTimeStockTransaction.Bid1 = Convert.ToDouble(arr[21]);
                realTimeStockTransaction.Bid2Volume = Convert.ToDouble(arr[22]);
                realTimeStockTransaction.Bid2 = Convert.ToDouble(arr[23]);
                realTimeStockTransaction.Bid3Volume = Convert.ToDouble(arr[24]);
                realTimeStockTransaction.Bid3 = Convert.ToDouble(arr[25]);
                realTimeStockTransaction.Bid4Volume = Convert.ToDouble(arr[26]);
                realTimeStockTransaction.Bid4 = Convert.ToDouble(arr[27]);
                realTimeStockTransaction.Bid5Volume = Convert.ToDouble(arr[28]);
                realTimeStockTransaction.Bid5 = Convert.ToDouble(arr[29]);
                realTimeStockTransaction.Date = Convert.ToDateTime(arr[30]);
                realTimeStockTransaction.Time = Convert.ToDateTime(arr[30] + " "+ arr[31]);
                lst.Add(realTimeStockTransaction);
            }
            return lst;
        }
    }
}