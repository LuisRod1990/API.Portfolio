using Microsoft.Data.SqlClient;
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

        public IDbConnection CreateConnection()
            => new SqlConnection(Environment.GetEnvironmentVariable("CONN"));
    }
}