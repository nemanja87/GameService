using SimpleGame.ScoreboardService.Core.Application.Dtos;
using SimpleGame.ScoreboardService.Core.Domain.Interfaces;

namespace SimpleGame.ScoreboardService.Core.Application.Services
{
    public class ScoreboardService : IScoreboardService
    {
        private readonly Queue<GameResultDto> _results;
        private readonly ILogger<ScoreboardService> _logger;

        public ScoreboardService(ILogger<ScoreboardService> logger)
        {
            _results = new Queue<GameResultDto>(10);
            _logger = logger;
        }

        /// <summary>
        /// Adds a game result to the scoreboard.
        /// </summary>
        /// <param name="result">The game result to add.</param>
        public void AddResult(GameResultDto result)
        {
            try
            {
                if (_results.Count >= 10)
                {
                    _results.Dequeue();
                }
                _results.Enqueue(result);
                _logger.LogInformation("Added game result to scoreboard");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding game result to scoreboard");
                throw;
            }
        }

        /// <summary>
        /// Retrieves the last game results from the scoreboard.
        /// </summary>
        /// <returns>An enumerable list of game results.</returns>
        public IEnumerable<GameResultDto> GetLastResults()
        {
            try
            {
                return _results.ToArray();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving last results from scoreboard");
                throw;
            }
        }
    }
}
