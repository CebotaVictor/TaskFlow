using System.Net.Http;
using TaskFlow.Application.WorkFlow.Projects.Command;
using TaskFlow.Domain.Entities.Labels;
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

        public async Task<Project> GetProjectById(ushort Id, CancellationToken token)
        {
            try
            {
                var response = await _httpClient.GetAsync($"GetProjectById?Id={Id}", token);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<Project>(token);
                }
                return null!;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to retrieve project: {ex.Message}");
            }
        }

        public async Task<Project> GetProjectByIdAsync(ushort Id, CancellationToken token)
        {
            try
            {
                var response = await _httpClient.GetAsync($"GetProjectById?Id={Id}", token);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<Project>(token);
                }
                return null!;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to retrieve project: {ex.Message}");
            }
        }
    }
}
