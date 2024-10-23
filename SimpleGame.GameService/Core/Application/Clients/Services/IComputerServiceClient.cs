using SimpleGame.GameServiceApp.Core.Domain.Models;

namespace SimpleGame.GameServiceApp.Core.Application.Clients.Services
{
    public interface IComputerServiceClient
    {
        Task<ComputerDetails> GetComputerDetailsAsync(int id);
    }
}
