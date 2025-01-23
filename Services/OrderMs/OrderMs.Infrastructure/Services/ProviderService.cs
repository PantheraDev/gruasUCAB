using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.Extensions.Options;
using OrderMs.Common.Dto.Response;
using OrderMs.Common.Dtos.Provider;
using OrderMs.Core.Services;
using OrderMs.Domain.ValueObjects;

namespace OrderMs.Infrastructure.Services
{
    public class ProviderService : IProviderService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _httpClientUrl;

        public ProviderService(IHttpContextAccessor httpContextAccessor, HttpClient httpClient, IOptions<HttpClientUrl> httpClientUrl)
        {
            _httpContextAccessor = httpContextAccessor;
            _httpClient = httpClient;
            _httpClientUrl = httpClientUrl.Value.ApiUrl;
            //* Configuracion del HttpClient
            var headerToken = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].ToString()?.Replace("Bearer ", "");
            _httpClient.BaseAddress = new Uri(_httpClientUrl);
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {headerToken}");
        }

        public async Task<List<GetTow?>> GetTowsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("provider/tow");
                response.EnsureSuccessStatusCode();

                await using var responseStream = await response.Content.ReadAsStreamAsync();
                List<GetTow> tows = await JsonSerializer.DeserializeAsync<List<GetTow>>(responseStream) ?? new List<GetTow>();

                return tows!;
            }
            catch
            {
                throw;
            }

        }
        public async Task<GetTow?> GetTowByIdAsync(TowId towId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"provider/tow/{towId.Value}");
                response.EnsureSuccessStatusCode();

                await using var responseStream = await response.Content.ReadAsStreamAsync();
                GetTow tow = await JsonSerializer.DeserializeAsync<GetTow>(responseStream) ?? new GetTow();

                return tow!;
            }
            catch
            {
                throw;
            }

        }

        public async Task<List<GetProvider?>> GetProviderAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("provider");
                response.EnsureSuccessStatusCode();

                await using var responseStream = await response.Content.ReadAsStreamAsync();
                List<GetProvider> providers = await JsonSerializer.DeserializeAsync<List<GetProvider>>(responseStream) ?? new List<GetProvider>();

                return providers!;
            }
            catch
            {
                throw;
            }

        }
    }
}
