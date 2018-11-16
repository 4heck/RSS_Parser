namespace KPO2.Worker.Services.DataService
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Data.Entities;
    using Npgsql;
    using NpgsqlTypes;

    public class DbDataService : IDataService
    {
        private const string ConnectionString = "Server=localhost;Port=5432;Database=aggregator;User Id=postgres;Password=1;";
        private const string DeleteQuery = "DELETE FROM \"rss_item\"";
        private const string UpdateQuery = "INSERT INTO \"rss_item\"(\"source\", " +
                                      "\"title\", \"link\", \"date\") " +
                                      "VALUES (@0, @1, @2, @3)";
        
        public async Task ClearData()
        {
            using (var conn = new NpgsqlConnection(ConnectionString)) 
            {
                await conn.OpenAsync();
                using (var comm = conn.CreateCommand()) 
                {
                    comm.CommandText = DeleteQuery;
                    await comm.ExecuteNonQueryAsync();
                }
            }
        }

        
        public async Task SaveRssItems(IEnumerable<RssItem> items)
        {
            using (var conn = new NpgsqlConnection(ConnectionString))
            {
                await conn.OpenAsync();
                foreach (var item in items)
                {
                    using (var comm = conn.CreateCommand())
                    {
                        comm.CommandText = UpdateQuery;
                        comm.Parameters.AddWithValue("0", NpgsqlDbType.Integer, (int)item.Source);
                        comm.Parameters.AddWithValue("1", NpgsqlDbType.Text, item.Title);
                        comm.Parameters.AddWithValue("2", NpgsqlDbType.Text, item.Link);
                        comm.Parameters.AddWithValue("3", NpgsqlDbType.TimestampTz, item.Date);
                        await comm.ExecuteNonQueryAsync();
                    }
                }
            }
        }
    }
}
