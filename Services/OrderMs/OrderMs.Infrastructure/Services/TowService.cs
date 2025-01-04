using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderMs.Core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Text.Json;
using OrderMs.Domain.Entities;
using OrderMs.Common.Dto.Response;
using OrderMs.Domain.ValueObjects;

namespace OrderMs.Infrastructure
{
    public class TowService : ITowRepository
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        //private readonly string _httpClientUrl;

        public TowService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor, IOptions<HttpClientUrl> httpClientUrl)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
            //_httpClientUrl = httpClientUrl.Value.ApiUrl;
            
            var headerToken = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].ToString()?.Replace("Bearer","");
            _httpClient.BaseAddress = new Uri("http://localhost:18083/");
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {headerToken}");

    }

    public async Task<GetTow?> GetTowByIdAsync(TowId towId)
    {
        var response = await _httpClient.GetAsync($"provider/tow/{towId.Value}");
        response.EnsureSuccessStatusCode();

        await using var responseStream = await response.Content.ReadAsStreamAsync();
        GetTow tow = await JsonSerializer.DeserializeAsync<GetTow>(responseStream) ?? new GetTow();

        return tow!;
    }
}
}