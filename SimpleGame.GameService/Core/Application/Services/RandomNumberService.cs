using Newtonsoft.Json;
using SimpleGame.GameServiceApp.Core.Application.Dtos;
using SimpleGame.GameServiceApp.Core.Domain.Interfaces;

namespace SimpleGame.GameServiceApp.Core.Application.Services
{
    public class RandomNumberService : IRandomNumberService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public RandomNumberService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<int> GetRandomNumber()
        {
            var randomApiUrl = _configuration["RandomNumberApiUrl"]; // Read URL from config

            try
            {
                // Attempt to get the random number from the API
                var response = await _httpClient.GetStringAsync(randomApiUrl);

                // Deserialize the API response to get the random number
                var randomNumberResponse = JsonConvert.DeserializeObject<RandomNumberResponseDto>(response);

                // If deserialization fails, throw an exception
                if (randomNumberResponse == null)
                {
                    throw new Exception("Failed to deserialize the random number response.");
                }

                return randomNumberResponse.RandomNumber;
            }
            catch (HttpRequestException ex)
            {
                // Handle the API call failure by logging the error or printing it
                Console.WriteLine($"API request failed: {ex.Message}. Falling back to local random number.");

                // Fallback to generating a random number locally
                return new Random().Next(1, 6); // Return a random number between 1 and 5
            }
        }
    }
}
