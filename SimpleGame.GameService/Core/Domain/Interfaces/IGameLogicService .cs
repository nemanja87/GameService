using SimpleGame.GameService.Core.Domain.Enums;
using SimpleGame.GameService.Core.Domain.Models;

namespace SimpleGame.GameService.Core.Domain.Interfaces
{
    public interface IGameLogicService
    {
        GameResult Play(GameChoiceEnum playerChoice, GameChoiceEnum computerChoice);
    }
}