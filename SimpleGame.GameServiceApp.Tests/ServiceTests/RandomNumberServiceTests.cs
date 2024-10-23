using Microsoft.Extensions.Configuration;
using Moq;
using Moq.Protected;
using SimpleGame.GameServiceApp.Core.Application.Services;
using System.Net;

namespace SimpleGame.GameServiceApp.Tests.ServiceTests
{
    public class RandomNumberServiceTests
    {
        private readonly RandomNumberService _randomNumberService;
        private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;
        private readonly Mock<IConfiguration> _configurationMock;

        public RandomNumberServiceTests()
        {
            _httpMessageHandlerMock = new Mock<HttpMessageHandler>();

            // Mock IConfiguration
            _configurationMock = new Mock<IConfiguration>();
            _configurationMock.Setup(c => c["RandomNumberApiUrl"]).Returns("https://codechallenge.boohma.com/");

            var httpClient = new HttpClient(_httpMessageHandlerMock.Object)
            {
                BaseAddress = new System.Uri("https://codechallenge.boohma.com/")
            };

            _randomNumberService = new RandomNumberService(httpClient, _configurationMock.Object);
        }

        [Fact]
        public async Task GetRandomNumber_Should_Return_Valid_Number_From_Api()
        {
            // Arrange: Correctly mock the API response to match the RandomNumberResponseDto structure
            var validApiResponse = "{\"random_number\": 3}";  // This is the correct JSON format

            // Mock the HttpClient to return a valid JSON response
            _httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(validApiResponse)  // Pass the correct JSON response
                });

            // Act: Call the service to get the random number from the API
            var result = await _randomNumberService.GetRandomNumber();

            // Assert: Ensure the result is the number returned by the API (3 in this case)
            Assert.Equal(3, result);
        }

        [Fact]
        public async Task GetRandomNumber_Should_Fallback_To_LocalRandomNumber_When_Api_Fails()
        {
            // Arrange
            var httpResponse = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.InternalServerError
            };

            _httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(httpResponse);

            // Act
            var result = await _randomNumberService.GetRandomNumber();

            // Assert
            Assert.InRange(result, 1, 5);  // Fallback generates a random number between 1 and 5
        }
    }
}
