using SimpleGame.ScoreboardService.Core.Application.Dtos;
using SimpleGame.ScoreboardService.Core.Domain.Interfaces;

namespace SimpleGame.ScoreboardService.Core.Application.Services
{
    public class ScoreboardService : IScoreboardService
    {
        private readonly Queue<GameResultDto> _results;

        public ScoreboardService()
        {
            _results = new Queue<GameResultDto>(10);
        }

        public void AddResult(GameResultDto result)
        {
            if (_results.Count >= 10)
            {
                _results.Dequeue();
            }
            _results.Enqueue(result);
        }

        public IEnumerable<GameResultDto> GetLastResults()
        {
            return [.. _results];
        }

        // TODO: Replace this with actual database operations when persistence layer is added.
    }
}
