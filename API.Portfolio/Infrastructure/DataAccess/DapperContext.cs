using Npgsql;
using System.Data;

namespace PortfolioApi.Infrastructure.DataAccess
{
    public class DapperContext
    {
        private readonly IConfiguration _config;

        public DapperContext(IConfiguration config)
        {
            _config = config;
        }

        // sql
        //public IDbConnection CreateConnection() => new SqlConnection(Environment.GetEnvironmentVariable("CONN"));
        public IDbConnection CreateConnection()
            => new NpgsqlConnection(Environment.GetEnvironmentVariable("CONN"));
    }
    
}