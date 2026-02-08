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

        // Conexión con SQL Server - descomentar si se usa SQL Server
        //public IDbConnection CreateConnection() => new SqlConnection(Environment.GetEnvironmentVariable("CONN"));

        // Conexión con PostgreSQL
        public IDbConnection CreateConnection()
            => new NpgsqlConnection(Environment.GetEnvironmentVariable("CONN"));
    }
    
}