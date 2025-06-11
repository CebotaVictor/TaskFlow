using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Domain.Entities.Labels;
using TaskFlow.Domain.Entities.Projects;

namespace TaskFlow.Application.Interfaces.Repository
{
    public interface IProjectRepository
    {
        Task<Project> UpdateProjectSectionsAsync();
        Project AddSectionToProject(Project CurrentProject, Section section);

        Task<IEnumerable<Project>> GetAllProjectsAsync();
    }
}
