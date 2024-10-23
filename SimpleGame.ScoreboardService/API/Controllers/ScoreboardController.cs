using MediatR;
using Microsoft.AspNetCore.Mvc;
using SimpleGame.ScoreboardService.Core.Application.Commands.AddScore;
using SimpleGame.ScoreboardService.Core.Application.Dtos;
using SimpleGame.ScoreboardService.Core.Application.Queries.GetLastResults;

namespace SimpleGame.ScoreboardService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScoreboardController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ScoreboardController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("add-result")]
        public async Task<IActionResult> AddResult([FromBody] GameResultDto gameResult)
        {
            var command = new AddScoreCommand(gameResult);
            await _mediator.Send(command);
            return Ok();
        }

        [HttpGet("last-results")]
        public async Task<IActionResult> GetLastResults()
        {
            var query = new GetLastResultsQuery();
            var results = await _mediator.Send(query);
            return Ok(results);
        }
    }
}
