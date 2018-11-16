namespace KPO2.Worker.Services.DataService
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;
    using Data.Entities;
    using Types;

    public class CsvDataService : IDataService
	{
		private static readonly string FilePath = Path.Combine(AppContext.BaseDirectory, "data", "output.csv");

	    public async Task ClearData()
	    {
	        Directory.CreateDirectory(Path.GetDirectoryName(FilePath));
	        File.Delete(FilePath);
	    }
	    
        public async Task SaveRssItems(IEnumerable<RssItem> rssItems)
        {
            var csvFile = new StreamWriter(FilePath, true, Encoding.UTF8);
            var sb = new StringBuilder();
            foreach (var rssItem in rssItems)
            {
                switch (rssItem.Source)
                {
                    case RssSource.Undefined:
                        throw new ArgumentOutOfRangeException();
                    case RssSource.Prokazan:
                        sb.Append("prokazan;");
                        break;
                    case RssSource.BusinessGazeta:
                        sb.Append("business-gazeta;");
                        break;
                    case RssSource.RtOnline:
                        sb.Append("rt-online;");
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                sb.Append($"{rssItem.Title};{rssItem.Date.ToString(CultureInfo.InvariantCulture)};{rssItem.Link}");
                sb.Append($"{Environment.NewLine}");
            }
            csvFile.Write(sb.ToString());
            csvFile.Close();
        }

	}
}
