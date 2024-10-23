using SimpleGame.GameService.Core.Domain.Enums;
using SimpleGame.GameService.Core.Domain.Interfaces;
using SimpleGame.GameService.Core.Domain.Models;

namespace SimpleGame.GameService.Core.Application.Services
{
    public class GameLogicService : IGameLogicService
    {
        private readonly ILogger<GameLogicService> _logger;

        public GameLogicService(ILogger<GameLogicService> logger)
        {
            _logger = logger;
        }

        public GameResult Play(GameChoiceEnum playerChoice, GameChoiceEnum computerChoice)
        {
            _logger.LogInformation("Starting game logic. PlayerChoice: {PlayerChoice}, ComputerChoice: {ComputerChoice}", playerChoice, computerChoice);

            if (playerChoice == computerChoice)
            {
                _logger.LogInformation("Game result: Tie. Both player and computer chose {Choice}", playerChoice);
                return new GameResult(playerChoice, computerChoice, GameResultEnum.Tie);
            }


            // Check game result based on the business logic of the game
            bool isPlayerWin = (playerChoice == GameChoiceEnum.Rock && (computerChoice == GameChoiceEnum.Scissors || computerChoice == GameChoiceEnum.Lizard)) ||
                               (playerChoice == GameChoiceEnum.Paper && (computerChoice == GameChoiceEnum.Rock || computerChoice == GameChoiceEnum.Spock)) ||
                               (playerChoice == GameChoiceEnum.Scissors && (computerChoice == GameChoiceEnum.Paper || computerChoice == GameChoiceEnum.Lizard)) ||
                               (playerChoice == GameChoiceEnum.Lizard && (computerChoice == GameChoiceEnum.Spock || computerChoice == GameChoiceEnum.Paper)) ||
                               (playerChoice == GameChoiceEnum.Spock && (computerChoice == GameChoiceEnum.Rock || computerChoice == GameChoiceEnum.Scissors));

            var result = isPlayerWin ? GameResultEnum.Win : GameResultEnum.Lose;

            _logger.LogInformation("Game result: {Result}. PlayerChoice: {PlayerChoice}, ComputerChoice: {ComputerChoice}", result, playerChoice, computerChoice);

            return new GameResult(playerChoice, computerChoice, result);
        }
    }
}
