
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualBasic;
using UserMs.Core.Interface;
using UserMs.Domain.Entities;
using UserMs.Infrastructure.Exceptions;

namespace UserMs.Infrastructure
{
    public class AuthMsService : IAuthMsService
    {
        public readonly HttpClient _httpClient;
        public readonly IHttpContextAccessor _httpContextAccessor;
        public AuthMsService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;

            var headerToken = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].ToString()?.Replace("Bearer ", "");
            _httpClient.BaseAddress = new Uri("http://localhost:18084/");
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {headerToken}");

        }

        public async Task AssignClientRoleToUser(Guid id, string roleName)
        {
            try
            {
                var role = String.Empty;
                if (roleName == "Conductor")
                {
                    role = "Driver";
                }
                else if (roleName == "Administrador")
                {
                    role = "Administrator";
                }
                else if (roleName == "Proveedor")
                {
                    role = "Provider";
                }
                else
                {
                    role = "Operator";
                }
                string ClientId = (roleName == "Conductor") ? "mobileclient" : "webclient";
                var userData = new
                {
                    userId = id,
                    roleName = role,
                    clientId = ClientId
                };
                Console.WriteLine("UserData: " + userData);
                var jsonBody = JsonSerializer.Serialize(userData);
                var contentJson = new StringContent(jsonBody, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"auth/assingRole", contentJson);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new AuthenticationMsException("Error on "
                            + _httpClient.BaseAddress
                            + "assing-role-user"
                            + "Status Code: "
                            + response.StatusCode
                            + "Content : "
                            + response.Content);
                }
                var content = await response.Content.ReadAsStringAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<string> CreateUserAsync(string userEmail, string userPassword)
        {
            try
            {
                var userData = new
                {
                    //id = user.UserId.Value,
                    username = userEmail,
                    email = userEmail,
                    password = userPassword
                };
                // Serializar el objeto a JSON
                var jsonBody = JsonSerializer.Serialize(userData);
                var contentJson = new StringContent(jsonBody, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("auth/createUser", contentJson);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new AuthenticationMsException("Error on "
                            + _httpClient.BaseAddress
                            + "create-user "
                            + "Status Code: "
                            + response.StatusCode
                            + "Content : "
                            + response.Content);
                }
                var content = await response.Content.ReadAsStringAsync();
                return content;
            }
            catch
            {
                throw;
            }
        }

        public async Task<string> DeleteUserAsync(UserId userId)
        {
            try
            {
                Console.WriteLine("UserId: " + userId.Value);
                var response = await _httpClient.DeleteAsync($"auth/deleteUser/{userId.Value}");

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new AuthenticationMsException("Error on "
                            + _httpClient.BaseAddress
                            + "delete-user "
                            + "Status Code: "
                            + response.StatusCode
                            + "Content : "
                            + response.Content);
                }
                var content = await response.Content.ReadAsStringAsync();
                return content;
            }
            catch
            {
                throw;
            }
        }

        public async Task<Guid> GetUserByUserName(UserEmail userName)
        {
            try
            {
                var response = await _httpClient.GetAsync($"auth/{userName.Value}");
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new AuthenticationMsException("Error on "
                            + _httpClient.BaseAddress
                            + "get-user-by-username"
                            + "Status Code: "
                            + response.StatusCode
                            + "Content : "
                            + response.Content);
                }

                var responseString = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine("ResponseString: " + responseString);
                return new Guid(responseString.Replace("\"", ""));
            }
            catch
            {
                throw;
            }
        }

        public async Task UpdateUser(UserId id, Base user)
        {
            try
            {
                var userData = new
                {
                    email = user.UserEmail.Value
                };


                var jsonBody = JsonSerializer.Serialize(userData);
                var contentJson = new StringContent(jsonBody, Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"auth/{id.Value}", contentJson);
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new AuthenticationMsException("Error on "
                            + _httpClient.BaseAddress
                            + "update-user"
                            + "Status Code: "
                            + response.StatusCode
                            + "Content : "
                            + response.Content);
                }
                var content = await response.Content.ReadAsStringAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}