namespace KPO2.Worker
{
    using System;
    using System.Threading.Tasks;
    using Services.DataService;
    using Services.RssService;
    using Types;

    internal static class Program
    {
        private static readonly IDataService _dataService = new DbDataService();

        private static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();
        }

        private static async Task MainAsync(string[] args)
        {
            await _dataService.ClearData();
            
            try
            {
                await GetNews(RssSource.Prokazan);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            
            try
            {
                await GetNews(RssSource.BusinessGazeta);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            
            try
            {
                await GetNews(RssSource.RtOnline);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private static async Task GetNews(RssSource source)
        {
            IRssService rssService;
            string sourceName;

            switch (source)
            {
                case RssSource.Undefined:
                    throw new ArgumentOutOfRangeException();
                case RssSource.Prokazan:
                    rssService = new RssServiceProkazan();
                    sourceName = "Prokazan";
                    break;
                case RssSource.RtOnline:
                    rssService = new RssServiceRtOnline();
                    sourceName = "Rtonline";
                    break;
                case RssSource.BusinessGazeta:
                    rssService = new RssServiceBusinessGazeta();
                    sourceName = "BusinessGazeta";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            Console.WriteLine($"Получение данных с {sourceName}");
            var rssItems = rssService.GetRssItems();
            Console.WriteLine($"Получено {rssItems.Length} статей");
            await _dataService.SaveRssItems(rssItems);
        }
    }
}
