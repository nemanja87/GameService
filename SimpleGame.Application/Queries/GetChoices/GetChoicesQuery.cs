using MediatR;
using SimpleGame.Application.Dtos;

namespace SimpleGame.Application.Queries.GetChoices
{
    public class GetChoicesQuery : IRequest<List<GameChoiceDto>>
    {
    }
}
