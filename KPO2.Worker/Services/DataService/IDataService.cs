namespace KPO2.Worker.Services.DataService
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Data.Entities;

    public interface IDataService
    {
        Task ClearData();
        Task SaveRssItems(IEnumerable<RssItem> rssItems);
    }
}
