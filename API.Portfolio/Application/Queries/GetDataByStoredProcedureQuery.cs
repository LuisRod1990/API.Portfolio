using MediatR;

namespace API.Portfolio.Application.Queries
{
    public record GetDataByStoredProcedureQuery(string StoredProcedure, int UsuarioId, int id, string? parametros)
    : IRequest<IEnumerable<object>>;

}
