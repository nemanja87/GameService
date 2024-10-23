using MediatR;
using SimpleGame.Application.Dtos;

namespace SimpleGame.Application.Queries.GetScoarboard
{
    public class GetScoreboardQuery : IRequest<List<GameResultDto>>
    {
    }
}
