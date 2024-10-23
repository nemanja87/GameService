using SimpleGame.ComputerService.Core.Domain.Interfaces;
using SimpleGame.ComputerService.Core.Domain.Enum;

namespace SimpleGame.ComputerService.Core.Application.Services
{
    public class ComputerChoiceService : IComputerChoiceService
    {
        public ComputerChoiceService()
        {
        }

        public Task<ComputerChoiceEnum> GetRandomComputerChoiceAsync()
        {
            var random = new Random();
            var randomChoice = (ComputerChoiceEnum)random.Next(1, 6);  // Generates a random number between 1 and 5
            return Task.FromResult(randomChoice);
        }
    }
}
