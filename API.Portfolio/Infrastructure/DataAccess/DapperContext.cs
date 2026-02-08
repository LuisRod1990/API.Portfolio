using Npgsql;
using System.Data;

namespace PortfolioApi.Infrastructure.DataAccess
{
    public class DapperContext
    {
        private readonly IConfiguration _config;
        private readonly NpgsqlDataSource _dataSource;

        public DapperContext(IConfiguration config)
        {
            _config = config;
            var connString = Environment.GetEnvironmentVariable("CONN");

            // Usa el builder para que maneje SSL y defaults
            var dataSourceBuilder = new NpgsqlDataSourceBuilder(connString);
            _dataSource = dataSourceBuilder.Build();
        }

        public IDbConnection CreateConnection()
            => _dataSource.OpenConnection();
    }
}