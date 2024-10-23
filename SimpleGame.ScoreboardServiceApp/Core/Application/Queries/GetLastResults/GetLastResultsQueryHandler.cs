using MediatR;
using SimpleGame.ScoreboardServiceApp.Core.Application.Dtos;
using SimpleGame.ScoreboardServiceApp.Core.Domain.Interfaces;

namespace SimpleGame.ScoreboardServiceApp.Core.Application.Queries.GetLastResults
{
    public class GetLastResultsQueryHandler : IRequestHandler<GetLastResultsQuery, IEnumerable<GameResultDto>>
    {
        private readonly IScoreboardService _scoreboardService;

        public GetLastResultsQueryHandler(IScoreboardService scoreboardService)
        {
            _scoreboardService = scoreboardService;
        }

        public Task<IEnumerable<GameResultDto>> Handle(GetLastResultsQuery request, CancellationToken cancellationToken)
        {
            var results = _scoreboardService.GetLastResults();
            return Task.FromResult(results);
        }
    }
}
