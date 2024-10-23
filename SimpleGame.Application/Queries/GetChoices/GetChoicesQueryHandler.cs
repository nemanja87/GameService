using MediatR;
using SimpleGame.Application.Dtos;
using SimpleGame.Domain.Enums;

namespace SimpleGame.Application.Queries.GetChoices
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
