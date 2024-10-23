using SimpleGame.GameService.Core.Domain.Models;

namespace SimpleGame.GameService.Core.Application.Clients.Services
{
    public interface IComputerServiceClient
    {
        Task<ComputerDetails> GetComputerDetailsAsync(int id);
    }
}
