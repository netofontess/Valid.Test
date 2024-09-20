using MediatR;
using Microsoft.AspNetCore.Mvc;
using Valid.Test.Application.Commands;

namespace Valid.Test.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProtocoloController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost("")]
        public async Task<IActionResult> Post([FromForm] GravarProtocoloCommand gravarProtocoloCommand)
        {
            await _mediator.Send(gravarProtocoloCommand);

            return Ok(gravarProtocoloCommand);
        }

        [HttpGet("{numeroProtocolo}")]
        public IActionResult Get(string numeroProtocolo)
        {
            return Ok(numeroProtocolo);
        }
    }
}
