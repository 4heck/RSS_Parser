namespace KPO2.Worker.Services.RssService
{
    using Data.Entities;

    public interface IRssService
    {
        RssItem[] GetRssItems();
    }
}