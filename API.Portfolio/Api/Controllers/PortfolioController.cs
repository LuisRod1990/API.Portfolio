using API.Portfolio.Application.Queries;
using API.Portfolio.Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;

namespace PortfolioApi.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PortfolioController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PortfolioController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpGet("index")]
        public IActionResult Register()
        {
            return Ok("Success...");
        }

        // POST: api/portfolio
        [HttpPost]
        public async Task<IActionResult> GData([FromBody] StoredProcedureRequest request)
        {
            var allowedPortfolio = new[]
            {
                "get_contact","get_aptitudes","get_experiencia","get_skills","get_totalexperiencia","get_formacion", "get_proyectospersonales"
            };
            var allowedECommerce = new[]
            {
                "obtener_precios","obtener_productos","obtener_inventario", "obtener_ordenes", "obtener_estatus",
                "obtener_notificaciones", "obtener_facturas", "obtener_logs_por_semana","obtener_logs_por_nivel", 
                "obtener_tokens_por_semana", "obtener_auditoria_por_funcion", "obtener_tokens_por_estatus",
                "actualizar_producto", "actualizar_inventario", "actualizar_precio", "actualizar_orden",
                "insertar_producto", "insertar_inventario", "insertar_precio", "insertar_orden",
                "eliminar_producto", "eliminar_inventario", "eliminar_precio", "eliminar_orden",

            };

            Console.WriteLine($"Controller: petición recibida con sp={request.Sp}, usuarioId={request.UsuarioId}");
            if (allowedPortfolio.Contains(request.Sp))
            {
                var result = await _mediator.Send(new GetDataByStoredProcedureQuery(request.Sp, request.UsuarioId, request.Id, request.Data));
                Console.WriteLine("Controller: resultado obtenido del mediator");
                return Ok(result);
            }
            else if (allowedECommerce.Contains(request.Sp))
            {
                var result = await _mediator.Send(new GetDataByStoredProcedureQuery(request.Sp, request.UsuarioId, request.Id, request.Data));
                Console.WriteLine("Controller: resultado obtenido del mediator");
                return Ok(result);
            }
            else
            {
                Console.WriteLine("Controller: stored procedure no permitida");
                return BadRequest("Stored procedure no permitida");
            }
        }
        // PUT: api/portfolio
        [HttpPut]
        public async Task<IActionResult> UData([FromBody] StoredProcedureRequest request)
        {
            var allowed = new[]
            {
                "get_contact","get_aptitudes","get_experiencia","get_skills","get_totalexperiencia","get_formacion",
                "actualizar_producto","actualizar_inventario","actualizar_precio", "actualizar_orden",
            };
            Console.WriteLine($"Controller: petición recibida con sp={request.Sp}, usuarioId={request.UsuarioId}");
            if (!allowed.Contains(request.Sp))
            {
                Console.WriteLine("Controller: stored procedure no permitida");
                return BadRequest("Stored procedure no permitida");
            }
            var result = await _mediator.Send(new GetDataByStoredProcedureQuery(request.Sp, request.UsuarioId, request.Id, request.Data));
            Console.WriteLine("Controller: resultado obtenido del mediator");
            return Ok(result);
        }
        // PUT: api/portfolio
        [HttpDelete]
        public async Task<IActionResult> DData([FromBody] StoredProcedureRequest request)
        {
            var allowed = new[]
            {
                "eliminar_producto", "eliminar_inventario", "eliminar_precio", "eliminar_orden"
            };
            Console.WriteLine($"Controller: petición recibida con sp={request.Sp}, usuarioId={request.UsuarioId}");
            if (!allowed.Contains(request.Sp))
            {
                Console.WriteLine("Controller: stored procedure no permitida");
                return BadRequest("Stored procedure no permitida");
            }
            var result = await _mediator.Send(new GetDataByStoredProcedureQuery(request.Sp, request.UsuarioId, request.Id, request.Data));
            Console.WriteLine("Controller: resultado obtenido del mediator");
            return Ok(result);
        }
    }
}