namespace KPO2.Utils
{
    using System;
    using System.IO;
    using System.Net;
    using System.Text;

    public static class WebRequestUtils
    {
        public static string Get(string url, Encoding encoding)
        {
            string result;
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.Timeout = 10000;
            using (var response = (HttpWebResponse)request.GetResponse())
            {
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception();
                }

                var responseStream = response.GetResponseStream();
                if (responseStream == null)
                {
                    throw new NullReferenceException();
                }
                
                using (var sr = new StreamReader(responseStream, encoding))
                {
                    result = sr.ReadToEnd();
                }
            }
            return result;
        }
    }
}
