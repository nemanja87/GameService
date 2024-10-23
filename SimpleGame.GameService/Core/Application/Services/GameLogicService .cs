using SimpleGame.GameService.Core.Domain.Enums;
using SimpleGame.GameService.Core.Domain.Interfaces;
using SimpleGame.GameService.Core.Domain.Models;

namespace SimpleGame.GameService.Core.Application.Services
{
    public class GameLogicService : IGameLogicService
    {
        public GameResult Play(GameChoiceEnum playerChoice, GameChoiceEnum computerChoice)
        {
            if (playerChoice == computerChoice)
            {
                return new GameResult(playerChoice, computerChoice, GameResultEnum.Tie);
            }

            if ((playerChoice == GameChoiceEnum.Rock && (computerChoice == GameChoiceEnum.Scissors || computerChoice == GameChoiceEnum.Lizard)) ||
                (playerChoice == GameChoiceEnum.Paper && (computerChoice == GameChoiceEnum.Rock || computerChoice == GameChoiceEnum.Spock)) ||
                (playerChoice == GameChoiceEnum.Scissors && (computerChoice == GameChoiceEnum.Paper || computerChoice == GameChoiceEnum.Lizard)) ||
                (playerChoice == GameChoiceEnum.Lizard && (computerChoice == GameChoiceEnum.Spock || computerChoice == GameChoiceEnum.Paper)) ||
                (playerChoice == GameChoiceEnum.Spock && (computerChoice == GameChoiceEnum.Rock || computerChoice == GameChoiceEnum.Scissors)))
            {
                return new GameResult(playerChoice, computerChoice, GameResultEnum.Win);
            }

            return new GameResult(playerChoice, computerChoice, GameResultEnum.Lose);
        }
    }
}
