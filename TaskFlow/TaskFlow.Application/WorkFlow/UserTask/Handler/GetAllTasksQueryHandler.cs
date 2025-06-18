using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Application.Interfaces.Repository;
using TaskFlow.Application.Interfaces.UnitOfWork;
using TaskFlow.Application.WorkFlow.UserTask.Queries;
using TaskFlow.Domain.Entities.Tasks;

namespace TaskFlow.Application.WorkFlow.UserTask.Handler
{
    public class GetAllTasksQueryHandler : IRequestHandler<GetAllTasksQuery, IEnumerable<UTask>>
    {

        private readonly ITaskRepository _taskRepository;
        private readonly ILogger<GetAllTasksQueryHandler> _logger;

        public GetAllTasksQueryHandler(ITaskRepository taskRepository, ILogger<GetAllTasksQueryHandler> logger)
        {
            _taskRepository = taskRepository ?? throw new NullReferenceException("IGenericRepository is null in CreateTaskHandler");
            _logger = logger ?? throw new NullReferenceException(nameof(logger));
        }


        public async Task<IEnumerable<UTask>> Handle(GetAllTasksQuery request, CancellationToken cancellationToken)
        {
            if(request == null) return Enumerable.Empty<UTask>();
            try
            {
                var tasks = await _taskRepository.GetAllTasks();
                if (tasks == null || !tasks.Any())
                {
                    _logger.LogWarning("No tasks found.");
                    return Enumerable.Empty<UTask>();
                }
                return tasks;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving tasks.");
                throw;
            }
        }
    }
}
