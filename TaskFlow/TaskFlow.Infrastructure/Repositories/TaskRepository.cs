using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Application.Interfaces.Repository;
using TaskFlow.Domain.Entities.Projects;
using TaskFlow.Domain.Entities.Tasks;
using TaskFlow.Infrastructure.BL;

namespace TaskFlow.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly WorkflowGenericRepository<UTask>? _repository;
        private readonly DbSet<UTask>? _context;
        private readonly ILogger<TaskRepository> _logger;

        public TaskRepository(WorkflowGenericRepository<UTask>? repository, WorkflowDBContext? context, ILogger<TaskRepository> logger)
        {
            _repository = repository;
            _context = context!.Set<UTask>();
            _logger = logger;
        }
        public async Task<IEnumerable<UTask>> GetAllTasks()
        {
            try
            {
                if (_context != null)
                    return await _context.Include(t => t.Tasks) .ToListAsync() ?? throw new NullReferenceException($"GetAllEntities query returned null for the GetAllEntities");
                throw new NullReferenceException("GetAllEntities got a null _dbSet for the GetAllEntities");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetAllEntities method \n{ex.Message}");
                return Enumerable.Empty<UTask>();
            }
        }
    }
}
