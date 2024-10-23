using MediatR;
using Microsoft.AspNetCore.Mvc;
using SimpleGame.GameService.Core.Application.Commands.PlayGame;
using SimpleGame.GameService.Core.Application.Dtos;
using SimpleGame.GameService.Core.Domain.Enums;

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
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Plays the game by comparing the player's choice with the computer's random choice.
        /// </summary>
        /// <param name="playerChoice">The player's game choice (Rock, Paper, Scissors, Lizard, Spock).</param>
        /// <returns>Returns the result of the game (Win, Lose, or Tie) based on the player's and computer's choices.</returns>
        /// <response code="200">Returns the game result.</response>
        /// <response code="400">If the provided player choice is invalid.</response>
        /// <response code="500">If an internal server error occurs.</response>
        [HttpPost("play")]
        [ProducesResponseType(typeof(GameResultDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        public async Task<IActionResult> PlayGame([FromBody] GameChoiceEnum playerChoice)
        {
            _logger.LogInformation("PlayGame action called with player choice: {PlayerChoice}", playerChoice);

            try
            {
                // Validate the player's choice
                if (!Enum.IsDefined(typeof(GameChoiceEnum), playerChoice))
                {
                    _logger.LogWarning("Invalid player choice: {PlayerChoice}", playerChoice);
                    return BadRequest("Invalid player choice.");
                }

                // Create and send the PlayGameCommand
                var result = await _mediator.Send(new PlayGameCommand
                {
                    PlayerChoice = playerChoice
                });

                _logger.LogInformation("Game result successfully returned for player choice: {PlayerChoice}", playerChoice);

                return Ok(result);  // Return the result from the handler
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Bad request due to invalid input: {Message}", ex.Message);
                return BadRequest(ex.Message);  // Return 400 Bad Request for invalid input
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during the PlayGame operation");
                return StatusCode(500, "Internal server error");
            }
        }

        private GameChoiceEnum MapToGameChoiceEnum(string name)
        {
            _logger.LogInformation("Mapping string '{Name}' to GameChoiceEnum", name);
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
