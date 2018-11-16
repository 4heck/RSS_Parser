namespace KPO2.Worker.Services.RssService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Xml.Linq;
    using Data.Entities;
    using Types;
    using Utils;

    public class RssServiceRtOnline : IRssService
    {
        private const string RssUrlRtonline = "http://rt-online.ru/feed/";
        
        public RssItem[] GetRssItems()
        {
            var result = new List<RssItem>();
            // Получение данных с помощью запроса
            var rssResult = WebRequestUtils.Get(RssUrlRtonline, Encoding.UTF8);
            // Парсинг данных
            var document = XDocument.Parse(rssResult);
            var xmlItems = document.Descendants("item");
            foreach (var xmlItem in xmlItems)
            {
                var rssItem = new RssItem
                {
                    Source = RssSource.RtOnline,
                    Title = xmlItem.Elements().First(x => x.Name == "title").Value,
                    Link = xmlItem.Elements().First(x => x.Name == "link").Value,
                    Date = Convert.ToDateTime(xmlItem.Elements().First(x => x.Name == "pubDate").Value)
                };
                result.Add(rssItem);
            }
            return result.ToArray();
        }
    }
}