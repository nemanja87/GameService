using SimpleGame.Domain.Enums;
using SimpleGame.Domain.Models;

namespace SimpleGame.Domain.Interfaces
{
    public interface IGameService
    {
        GameResult Play(GameChoiceEnum playerChoice, GameChoiceEnum computerChoice);
    }
}