using SimpleGame.Domain.Enums;

namespace SimpleGame.Domain.Models
{
    public class GameResult(GameChoiceEnum playerChoice, GameChoiceEnum computerChoice, GameResultEnum result)
    {
        public GameChoiceEnum PlayerChoice { get; set; } = playerChoice;
        public GameChoiceEnum ComputerChoice { get; set; } = computerChoice;
        public GameResultEnum Result { get; set; } = result;
    }
}
