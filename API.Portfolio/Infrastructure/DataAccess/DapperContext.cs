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
            //=> new NpgsqlConnection(Environment.GetEnvironmentVariable("CONN"));
            => new NpgsqlConnection("Host=db-cv.ctoeigs0euz0.us-east-2.rds.amazonaws.com;Port=5432;Database=db-cv;Username=postgres;Password=MxN1990*-;Ssl Mode = Require; Trust Server Certificate=true");
    }
    
}