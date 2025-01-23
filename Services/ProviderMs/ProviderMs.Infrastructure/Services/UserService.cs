
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using ProviderMs.Common.Exceptions;
using ProviderMs.Core.Services;
using ProviderMs.Domain.ValueObjects;

namespace ProviderMs.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _httpClientUrl;
        public UserService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor, IOptions<HttpClientUrl> httpClientUrl)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
            _httpClientUrl = httpClientUrl.Value.ApiUrl;

            //* Configuracion del HttpClient
            var headerToken = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].ToString()?.Replace("Bearer ", "");
            _httpClient.BaseAddress = new Uri(_httpClientUrl);
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {headerToken}");
        }

        public async Task<bool> DriverExists(TowDriver driver)
        {
            try
            {
                var response = await _httpClient.GetAsync($"user/driver/{driver.Value}");
                if (!response.IsSuccessStatusCode)
                {
                    throw new NotFoundException("Driver not found");
                }
                return true;
            }
            catch
            {
                throw;
            }
        }
    }
}