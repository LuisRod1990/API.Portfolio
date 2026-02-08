using API.Portfolio.Application.Queries;
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
        
        [HttpGet("{sp}/{usuarioId}")]
        public async Task<IActionResult> GetData(string sp, int usuarioId)
        {
            var allowed = new[] { "get_contact", "get_aptitudes", "get_experiencia", "get_skills", "get_totalexperiencia", "get_formacion" };
            if (!allowed.Contains(sp))
                return BadRequest("Stored procedure no permitida");

            var result = await _mediator.Send(new GetDataByStoredProcedureQuery(sp, usuarioId));
            return Ok(result);
        }
    }
}