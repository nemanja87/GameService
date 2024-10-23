﻿using SimpleGame.GameServiceApp.Core.Application.Clients.Services;
using SimpleGame.GameServiceApp.Core.Domain.Models;

namespace SimpleGame.GameServiceApp.Core.Infrastructure.Services.ComputerServices
{
    public class ComputerServiceClient : IComputerServiceClient
    {
        private readonly HttpClient _httpClient;

        public ComputerServiceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ComputerDetails> GetComputerDetailsAsync(int id)
        {
            var response = await _httpClient.GetAsync("/api/Computer/choice");
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Failed to fetch computer details: {response.StatusCode}");
            }

            return await response.Content.ReadFromJsonAsync<ComputerDetails>();
        }
    }
}
