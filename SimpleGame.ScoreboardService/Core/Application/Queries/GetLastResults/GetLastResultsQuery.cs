using MediatR;
using SimpleGame.ScoreboardService.Core.Application.Dtos;

namespace SimpleGame.ScoreboardService.Core.Application.Queries.GetLastResults
{
    public class GetLastResultsQuery : IRequest<IEnumerable<GameResultDto>>
    {
    }
}
