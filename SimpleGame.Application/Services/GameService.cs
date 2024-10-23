using SimpleGame.Domain.Enums;
using SimpleGame.Domain.Interfaces;
using SimpleGame.Domain.Models;

namespace SimpleGame.Application.Services
{

    public class GameService : IGameService
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
