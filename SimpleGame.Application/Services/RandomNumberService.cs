using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SimpleGame.Application.Dtos;
using SimpleGame.Domain.Interfaces;

namespace SimpleGame.Application.Services
{
    public class RandomNumberService(HttpClient httpClient, IConfiguration configuration) : IRandomNumberService
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly IConfiguration _configuration = configuration;

        public async Task<int> GetRandomNumber()
        {
            var randomApiUrl = _configuration["RandomNumberApiUrl"]; // Read URL from config
            var response = await _httpClient.GetStringAsync(randomApiUrl);

            // Assuming the API response is: { "random_number": integer }
            var randomNumberResponse = JsonConvert.DeserializeObject<RandomNumberResponseDto>(response);

            return randomNumberResponse == null
                ? throw new Exception("Failed to deserialize the random number response.")
                : randomNumberResponse.RandomNumber;
        }
    }
}
