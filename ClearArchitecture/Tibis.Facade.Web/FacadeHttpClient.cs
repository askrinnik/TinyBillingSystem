﻿using Tibis.Application.HttpClients;

namespace Tibis.Facade.Web
{
    public interface IFacadeHttpClient
    {
        Task CreateDemoDataAsync();
    }

    public class FacadeHttpClient : IFacadeHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public FacadeHttpClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task CreateDemoDataAsync()
        {   
            var accountManagementUrl = _configuration["AccountManagementUrl"]?? "http://localhost:5001";
            var accClient = new AccountManagementHttpClient(accountManagementUrl, _httpClient);

            var account = await accClient.AccountPOSTAsync(new() { Name = $"User{Guid.NewGuid()}" });
        }
    }
}