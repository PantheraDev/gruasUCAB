
using System.Net;
using System.Text;
using System.Text.Json;
using AuthenticationMs.Common.Dtos;
using AuthenticationMs.Common.Exceptions;
using AuthenticationMs.Core;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;

namespace AuthenticationMs.Infrastructure
{
    public class KeycloakRepository : IKeycloakRepository
    {
        private readonly HttpClient _httpClient;
        private readonly HttpClient _userMsClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly EmailSender _emailSender;


        public KeycloakRepository(HttpClient httpClient, IHttpContextAccessor httpContextAccessor, HttpClient userMsClient)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;

            //* Configuracion del HttpClient
            var headerToken = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].ToString()?.Replace("Bearer ", "");
            _httpClient.BaseAddress = new Uri("http://localhost:18080/");
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {headerToken}");

            _emailSender = new EmailSender();

        }
        //* Me retorna el Authorization TOKEN
        /*public async Task<string> GetTokenAsync()
        {
            var response = await _httpClient.PostAsync("http://localhost:18080/realms/auth/protocol/openid-connect/token",
            new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "grant_type", "client_credentials" },
                { "client_id", "web-client" },
                { "client_secret", "REdOcKznwuvtZ54jVVt9ebc3nCz6uqMy" }
            }));

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return content;

        }*/

        /* public async Task<string> LoginAsync(string username, string password)
         {
             var response = await _httpClient.PostAsync("http://localhost:18080/realms/auth/protocol/openid-connect/token",
             new FormUrlEncodedContent(new Dictionary<string, string>
             {
                 { "grant_type", "password" },
                 { "client_id", "web-client" },
                 { "username", username },
                 {"password", password},
                 {"client_secret", "REdOcKznwuvtZ54jVVt9ebc3nCz6uqMy" }
             }));

             response.EnsureSuccessStatusCode();

             var content = await response.Content.ReadAsStringAsync();
             //Parse the token from the response
             return content;
         }*/

        /*public async Task<string> LogOutAsync(string refreshToken, string authToken)
        {
            //* MANEJA EL TOKEN DEL HEADER
            var headerToken = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].ToString()?.Replace("Bearer ", "");
            var accessToken = new JwtSecurityTokenHandler().ReadToken(headerToken) as JwtSecurityToken;
            var userId = accessToken!.Payload["sub"];

            _httpClient.BaseAddress = new Uri("http://localhost:18080/");
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {authToken}");

            var response = await _httpClient.PostAsync($"admin/realms/auth/users/{userId}/logout",
            new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"refresh_token", refreshToken },
                { "client_id", "web-client" },
                {"client_secret", "REdOcKznwuvtZ54jVVt9ebc3nCz6uqMy" }
             }));

            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return content;

        }*/



        public async Task<string> CreateUserAsync(CreateUserDto user)
        {
            try
            {
                //var randomPassword = RandomPassword();
                var userData = new
                {
                    username = user.Username,
                    email = user.Email,
                    emailVerified = true,
                    enabled = true,
                    credentials = new[]
                        {
                        new
                        {
                            type = "password",
                            value = $"{user.Password}",
                            temporary = true,
                        },
                    },
                };
                // Serializar el objeto a JSON
                var jsonBody = JsonSerializer.Serialize(userData);
                var contentJson = new StringContent(jsonBody, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("admin/realms/auth/users", contentJson);

                if (!response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == HttpStatusCode.Conflict)
                    {
                        throw new UserExistException("User already exists, try with another username or email");
                    }
                    else
                    {
                        throw new KeycloakException("Error on "
                                + _httpClient.BaseAddress
                                + "create-user "
                                + "Status Code: "
                                + response.StatusCode
                                + "Content : "
                                + response.Content);
                    }
                }

                //TODO: Si falla el envio del correo, eliminar el usuario creado
                // Enviar un correo con la contraseña temporal
                await _emailSender.SendEmailAsync(user.Email, "Credenciales de Acceso",
                    $"Hola {user.Username},\n\nTu contraseña temporal es: {user.Password}\n\nPor favor, cámbiala al iniciar sesión.");

                var content = await response.Content.ReadAsStringAsync();
                return content;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> DeleteUserAsync(Guid userId)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"admin/realms/auth/users/{userId}");

                if (!response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        throw new UserNotFoundException("User not found");
                    }
                    throw new KeycloakException("Error on "
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
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> GetClientRolesId(string clientId, string userId, string roleName)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/admin/realms/auth/users/{userId}/role-mappings/clients/{clientId}/available");

                if (!response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        throw new UserNotFoundException("User not found");
                    }
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new KeycloakException("Error on "
                            + _httpClient.BaseAddress
                            + "create-user"
                            + "Status Code: "
                            + response.StatusCode
                            + "Content : "
                            + response.Content);
                }
                var content = await response.Content.ReadAsStringAsync();
                var json = JArray.Parse(content);
                foreach (var item in json)
                {
                    if (item["name"]!.ToString() == roleName)
                    {
                        return item["id"]!.ToString();
                    }
                }
                return "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task AssignClientRoleToUser(Guid userId, string clientId, string roleName)
        {
            try
            {
                //* La Client Id es un guid
                var ClientId = "f49adac8-b4f4-4791-9266-9d097c5875ff";

                var rolId = this.GetClientRolesId(ClientId, userId.ToString(), roleName).Result;
                var roles = new[] { new { id = rolId, name = roleName } };
                var userData = new { };

                // Serializar el objeto a JSON
                var jsonBody = JsonSerializer.Serialize(roles);
                // Crear el contenido de la solicitud con el encabezado Content-Type
                var contentJson = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"admin/realms/auth/users/{userId}/role-mappings/clients/{ClientId}", contentJson);

                if (!response.IsSuccessStatusCode)
                {

                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new KeycloakException("Error on "
                            + _httpClient.BaseAddress
                            + " assing-role-user"
                            + " Status Code: "
                            + response.StatusCode
                            + " Content : "
                            + response.Content);
                }
                var content = await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task AssignClientRoleToUserMobile(Guid userId, string clientId, string roleName)
        {
            try
            {
                //* La Client Id es un guid
                var mobileId = "3a54e43c-8bd4-401e-bc6f-62ea392c80e6";

                var rolId = this.GetClientRolesId(mobileId, userId.ToString(), roleName).Result;
                var roles = new[] { new { id = rolId, name = roleName } };
                var userData = new { };

                // Serializar el objeto a JSON
                var jsonBody = JsonSerializer.Serialize(roles);
                // Crear el contenido de la solicitud con el encabezado Content-Type
                var contentJson = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"admin/realms/auth/users/{userId}/role-mappings/clients/{mobileId}", contentJson);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new KeycloakException("Error on "
                            + _httpClient.BaseAddress
                            + "assing-role-user"
                            + "Status Code: "
                            + response.StatusCode
                            + "Content : "
                            + response.Content);
                }
                var content = await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Guid> GetUserByUserName(string userName)
        {
            try
            {
                var response = await _httpClient.GetAsync($"admin/realms/auth/users?username={userName}");
                if (!response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        throw new UserNotFoundException("User not found");
                    }
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new KeycloakException("Error on "
                            + _httpClient.BaseAddress
                            + "get-user-by-username"
                            + "Status Code: "
                            + response.StatusCode
                            + "Content : "
                            + response.Content);
                }

                var responseString = response.Content.ReadAsStringAsync().Result;
                var user = JArray.Parse(responseString);

                return user.Count > 0 ? new Guid(user[0]["id"]!.ToString()) : Guid.Empty;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task UpdateUser(Guid id, UpdateUserDto user)
        {
            try
            {
                var userData = new
                {
                    username = user.Email,
                    email = user.Email,
                    enabled = true
                };

                // Serializar el objeto a JSON
                var jsonBody = JsonSerializer.Serialize(userData);
                var contentJson = new StringContent(jsonBody, Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"admin/realms/auth/users/{id}", contentJson);
                if (!response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        throw new UserNotFoundException("User not found");
                    }
                    else if (response.StatusCode == HttpStatusCode.Conflict)
                    {
                        throw new UserExistException("User already exists, try with another username or email");
                    }
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new KeycloakException("Error on "
                            + _httpClient.BaseAddress
                            + "update-user"
                            + "Status Code: "
                            + response.StatusCode
                            + "Content : "
                            + response.Content);
                }
                var content = await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
