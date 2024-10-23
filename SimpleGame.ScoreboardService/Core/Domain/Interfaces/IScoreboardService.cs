using SimpleGame.ScoreboardService.Core.Application.Dtos;

namespace SimpleGame.ScoreboardService.Core.Domain.Interfaces
{
    public interface IScoreboardService
    {
        void AddResult(GameResultDto result);
        IEnumerable<GameResultDto> GetLastResults();
    }
}
