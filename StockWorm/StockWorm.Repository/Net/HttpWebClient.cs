using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Text;

namespace StockWorm.Repository.Net
{
    public class HttpWebClient
    {
        private string url;
        [Description("请求路径")]
        public string URL
        {
            get { return url; }
            set
            {
                if (url == value) return;
                url = value;
            }
        }

        private Dictionary<string, string> headers;
        [Description("请求头")]
        public Dictionary<string, string> Headers
        {
            get { return headers; }
        }

        private string method = "";
        [Description("请求方式(GET,POST)")]
        private string Method
        {
            get { return method; }
            set
            {
                if (method == value) return;
                method = value;
            }
        }

        private string data = "";
        [Description("请求数据")]
        public string Data
        {
            get { return data; }
            set
            {
                if (data == value) return;
                data = value;
            }
        }

        private Encoding encoding = Encoding.UTF8;
        [Description("编码方式")]
        public Encoding Encoding
        {
            get { return encoding; }
            set
            {
                if (encoding == value) return;
                encoding = value;
            }
        }
        public HttpWebClient()
        {
            headers = new Dictionary<string, string>();
        }

        public string SendGetRequest()
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format("{0}?{1}", url, data));
                request.Method = "GET";
                foreach (KeyValuePair<string, string> item in headers)
                {
                    request.Headers.Add(item.Key, item.Value);
                }

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (Stream sm = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(sm, encoding))
                        {
                            return reader.ReadToEnd();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("ex.Message={0},requestData={1}",ex.Message,data));
            }
        }

    }
}
