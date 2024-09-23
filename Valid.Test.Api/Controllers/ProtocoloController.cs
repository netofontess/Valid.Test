using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Valid.Test.Application.UseCases.ProtocoloCase.Create;
using Valid.Test.Application.UseCases.ProtocoloCase.Get;
using Valid.Test.Domain.Helpers;

namespace Valid.Test.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class ProtocoloController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost("")]
        public async Task<IActionResult> CriaProtocolo([FromForm] GravarProtocoloCommand gravarProtocoloCommand)
        {
            await _mediator.Send(gravarProtocoloCommand);

            return Ok(gravarProtocoloCommand);
        }

        [HttpGet("")]
        public async Task<IActionResult> ConsultaProtocolo(FiltroProtocolo filtro)
        {
            var query = new ConsultaProtocoloQuery(filtro);
            var resultado = await _mediator.Send(query);

            return Ok(resultado);
        }
    }
}
