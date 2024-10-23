using MediatR;
using SimpleGame.GameService.Core.Application.Dtos;
using SimpleGame.GameService.Core.Domain.Enums;

namespace SimpleGame.GameService.Core.Application.Commands.PlayGame
{
    public class PlayGameCommand : IRequest<GameResultDto>
    {
        public GameChoiceEnum PlayerChoice { get; set; }
    }
}
