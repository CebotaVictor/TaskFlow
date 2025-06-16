using System.Net;
using System.Text;
using System.Text.Json;
using TaskFlow.Application.Contracts.Authentication;
using TaskFlow.Application.Users.Members.Commands;
using TaskFlow.WebApp.API.Interfaces;

namespace TaskFlow.WebApp.API.Services
{
    public class AuthAPIService : IAuth
    {
        private HttpClient _httpClient;
        public AuthAPIService(HttpClient httpclient)
        {
            _httpClient = httpclient;
        }

        public async Task<object> Register(RegisterRequestApi request, CancellationToken token)
        {
            try
            {
                var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("register", content, token);

                if (response.StatusCode == HttpStatusCode.Conflict)
                {
                    Console.WriteLine($"Email {request.Email}: is being used {response.StatusCode}");
                    return null!;
                }

                if(!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Failed the registration: {response.StatusCode}");
                    return null!;
                }

                var responseBody = await response.Content.ReadAsStringAsync();

                return responseBody!;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred during registration: {ex.Message}");
                return null!;
            }
        
        }
        public async Task<string> Login(LoginRequest request, CancellationToken token)
        {
            try
            {
                var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("login", content, token);

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Failed the registration: {response.StatusCode}");
                    return null!;
                }

                var responseBody = await response.Content.ReadAsStringAsync();

                return responseBody!;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred during login: {ex.Message}");
                return null!;
            }
        }
    }
}
