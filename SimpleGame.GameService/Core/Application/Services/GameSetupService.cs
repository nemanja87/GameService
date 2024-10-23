using SimpleGame.GameService.Core.Application.Clients.Services;
using SimpleGame.GameService.Core.Domain.Interfaces;
using SimpleGame.GameService.Core.Domain.Models;

namespace SimpleGame.GameService.Core.Application.Services
{
    public class GameSetupService : IGameSetupService
    {
        private readonly IComputerServiceClient _computerServiceClient;
        private readonly ILogger<GameSetupService> _logger;

        public GameSetupService(IComputerServiceClient computerServiceClient, ILogger<GameSetupService> logger)
        {
            _computerServiceClient = computerServiceClient;
            _logger = logger;
        }

        public async Task<GameSetup> InitializeGameAsync(int computerId)
        {
            try
            {
                var computerDetails = await _computerServiceClient.GetComputerDetailsAsync(computerId);

                var gameSetup = new GameSetup
                {
                    ComputerName = computerDetails.Name,
                    Processor = computerDetails.Processor,
                    RamSize = computerDetails.RamSize,
                    GraphicsCard = computerDetails.GraphicsCard,
                    ManufactureDate = computerDetails.ManufactureDate,
                    GameMode = "Adventure",
                    InitialLevel = 1,
                    Difficulty = "Normal"
                };

                return gameSetup;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error initializing game with computer ID {ComputerId}", computerId);
                throw;
            }
        }
    }
}
