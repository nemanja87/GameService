using MediatR;
using SimpleGame.Application.Dtos;
using SimpleGame.Domain.Interfaces;

namespace SimpleGame.Application.Queries.GetScoarboard
{
    public class GetScoreboardQueryHandler : IRequestHandler<GetScoreboardQuery, List<GameResultDto>>
    {
        private readonly IScoreboardRepository _scoreboardRepository;

        public GetScoreboardQueryHandler(IScoreboardRepository scoreboardRepository) => _scoreboardRepository = scoreboardRepository;

        public async Task<List<GameResultDto>> Handle(GetScoreboardQuery request, CancellationToken cancellationToken)
        {
            var results = await _scoreboardRepository.GetRecentResultsAsync();

            var resultDtos = results.Select(result => new GameResultDto
            {
                PlayerChoice = result.PlayerChoice.ToString(),
                ComputerChoice = result.ComputerChoice.ToString(),
                Result = result.Result.ToString()
            }).ToList();

            return resultDtos;
        }
    }
}
