using API.Portfolio.Application.Queries;
using Dapper;
using MediatR;
using PortfolioApi.Infrastructure.DataAccess;
using System.Data;

namespace PortfolioApi.Application.Queries
{
    public class GetDataByStoredProcedureHandler
        : IRequestHandler<GetDataByStoredProcedureQuery, IEnumerable<object>>
    {
        private readonly DapperContext _context;

        public GetDataByStoredProcedureHandler(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<dynamic>> Handle(GetDataByStoredProcedureQuery request, CancellationToken ct)
        {
            using var connection = _context.CreateConnection();

            var result = await connection.QueryAsync(
                request.StoredProcedure,
                new { UsuarioId = request.UsuarioId },
                commandType: CommandType.StoredProcedure);

            return result;
        }
    }
}