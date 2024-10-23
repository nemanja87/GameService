using SimpleGame.Domain.Models;

namespace SimpleGame.Domain.Interfaces
{
    public interface IScoreboardRepository
    {
        Task AddResultAsync(GameResult result);
        Task<List<GameResult>> GetRecentResultsAsync();
        Task ResetScoreboardAsync();
    }
}
