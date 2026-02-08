namespace API.Portfolio.Domain.Dtos
{
    public class StoredProcedureRequest
    {
        public string  Sp { get; set; } = string.Empty;
        public int UsuarioId { get; set; }

    }
}
