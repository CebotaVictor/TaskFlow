using System.Text.Json;
using TaskFlow.Domain.Entities.Users;
using TaskFlow.WebApp.API.Interfaces;

namespace TaskFlow.WebApp.API.Services
{
    public class UserApiService : IUser
    {
        private HttpClient _httpClient;

        public UserApiService(HttpClient httpclient)
        {
            _httpClient = httpclient;
        }
        public async Task<Admin> GetAdminById(ushort Id, CancellationToken token)
        {
            var response = await _httpClient.GetAsync($"Admin/{Id}", token);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Admin with id {Id} not fund.");
                return null!;
            }

            var result = await response.Content.ReadFromJsonAsync<Admin>(token);
            if (result == null) throw new Exception($"Admin with id {Id} not found");
            return result;
        }

        public async Task<Member> GetMemberById(ushort Id, CancellationToken token)
        {
            var response = await _httpClient.GetAsync($"Member/{Id}", token);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Member with id {Id} not fund.");
                return null!;
            }

            var result = await response.Content.ReadFromJsonAsync<Member>(token);
            if (result == null) throw new Exception($"Member with id {Id} not found");
            return result;
        }
    }
}
