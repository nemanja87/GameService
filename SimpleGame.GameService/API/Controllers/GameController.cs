using Microsoft.AspNetCore.Mvc;
using SimpleGame.GameServiceApp.Core.Application.Clients.Services;
using SimpleGame.GameServiceApp.Core.Application.Dtos;
using SimpleGame.GameServiceApp.Core.Domain.Enums;
using SimpleGame.GameServiceApp.Core.Domain.Interfaces;
using SimpleGame.GameServiceApp.Core.Domain.Models;

namespace SimpleGame.GameServiceApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameController : ControllerBase
    {
        private readonly IGameSetupService _gameSetupService;
        private readonly IGameLogicService _gameLogicService;
        private readonly IComputerServiceClient _computerServiceClient;
        private readonly ILogger<GameController> _logger;


        public GameController(IComputerServiceClient computerServiceClient, IGameSetupService gameSetupService, IGameLogicService gameLogicService, ILogger<GameController> logger)
        {
            _gameSetupService = gameSetupService;
            _gameLogicService = gameLogicService;
            _computerServiceClient = computerServiceClient;
            _logger = logger;
        }

        /// <summary>
        /// Initializes the game by fetching computer details and setting up the game environment.
        /// </summary>
        /// <param name="computerId">The ID of the computer to fetch details from the ComputerService.</param>
        /// <returns>Returns the game setup details including computer specifications and default game settings.</returns>
        /// <response code="200">Returns the game setup details successfully.</response>
        /// <response code="500">If there was an error while fetching computer details or initializing the game.</response>
        [HttpPost("initialize")]
        [ProducesResponseType(typeof(GameSetup), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        public async Task<IActionResult> InitializeGame([FromBody] int computerId)
        {
            try
            {
                var gameSetup = await _gameSetupService.InitializeGameAsync(computerId);
                return Ok(gameSetup);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
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
        public IActionResult PlayGame([FromBody] GameChoiceEnum playerChoice)
        {
            try
            {
                // Fetch the computer's choice from ComputerService
                var computerDetails = _computerServiceClient.GetComputerDetailsAsync(0).Result;
                GameChoiceEnum computerChoice = MapToGameChoiceEnum(computerDetails.Name);

                var gameResult = _gameLogicService.Play(playerChoice, computerChoice);

                // Map to DTO for string representation
                var resultDto = new GameResultDto
                {
                    PlayerChoice = playerChoice.ToString(),
                    ComputerChoice = computerChoice.ToString(),
                    Result = gameResult.Result.ToString()
                };

                return Ok(resultDto);
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
