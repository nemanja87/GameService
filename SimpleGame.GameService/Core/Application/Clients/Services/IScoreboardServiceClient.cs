using SimpleGame.GameService.Core.Application.Dtos;

namespace SimpleGame.GameService.Core.Application.Clients.Services;

public interface IScoreboardServiceClient
{
    Task SaveGameResult(GameResultDto gameResultDto);
}
