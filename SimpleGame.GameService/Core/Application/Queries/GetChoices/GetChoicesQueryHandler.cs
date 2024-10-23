using MediatR;
using SimpleGame.GameServiceApp.Core.Application.Dtos;
using SimpleGame.GameServiceApp.Core.Domain.Enums;

namespace SimpleGame.GameServiceApp.Core.Application.Queries.GetChoices
{
    public class GetChoicesQueryHandler : IRequestHandler<GetChoicesQuery, List<GameChoiceDto>>
    {
        public Task<List<GameChoiceDto>> Handle(GetChoicesQuery request, CancellationToken cancellationToken)
        {
            var choices = Enum.GetValues(typeof(GameChoiceEnum))
                .Cast<GameChoiceEnum>()
                .Select(choice => new GameChoiceDto { Id = (int)choice, Name = choice.ToString() })
                .ToList();

            return Task.FromResult(choices);
        }
    }
}
