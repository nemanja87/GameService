using MediatR;
using SimpleGame.GameService.Core.Application.Dtos;

namespace SimpleGame.GameService.Core.Application.Queries.GetChoices
{
    public class GetChoicesQuery : IRequest<List<GameChoiceDto>>
    {
    }
}
