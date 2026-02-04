using MediatR;

namespace API.Portfolio.Application.Queries
{
    public record GetDataByStoredProcedureQuery(string StoredProcedure, int UsuarioId)
    : IRequest<IEnumerable<object>>;
}
