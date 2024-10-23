using SimpleGame.ScoreboardServiceApp.Core.Application.Dtos;

namespace SimpleGame.ScoreboardServiceApp.Core.Domain.Interfaces
{
    public interface IScoreboardService
    {
        void AddResult(GameResultDto result);
        IEnumerable<GameResultDto> GetLastResults();
    }
}
