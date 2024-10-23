using MediatR;
using SimpleGame.GameServiceApp.Core.Application.Dtos;
using SimpleGame.GameServiceApp.Core.Domain.Enums;

namespace SimpleGame.GameServiceApp.Core.Application.Commands.PlayGame
{
    public class PlayGameCommand : IRequest<GameResultDto>
    {
        public GameChoiceEnum PlayerChoice { get; set; }
    }
}
