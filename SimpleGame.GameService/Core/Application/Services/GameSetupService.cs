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
            _logger.LogInformation("Initializing game for computer ID {ComputerId}", computerId);

            try
            {
                // Fetch computer details from the ComputerService
                var computerDetails = await _computerServiceClient.GetComputerDetailsAsync(computerId);

                if (computerDetails == null)
                {
                    _logger.LogWarning("No computer details found for computer ID {ComputerId}", computerId);
                    throw new Exception($"No computer details found for ID: {computerId}");
                }

                _logger.LogInformation("Successfully fetched computer details for computer ID {ComputerId}", computerId);

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

                _logger.LogInformation("Game setup initialized for computer ID {ComputerId} with GameMode: {GameMode}", computerId, gameSetup.GameMode);

                return gameSetup;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Failed to communicate with ComputerService for computer ID {ComputerId}", computerId);
                throw new Exception("Failed to retrieve computer details.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while initializing game for computer ID {ComputerId}", computerId);
                throw;
            }
        }
    }
}
