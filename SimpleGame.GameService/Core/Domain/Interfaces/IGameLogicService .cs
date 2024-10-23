using SimpleGame.GameServiceApp.Core.Domain.Enums;
using SimpleGame.GameServiceApp.Core.Domain.Models;

namespace SimpleGame.GameServiceApp.Core.Domain.Interfaces
{
    public interface IGameLogicService
    {
        GameResult Play(GameChoiceEnum playerChoice, GameChoiceEnum computerChoice);
    }
}