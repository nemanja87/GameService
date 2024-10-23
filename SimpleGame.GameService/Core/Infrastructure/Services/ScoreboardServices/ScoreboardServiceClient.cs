using Newtonsoft.Json;
using SimpleGame.GameService.Core.Application.Clients.Services;
using SimpleGame.GameService.Core.Application.Dtos;
using System.Text;

namespace SimpleGame.GameService.Core.Infrastructure.Services.ScoreboardServices
{
    public class ScoreboardServiceClient : IScoreboardServiceClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ScoreboardServiceClient> _logger;

        public ScoreboardServiceClient(HttpClient httpClient, ILogger<ScoreboardServiceClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task SaveGameResult(GameResultDto gameResultDto)
        {
            _logger.LogInformation("Sending game result to Scoreboard Service. PlayerChoice: {PlayerChoice}, Result: {Result}", gameResultDto.PlayerChoice, gameResultDto.Result);

            try
            {
                var jsonContent = JsonConvert.SerializeObject(gameResultDto);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("/api/Scoreboard/add-result", content);

                response.EnsureSuccessStatusCode();

                _logger.LogInformation("Successfully saved game result to the Scoreboard Service.");
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Failed to save game result to the Scoreboard Service.");
                throw;
            }
        }
    }
}
