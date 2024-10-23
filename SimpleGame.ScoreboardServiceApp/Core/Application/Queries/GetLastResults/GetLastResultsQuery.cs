using MediatR;
using SimpleGame.ScoreboardServiceApp.Core.Application.Dtos;

namespace SimpleGame.ScoreboardServiceApp.Core.Application.Queries.GetLastResults
{
    public class GetLastResultsQuery : IRequest<IEnumerable<GameResultDto>>
    {
    }
}
