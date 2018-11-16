namespace KPO2.Data.Repositories.RssItemRepository
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Entities;
    using Npgsql;
    using NpgsqlTypes;
    using Types;

    public class RssItemRepository : IRssItemRepository
    {
        private readonly string _connectionString;
        
        public RssItemRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        private const string Select = "SELECT \"id\", \"source\", \"title\", \"link\", \"date\" " +
                                      "FROM \"rss_item\" " +
                                      "ORDER BY \"id\"";
        
        private const string SelectById = "SELECT \"id\", \"source\", \"title\", \"link\", \"date\" " +
                                          "FROM \"rss_item\" " +
                                          "WHERE \"id\" = @0";
        
        private const string Insert = "INSERT INTO \"rss_item\"(\"source\", \"title\", \"link\", \"date\") " +
                                      "VALUES (@0, @1, @2, @3) " +
                                      "RETURNING \"id\", \"source\", \"title\", \"link\", \"date\"";
        
        private const string Update = "UPDATE \"rss_item\" " +
                                      "SET \"source\" = @1, \"title\" = @2, \"link\" = @3, \"date\" = @4 " +
                                      "WHERE \"id\" = @0 " +
                                      "RETURNING \"id\", \"source\", \"title\", \"link\", \"date\"";
        
        private const string Delete = "DELETE FROM \"rss_item\" " +
                                      "WHERE \"id\" = @0 " +
                                      "RETURNING \"id\", \"source\", \"title\", \"link\", \"date\"";
        
        public async Task<RssItem> Get(long id)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                using (var comm = conn.CreateCommand())
                {
                    comm.CommandText = SelectById;
                    comm.Parameters.AddWithValue("0", NpgsqlDbType.Bigint, id);
                    using (var reader = await comm.ExecuteReaderAsync())
                    {
                        if (! await reader.ReadAsync()) return null;
                        return new RssItem
                        {
                            Id = reader.GetInt64(0),
                            Source = (RssSource)reader.GetInt32(1),
                            Title = reader.GetString(2),
                            Link = reader.GetString(3),
                            Date = reader.GetDateTime(4)
                        };
                    }
                }
            }
        }

        public async Task<RssItem[]> GetAll()
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                using (var comm = conn.CreateCommand())
                {
                    var result = new List<RssItem>();
                    comm.CommandText = Select;
                    using (var reader = await comm.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            result.Add(new RssItem
                            {
                                Id = reader.GetInt64(0),
                                Source = (RssSource)reader.GetInt32(1),
                                Title = reader.GetString(2),
                                Link = reader.GetString(3),
                                Date = reader.GetDateTime(4)
                            });
                        }
                        return result.ToArray();
                    }
                }
            }
        }

        public async Task<RssItem> Add(RssItem entity)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                using (var comm = conn.CreateCommand())
                {
                    comm.CommandText = Insert;
                    comm.Parameters.AddWithValue("0", NpgsqlDbType.Integer, (int)entity.Source);
                    comm.Parameters.AddWithValue("1", NpgsqlDbType.Text, entity.Title);
                    comm.Parameters.AddWithValue("2", NpgsqlDbType.Text, entity.Link);
                    comm.Parameters.AddWithValue("3", NpgsqlDbType.TimestampTz, entity.Date);
                    using (var reader = await comm.ExecuteReaderAsync())
                    {
                        if (! await reader.ReadAsync()) return null;
                        return new RssItem
                        {
                            Id = reader.GetInt64(0),
                            Source = (RssSource)reader.GetInt32(1),
                            Title = reader.GetString(2),
                            Link = reader.GetString(3),
                            Date = reader.GetDateTime(4)
                        };
                    }
                }
            }
        }

        public async Task<RssItem> Edit(RssItem entity)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                using (var comm = conn.CreateCommand())
                {
                    comm.CommandText = Update;
                    comm.Parameters.AddWithValue("0", NpgsqlDbType.Bigint, entity.Id);
                    comm.Parameters.AddWithValue("1", NpgsqlDbType.Integer, (int)entity.Source);
                    comm.Parameters.AddWithValue("2", NpgsqlDbType.Text, entity.Title);
                    comm.Parameters.AddWithValue("3", NpgsqlDbType.Text, entity.Link);
                    comm.Parameters.AddWithValue("4", NpgsqlDbType.TimestampTz, entity.Date);
                    using (var reader = await comm.ExecuteReaderAsync())
                    {
                        if (! await reader.ReadAsync()) return null;
                        return new RssItem
                        {
                            Id = reader.GetInt64(0),
                            Source = (RssSource)reader.GetInt32(1),
                            Title = reader.GetString(2),
                            Link = reader.GetString(3),
                            Date = reader.GetDateTime(4)
                        };
                    }
                }
            }
        }

        public async Task<RssItem> Remove(long id)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                using (var comm = conn.CreateCommand())
                {
                    comm.CommandText = Delete;
                    comm.Parameters.AddWithValue("0", NpgsqlDbType.Bigint, id);
                    using (var reader = await comm.ExecuteReaderAsync())
                    {
                        if (! await reader.ReadAsync()) return null;
                        return new RssItem
                        {
                            Id = reader.GetInt64(0),
                            Source = (RssSource)reader.GetInt32(1),
                            Title = reader.GetString(2),
                            Link = reader.GetString(3),
                            Date = reader.GetDateTime(4)
                        };
                    }
                }
            }
        }
    }
}