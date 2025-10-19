using System.Net.Http;
using TaskFlow.Application.WorkFlow.Projects.Command;
using TaskFlow.Domain.Entities.WSections;
using TaskFlow.Domain.Entities.Projects;
using TaskFlow.WebApp.API.Interfaces;
using UtmHttp.Utility;
using TaskFlow.Application.Contracts.Workflow;
using TaskFlow.Application.Mapping;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;


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

                if (response.Content != null)
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return await response.Content
                        /*The Empty<TResult>() method caches an empty sequence of type TResult. When the object it returns is enumerated, it yields no elements*/
                        .ReadFromJsonAsync<IEnumerable<Project>>(token) ?? Enumerable.Empty<Project>();
                    }
                }

                return null!;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to retrieve projects: {ex.Message}");
            }
        }

        public async Task<Project> GetProjectByIdAsync(ushort Id, CancellationToken token)
        {
            try
            {
                var response = await _httpClient.GetAsync($"GetProjectById?Id={Id}", token);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<Project>(token) ?? new Project();
                }
                return null!;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to retrieve project: {ex.Message}");
            }
        }

        public async Task<IEnumerable<Section>> GetAllSectionsByProjectIdAsync(ushort Id, CancellationToken token)
        {
            try
            {
                var response = await _httpClient.GetAsync($"GetAllSectionsFromProjectId?Id={Id}", token);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<IEnumerable<Section>>(token) ?? Enumerable.Empty<Section>().ToList();
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
