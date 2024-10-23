using MediatR;
using SimpleGame.ScoreboardService.Core.Application.Dtos;
using SimpleGame.ScoreboardService.Core.Domain.Interfaces;

namespace SimpleGame.ScoreboardService.Core.Application.Queries.GetLastResults
{
    public class GetLastResultsQueryHandler : IRequestHandler<GetLastResultsQuery, IEnumerable<GameResultDto>>
    {
        private readonly IScoreboardService _scoreboardService;
        private readonly ILogger<GetLastResultsQueryHandler> _logger;

        public GetLastResultsQueryHandler(IScoreboardService scoreboardService, ILogger<GetLastResultsQueryHandler> logger)
        {
            _scoreboardService = scoreboardService;
            _logger = logger;
        }

        /// <summary>
        /// Handles the query to retrieve the last game results from the scoreboard.
        /// </summary>
        /// <param name="request">The query request.</param>
        /// <param name="cancellationToken">Cancellation token to observe while waiting for the task to complete.</param>
        /// <returns>A task representing the asynchronous operation, containing the list of last game results.</returns>
        public Task<IEnumerable<GameResultDto>> Handle(GetLastResultsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var results = _scoreboardService.GetLastResults();
                _logger.LogInformation("Fetched last results successfully");
                return Task.FromResult(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching last results from scoreboard");
                throw;
            }
        }
    }
}
