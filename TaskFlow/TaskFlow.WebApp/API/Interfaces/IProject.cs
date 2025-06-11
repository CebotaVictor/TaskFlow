
using TaskFlow.Application.WorkFlow.Projects.Command;
using TaskFlow.Domain.Entities.Projects;

namespace TaskFlow.WebApp.API.Interfaces
{
    public interface IProject
    {
        Task<object> CreateProjectAsync(CreateProjectCommand request, CancellationToken token);

        Task<IEnumerable<Project>> GetAllProjectsAsync(CancellationToken token);
    }
}
