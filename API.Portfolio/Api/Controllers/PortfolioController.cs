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
        [HttpGet("{sp}/{usuarioId}")]
        public async Task<IActionResult> GetData(string sp, int usuarioId)
        {
            // Lista blanca de SP permitidos
            var allowed = new[] { "Get_Contact"
                , "Get_Aptitudes"
                , "Get_Skills"
                , "Get_Experiencia"
                , "Get_TotalExperiencia"
                , "Get_Formacion"
            };
            if (!allowed.Contains(sp))
                return BadRequest("Stored procedure no permitida");

            var result = await _mediator.Send(new GetDataByStoredProcedureQuery(sp, usuarioId));
            return Ok(result);
        }
    }
}