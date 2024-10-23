using SimpleGame.GameService.Core.Domain.Models;

namespace SimpleGame.GameService.Core.Domain.Interfaces
{
    public interface IGameSetupService
    {
        Task<GameSetup> InitializeGameAsync(int computerId);
    }
}
