using MediatR;
using Microsoft.AspNetCore.Mvc;
using SimpleGame.GameService.Core.Application.Clients.Services;
using SimpleGame.GameService.Core.Application.Commands.PlayGame;
using SimpleGame.GameService.Core.Domain.Enums;
using SimpleGame.GameService.Core.Domain.Interfaces;
using SimpleGame.GameService.Core.Domain.Models;

namespace SimpleGame.GameService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<GameController> _logger;


        public GameController(IMediator mediator, ILogger<GameController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Plays the game by comparing the player's choice with the computer's random choice.
        /// </summary>
        /// <param name="playerChoice">The player's game choice (Rock, Paper, Scissors, Lizard, Spock).</param>
        /// <returns>Returns the result of the game (Win, Lose, or Tie) based on the player's and computer's choices.</returns>
        /// <response code="200">Returns the game result.</response>
        /// <response code="400">If the provided player choice is invalid.</response>
        [HttpPost("play")]
        [ProducesResponseType(typeof(GameResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public async Task<IActionResult> PlayGame([FromBody] GameChoiceEnum playerChoice)
        {
            try
            {
                // Create and send the PlayGameCommand to MediatR
                var result = await _mediator.Send(new PlayGameCommand
                {
                    PlayerChoice = playerChoice
                });

                return Ok(result);  // Return the result from the handler
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during PlayGame");
                return StatusCode(500, "Internal server error");
            }
        }

        private GameChoiceEnum MapToGameChoiceEnum(string name)
        {
            return name.ToLower() switch
            {
                "rock" => GameChoiceEnum.Rock,
                "paper" => GameChoiceEnum.Paper,
                "scissors" => GameChoiceEnum.Scissors,
                "lizard" => GameChoiceEnum.Lizard,
                "spock" => GameChoiceEnum.Spock,
                _ => throw new ArgumentException("Invalid game choice")
            };
        }
    }
}
