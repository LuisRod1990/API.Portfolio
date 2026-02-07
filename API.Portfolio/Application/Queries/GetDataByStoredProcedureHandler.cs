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

        // Lista blanca de funciones permitidas
        private readonly HashSet<string> _allowedFunctions = new()
        {
            "get_contact",
            "get_aptitudes",
             "get_experiencia",
            "get_skills"


                , "get_totalexperiencia"
                , "get_formacion"
        };

        public GetDataByStoredProcedureHandler(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<dynamic>> Handle(GetDataByStoredProcedureQuery request, CancellationToken ct)
        {
            using var connection = _context.CreateConnection();
            try
            {
                // Validar que la función esté permitida
                if (!_allowedFunctions.Contains(request.StoredProcedure))
                    throw new ArgumentException($"Función no permitida: {request.StoredProcedure}");

                // Construir el SQL dinámicamente con esquema dbo
                var sql = $"SELECT * FROM dbo.{request.StoredProcedure}(@UsuarioId);";

                var result = await connection.QueryAsync(sql, new { UsuarioId = request.UsuarioId });

                return result;
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Función no permitida: {request.StoredProcedure}");
            }
            
        }
    }
}