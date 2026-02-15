using API.Portfolio.Application.Queries;
using Dapper;
using MediatR;
using PortfolioApi.Infrastructure.DataAccess;

namespace PortfolioApi.Application.Queries
{
    public class GetDataByStoredProcedureHandler
        : IRequestHandler<GetDataByStoredProcedureQuery, IEnumerable<object>>
    {
        private readonly DapperContext _context;

        // Lista blanca de funciones permitidas
        private readonly HashSet<string> _allowedFunctionsPortfolio = new()
        {
            "get_contact","get_aptitudes","get_experiencia","get_skills", "get_totalexperiencia", "get_formacion", "get_proyectospersonales"
        };
        // Lista blanca de funciones permitidas ECommerce
        private readonly HashSet<string> _allowedFunctionsECommerce = new()
        {
            "obtener_precios","obtener_productos","obtener_inventario", "obtener_ordenes", "obtener_estatus",
            "obtener_notificaciones", "obtener_facturas", "obtener_logs_por_semana", "obtener_logs_por_nivel",
            "obtener_tokens_por_semana", "obtener_auditoria_por_funcion", "obtener_tokens_por_estatus",
                "actualizar_producto", "actualizar_inventario", "actualizar_precio", "actualizar_orden",
                "insertar_producto", "insertar_inventario", "insertar_precio", "insertar_orden",
                "eliminar_producto", "eliminar_inventario", "eliminar_precio", "eliminar_orden",
        };

        public GetDataByStoredProcedureHandler(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<dynamic>> Handle(GetDataByStoredProcedureQuery request, CancellationToken ct)
        {
            using var connection = _context.CreateConnection();
            if (!_allowedFunctionsPortfolio.Contains(request.StoredProcedure)&& !_allowedFunctionsECommerce.Contains(request.StoredProcedure))
                throw new ArgumentException($"Función no permitida: {request.StoredProcedure}");
            try
            {
                if (_allowedFunctionsPortfolio.Contains(request.StoredProcedure))
                {
                    var sql = $"SELECT * FROM dbo.{request.StoredProcedure}(@UsuarioId);";
                    var result = await connection.QueryAsync(sql, new { UsuarioId = request.UsuarioId });
                    return result;
                }
                else if (_allowedFunctionsECommerce.Contains(request.StoredProcedure))
                {
                    var sql = $"SELECT * FROM tienda.{request.StoredProcedure}(@UsuarioId,@Id,@Param);";
                    var result = await connection.QueryAsync(sql, new { UsuarioId = request.UsuarioId, Id = request.id, Param = request.parametros });
                    return result;
                }
                else
                {
                    return Enumerable.Empty<dynamic>();
                }
            }
            catch (Exception ex)
            {
                var sql = $"SELECT dbo.registrar_log(@UsuarioId, @LogLevel, @Logger, @Message, @Exception, @Thread, @MachineName);";
                await connection.ExecuteAsync(sql, new
                {
                    UsuarioId = request.UsuarioId,  
                    LogLevel = "ERROR",
                    Logger = "ApiClient",
                    Message = "Error ejecutando SP " + request.StoredProcedure,
                    Exception = ex.ToString(),
                    Thread = System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(),
                    MachineName = Environment.MachineName
                });

                throw;
            }


        }
        public async Task<IEnumerable<dynamic>> HandlePut(GetDataByStoredProcedureQuery request, CancellationToken ct)
        {
            using var connection = _context.CreateConnection();
            // Validar que la función esté permitida
            if (!_allowedFunctionsPortfolio.Contains(request.StoredProcedure))
                throw new ArgumentException($"Función no permitida: {request.StoredProcedure}");
            // Construir el PostgreSQL dinámicamente con esquema dbo
            var sql = $"SELECT * FROM tienda.{request.StoredProcedure}(@UsuarioId,@Id);";
            var result = await connection.QueryAsync(sql, new { UsuarioId = request.UsuarioId, Id = request.id, request.parametros });
            return result;
        }

    }
}