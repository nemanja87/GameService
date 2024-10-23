using MediatR;
using SimpleGame.GameService.Core.Application.Dtos;
using SimpleGame.GameService.Core.Domain.Enums;

namespace SimpleGame.GameService.Core.Application.Queries.GetChoices
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
