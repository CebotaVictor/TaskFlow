using System.Net.Http;
using TaskFlow.Application.WorkFlow.Projects.Command;
using TaskFlow.Domain.Entities.Projects;
using TaskFlow.WebApp.API.Interfaces;
using UtmHttp.Utility;

namespace TaskFlow.WebApp.API.Services
{
    public class ProjectAPIService : IProject
    {
        private HttpClient _httpClient;

        public ProjectAPIService(HttpClient httpclient)
        {
            _httpClient = httpclient;
        }

        public Task<object> CreateProjectAsync(CreateProjectCommand request, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Project>> GetAllProjectsAsync(CancellationToken token)
        {
            try
            {
                var response = await _httpClient.GetAsync("GetAllProjects", token);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<IEnumerable<Project>>(token);
                }
                return null!;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to retrieve projects: {ex.Message}");
            }
        }
    }
}
