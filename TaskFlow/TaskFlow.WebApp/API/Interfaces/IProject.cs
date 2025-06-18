using TaskFlow.Application.WorkFlow.Projects.Command;
using TaskFlow.Domain.Entities.Labels;
using TaskFlow.Domain.Entities.Projects;

namespace TaskFlow.WebApp.API.Interfaces
{
    public interface IProject
    {
        Task<IEnumerable<Project>> GetAllProjectsAsync(CancellationToken token);
        Task<Project> GetProjectById(ushort Id,CancellationToken token);
    }
}
