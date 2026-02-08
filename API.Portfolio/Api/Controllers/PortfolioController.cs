using API.Portfolio.Application.Queries;
using API.Portfolio.Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<IActionResult> GetData([FromBody] StoredProcedureRequest request)
        {
            var allowed = new[]
            {
                "get_contact",
                "get_aptitudes",
                "get_experiencia",
                "get_skills",
                "get_totalexperiencia",
                "get_formacion"
            };

            Console.WriteLine($"Controller: petición recibida con sp={request.Sp}, usuarioId={request.UsuarioId}");

            if (!allowed.Contains(request.Sp))
            {
                Console.WriteLine("Controller: stored procedure no permitida");
                return BadRequest("Stored procedure no permitida");
            }

            var result = await _mediator.Send(new GetDataByStoredProcedureQuery(request.Sp, request.UsuarioId));

            Console.WriteLine("Controller: resultado obtenido del mediator");
            return Ok(result);
        }

    }
}