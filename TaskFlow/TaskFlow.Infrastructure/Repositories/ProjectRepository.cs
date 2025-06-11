using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Application.Interfaces.Repository;
using TaskFlow.Domain.Entities.Labels;
using TaskFlow.Domain.Entities.Projects;
using TaskFlow.Domain.Entities.Users;
using TaskFlow.Infrastructure.BL;

namespace TaskFlow.Infrastructure.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly WorkflowGenericRepository<Project>? _repository;
        private readonly DbSet<Project>? _context;
        private readonly ILogger<ProjectRepository> _logger;

        public ProjectRepository(WorkflowGenericRepository<Project>? repository, WorkflowDBContext? context, ILogger<ProjectRepository> logger)
        {
            _repository = repository;
            _context = context!.Set<Project>();
            _logger = logger;
        }

        public Project AddSectionToProject(Project CurrentProject, Section section)
        {
            CurrentProject.Sections.Add(section);
            return CurrentProject;
        }

        public Task<Project> UpdateProjectSectionsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Project>> GetAllProjectsAsync()
        {
            try
            {
                if (_context!= null)
                    return await _context.Include("Sections").ToListAsync() ?? throw new NullReferenceException($"GetAllEntities query returned null for the GetAllEntities");
                throw new NullReferenceException("GetAllEntities got a null _dbSet for the GetAllEntities");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetAllEntities method \n{ex.Message}");
                return Enumerable.Empty<Project>();
            }
        }
    }
}
