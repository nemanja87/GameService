using SimpleGame.GameService.Core.Application.Clients.Services;
using SimpleGame.GameService.Core.Infrastructure.Services.ScoreboardServices;
using SimpleGame.GameService.Core.Application.Services;
using SimpleGame.GameService.Core.Domain.Interfaces;
using SimpleGame.GameService.Core.Infrastructure.Services.ComputerServices;

namespace SimpleGame.GameService.Core.Infrastructure.Extensions
{
    public static class HttpClientConfigurationExtensions
    {
        /// <summary>
        /// Configures HttpClients for external services (ComputerService, ScoreboardService, RandomNumberService).
        /// </summary>
        public static IServiceCollection ConfigureHttpClients(this IServiceCollection services, IConfiguration configuration)
        {
            // Register HttpClient for Random Number API
            services.AddHttpClient<IRandomNumberService, RandomNumberService>(client =>
            {
                var randomNumberApiUrl = configuration["RandomNumberApiUrl"];
                client.BaseAddress = new Uri(randomNumberApiUrl);
            });

            // Register HttpClient for ComputerService
            services.AddHttpClient<IComputerServiceClient, ComputerServiceClient>(client =>
            {
                var computerServiceUrl = configuration["ExternalServices:ComputerService:BaseUrl"];
                client.BaseAddress = new Uri(computerServiceUrl);
            });

            // Register HttpClient for ScoreboardService
            services.AddHttpClient<IScoreboardServiceClient, ScoreboardServiceClient>(client =>
            {
                var scoreboardServiceUrl = configuration["ExternalServices:ScoreboardService:BaseUrl"];
                client.BaseAddress = new Uri(scoreboardServiceUrl);
            });

            return services;
        }
    }
}