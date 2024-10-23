using MediatR;
using Microsoft.AspNetCore.Mvc;
using SimpleGame.ComputerService.Core.Application.Queries.AddComputerChoice;

namespace SimpleGame.ComputerService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComputerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ComputerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("choice")]
        public async Task<IActionResult> GetComputerChoice()
        {
            var result = await _mediator.Send(new GetComputerChoiceCommand());
            return Ok(result);
        }
    }
}
