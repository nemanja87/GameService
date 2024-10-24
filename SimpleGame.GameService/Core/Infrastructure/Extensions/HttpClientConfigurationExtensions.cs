﻿using SimpleGame.GameService.Core.Application.Clients.Services;
using SimpleGame.GameService.Core.Infrastructure.Services.ComputerServices;
using SimpleGame.GameService.Core.Infrastructure.Services.ScoreboardServices;

namespace SimpleGame.GameService.Core.Infrastructure.Extensions
{
    /// <summary>
    /// Extension methods for configuring HttpClients.
    /// </summary>
    public static class HttpClientConfigurationExtensions
    {
        /// <summary>
        /// Configures HttpClients for external services (ComputerService, ScoreboardService, RandomNumberService).
        /// </summary>
        /// <param name="services">The service collection to configure.</param>
        /// <param name="configuration">The application configuration containing service URLs.</param>
        /// <returns>The updated service collection.</returns>
        /// <exception cref="ArgumentNullException">Thrown if configuration values are null.</exception>
        public static IServiceCollection ConfigureHttpClients(this IServiceCollection services, IConfiguration configuration)
        {
            // Register HttpClient for ComputerService
            services.AddHttpClient<IComputerServiceClient, ComputerServiceClient>(client =>
            {
                var computerServiceUrl = configuration["ExternalServices:ComputerService:BaseUrl"];
                if (string.IsNullOrWhiteSpace(computerServiceUrl))
                {
                    throw new ArgumentNullException(nameof(computerServiceUrl), "ComputerService BaseUrl cannot be null or empty.");
                }
                client.BaseAddress = new Uri(computerServiceUrl);
            });

            // Register HttpClient for ScoreboardService
            services.AddHttpClient<IScoreboardServiceClient, ScoreboardServiceClient>(client =>
            {
                var scoreboardServiceUrl = configuration["ExternalServices:ScoreboardService:BaseUrl"];
                if (string.IsNullOrWhiteSpace(scoreboardServiceUrl))
                {
                    throw new ArgumentNullException(nameof(scoreboardServiceUrl), "ScoreboardService BaseUrl cannot be null or empty.");
                }
                client.BaseAddress = new Uri(scoreboardServiceUrl);
            });

            return services;
        }
    }
}