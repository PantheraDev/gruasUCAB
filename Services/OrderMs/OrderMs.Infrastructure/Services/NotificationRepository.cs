using OrderMs.Core.Repositories;
using Microsoft.AspNetCore.Http;
using System.Text.Json;                                             //AQUI ESTA LO QUE MODIFIQUEEE KINCE SHAMMPIONS
using System.Text;
using Microsoft.Extensions.Options;
using OrderMs.Common.Dtos.Provider;

namespace OrderMs.Infrastructure.Repositories
{
    public class NotificationService : INotificationRepository
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        //private readonly string _httpClientUrl;

        public NotificationService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor, IOptions<HttpClientUrl> httpClientUrl)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
           // _httpClientUrl = httpClientUrl.Value.ApiUrl;
            
            var headerToken = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].ToString()?.Replace("Bearer","");
            _httpClient.BaseAddress = new Uri("http://localhost:18085/");
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {headerToken}");

    }

    public async  Task SendNotificationAsync (Guid id){
        var notification = new SendNotificationdto {id = id};
        var content = new StringContent(JsonSerializer.Serialize(id), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync($"job/firebase/", content);
        var responseBody = await response.Content.ReadAsStringAsync();
        response.EnsureSuccessStatusCode();
    }
    }
}