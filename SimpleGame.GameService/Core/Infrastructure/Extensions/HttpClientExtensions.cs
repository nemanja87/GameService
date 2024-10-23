using Polly;
using Polly.Extensions.Http;
using SimpleGame.GameService.Core.Application.Clients.Services;
using SimpleGame.GameService.Core.Infrastructure.Services.ComputerServices;
using SimpleGame.GameService.Core.Infrastructure.Services.ScoreboardServices;

namespace SimpleGame.GameService.Core.Infrastructure.Extensions
{
    /// <summary>
    /// Extension methods for configuring HttpClients with Polly.
    /// </summary>
    public static class HttpClientExtensions
    {
        /// <summary>
        /// Configures HttpClients with Polly for retry and fallback policies.
        /// </summary>
        /// <param name="services">The service collection to configure.</param>
        /// <param name="configuration">The application configuration containing service URLs.</param>
        /// <returns>The updated service collection.</returns>
        /// <exception cref="ArgumentNullException">Thrown if configuration values are null.</exception>
        public static IServiceCollection ConfigureHttpClientsWithPolly(this IServiceCollection services, IConfiguration configuration)
        {
            // Configure HttpClient for ComputerService with Polly
            services.AddHttpClient<IComputerServiceClient, ComputerServiceClient>(client =>
            {
                var computerServiceUrl = configuration["ExternalServices:ComputerService:BaseUrl"];
                if (string.IsNullOrWhiteSpace(computerServiceUrl))
                {
                    throw new ArgumentNullException(nameof(computerServiceUrl), "ComputerService BaseUrl cannot be null or empty.");
                }
                client.BaseAddress = new Uri(computerServiceUrl);
            })
            .AddPolicyHandler(GetRetryPolicy())
            .AddPolicyHandler(GetFallbackPolicy());

            // Configure HttpClient for ScoreboardService with Polly
            services.AddHttpClient<IScoreboardServiceClient, ScoreboardServiceClient>(client =>
            {
                var scoreboardServiceUrl = configuration["ExternalServices:ScoreboardService:BaseUrl"];
                if (string.IsNullOrWhiteSpace(scoreboardServiceUrl))
                {
                    throw new ArgumentNullException(nameof(scoreboardServiceUrl), "ScoreboardService BaseUrl cannot be null or empty.");
                }
                client.BaseAddress = new Uri(scoreboardServiceUrl);
            })
            .AddPolicyHandler(GetRetryPolicy())
            .AddPolicyHandler(GetFallbackPolicy());

            return services;
        }

        // Define a retry policy with Polly
        private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        }

        // Define a fallback policy with Polly
        private static IAsyncPolicy<HttpResponseMessage> GetFallbackPolicy()
        {
            return Policy<HttpResponseMessage>
                .Handle<HttpRequestException>()
                .FallbackAsync(new HttpResponseMessage(System.Net.HttpStatusCode.OK)
                {
                    Content = new StringContent("Fallback executed.")
                });
        }
    }
}
