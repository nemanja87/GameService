using MediatR;
using SimpleGame.GameServiceApp.Core.Application.Dtos;

namespace SimpleGame.GameServiceApp.Core.Application.Queries.GetChoices
{
    public class GetChoicesQuery : IRequest<List<GameChoiceDto>>
    {
    }
}
