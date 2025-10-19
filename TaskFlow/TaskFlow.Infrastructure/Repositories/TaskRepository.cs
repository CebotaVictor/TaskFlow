using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private readonly WorkflowDBContext? _context;
        private readonly ILogger<TaskRepository> _logger;

        [DebuggerStepThrough]
        public TaskRepository(WorkflowGenericRepository<UTask>? repository, WorkflowDBContext? context, ILogger<TaskRepository> logger)
        {
            _repository = repository;
            _context = context;
            _logger = logger;
        }
        /*
         SELECT t.Id, t.Name, st.Id, st.ParentTaskId
         FROM Tasks AS t
         LEFT JOIN Tasks AS st ON t.Id = st.ParentTaskId
         WHERE t.Id = @Id
         */
        public async Task<bool> DeleteTaskByIdAsync(ushort Id)
        {
            //get the first task 
            var task = await _context?.Tasks?.Include(t => t.Tasks).FirstOrDefaultAsync(t => t.Id == Id)!; //null forgiving operator 

            if (task == null) return false;

            //iterate over the subtasks recursively and delete them

            foreach (var subTask in task.Tasks.ToList())
            {
                await DeleteTaskByIdAsync(subTask.Id);
            }

            //remove the task from the dbset
            _context?.Tasks?.Remove(task);
            return true;
        }

        public async Task<IEnumerable<UTask>> GetAllTasksAsync()
        {
            try
            {
                if (_context != null)
                    return await _context.Tasks.Include(t => t.Tasks).ToListAsync() ?? throw new NullReferenceException($"GetAllTasks query returned null for the TaskRepository");
                throw new NullReferenceException("GetAllTasks got a null _dbSet for the TaskRepository");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetAllTasks method \n{ex.Message}");
                return Enumerable.Empty<UTask>();
            }
        }


    }
}
