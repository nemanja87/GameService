using Newtonsoft.Json;
using SimpleGame.GameService.Core.Application.Clients.Services;
using SimpleGame.GameService.Core.Application.Dtos;
using System.Text;

namespace SimpleGame.GameService.Core.Infrastructure.Services.ScoreboardServices
{
    public class ScoreboardServiceClient : IScoreboardServiceClient
    {
        private readonly HttpClient _httpClient;

        public ScoreboardServiceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task SaveGameResult(GameResultDto gameResultDto)
        {
            var jsonContent = JsonConvert.SerializeObject(gameResultDto);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/api/Scoreboard/add-result", content);

            response.EnsureSuccessStatusCode();
        }
    }
}
