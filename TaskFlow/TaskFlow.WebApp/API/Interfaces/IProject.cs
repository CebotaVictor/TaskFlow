using TaskFlow.Application.WorkFlow.Projects.Command;
using TaskFlow.Domain.Entities.WSections;
using TaskFlow.Domain.Entities.Projects;

namespace TaskFlow.WebApp.API.Interfaces
{
    public interface IProject
    {
        Task<IEnumerable<Project>> GetAllProjectsAsync(CancellationToken token);
        Task<Project> GetProjectByIdAsync(ushort Id,CancellationToken token);
        Task<IEnumerable<Section>> GetAllSectionsByProjectIdAsync(ushort Id, CancellationToken token);
    }
}
