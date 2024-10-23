using MediatR;
using Microsoft.AspNetCore.Mvc;
using SimpleGame.ScoreboardService.Core.Application.Commands.AddScore;
using SimpleGame.ScoreboardService.Core.Application.Dtos;
using SimpleGame.ScoreboardService.Core.Application.Queries.GetLastResults;

namespace SimpleGame.ScoreboardService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScoreboardController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ScoreboardController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Adds a game result to the scoreboard.
        /// </summary>
        /// <param name="gameResult">The game result to add.</param>
        /// <returns>An action result indicating success or failure.</returns>
        [HttpPost("add-result")]
        public async Task<IActionResult> AddResult([FromBody] GameResultDto gameResult)
        {
            var command = new AddScoreCommand(gameResult);
            await _mediator.Send(command);
            return Ok();
        }

        /// <summary>
        /// Retrieves the last results from the scoreboard.
        /// </summary>
        /// <returns>A list of the last game results.</returns>
        [HttpGet("last-results")]
        public async Task<IActionResult> GetLastResults()
        {
            var query = new GetLastResultsQuery();
            var results = await _mediator.Send(query);
            return Ok(results);
        }
    }
}