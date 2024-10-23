using SimpleGame.Domain.Interfaces;
using SimpleGame.Domain.Models;

namespace SimpleGame.Application.Services
{
    public class ScoreboardService
    {
        private readonly IScoreboardRepository _scoreboardRepository;

        public ScoreboardService(IScoreboardRepository scoreboardRepository)
        {
            _scoreboardRepository = scoreboardRepository;
        }

        public async Task AddResultAsync(GameResult result)
        {
            await _scoreboardRepository.AddResultAsync(result);
        }

        public async Task<List<GameResult>> GetRecentResultsAsync()
        {
            return await _scoreboardRepository.GetRecentResultsAsync();
        }

        public async Task ResetScoreboardAsync()
        {
            await _scoreboardRepository.ResetScoreboardAsync();
        }
    }
}
