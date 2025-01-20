using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ProviderMs.Common.Exceptions;
using ProviderMs.Core.Services;
using ProviderMs.Domain.ValueObjects;

namespace ProviderMs.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
            //_httpClientUrl = httpClientUrl.Value.ApiUrl;

            //* Configuracion del HttpClient
            var headerToken = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].ToString()?.Replace("Bearer ", "");
            _httpClient.BaseAddress = new Uri("http://localhost:18081/");
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