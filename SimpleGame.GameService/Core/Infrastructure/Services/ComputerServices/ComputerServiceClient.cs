using SimpleGame.GameService.Core.Application.Clients.Services;
using SimpleGame.GameService.Core.Domain.Models;

namespace SimpleGame.GameService.Core.Infrastructure.Services.ComputerServices
{
    public class ComputerServiceClient : IComputerServiceClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ComputerServiceClient> _logger;

        public ComputerServiceClient(HttpClient httpClient, ILogger<ComputerServiceClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<ComputerDetails> GetComputerDetailsAsync(int id)
        {
            _logger.LogInformation("Requesting computer details for ID {Id}", id);

            try
            {
                var response = await _httpClient.GetAsync("/api/Computer/choice");

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError("Failed to fetch computer details. Status code: {StatusCode}", response.StatusCode);
                    throw new HttpRequestException($"Failed to fetch computer details: {response.StatusCode}");
                }

                var computerDetails = await response.Content.ReadFromJsonAsync<ComputerDetails>();

                if (computerDetails == null)
                {
                    _logger.LogError("Failed to deserialize computer details.");
                    throw new HttpRequestException("Computer details response was null.");
                }

                _logger.LogInformation("Successfully fetched computer details for ID {Id}", id);

                return computerDetails;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error occurred while fetching computer details for ID {Id}", id);
                throw;
            }
        }
    }
}
