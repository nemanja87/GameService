using SimpleGame.GameServiceApp.Core.Domain.Models;

namespace SimpleGame.GameServiceApp.Core.Domain.Interfaces
{
    public interface IGameSetupService
    {
        Task<GameSetup> InitializeGameAsync(int computerId);
    }
}
