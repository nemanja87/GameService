using SimpleGame.ComputerService.Core.Domain.Enum;
using SimpleGame.ComputerService.Core.Domain.Exceptions;
using SimpleGame.ComputerService.Core.Domain.Interfaces;

namespace SimpleGame.ComputerService.Core.Application.Services
{
    public class ComputerChoiceService : IComputerChoiceService
    {
        private readonly ILogger<ComputerChoiceService> _logger;

        public ComputerChoiceService(ILogger<ComputerChoiceService> logger)
        {
            _logger = logger;
        }

        public Task<ComputerChoiceEnum> GetRandomComputerChoiceAsync()
        {
            try
            {
                var random = new Random();
                var randomChoice = (ComputerChoiceEnum)random.Next(1, 6);

                _logger.LogInformation("Random computer choice generated: {Choice}", randomChoice);

                return Task.FromResult(randomChoice);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to generate random computer choice.");
                throw new ComputerChoiceException("An error occurred while generating a computer choice.", ex);
            }
        }
    }
}
