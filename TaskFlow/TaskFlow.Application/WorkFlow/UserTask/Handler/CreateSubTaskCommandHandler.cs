using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Application.Contracts.Shared;
using TaskFlow.Application.Interfaces.UnitOfWork;
using TaskFlow.Application.WorkFlow.UserTask.Command;
using TaskFlow.Domain.Entities.Tasks;

namespace TaskFlow.Application.WorkFlow.UserTask.Handler
{
    public class CreateSubTaskCommandHandler : IRequestHandler<CreateSubTaskCommand, WorkflowResponse>
    {
        private IWorkflowUnitOfWork _unitOfWork;
        private readonly ILogger<CreateSubTaskCommandHandler> _logger;

        public CreateSubTaskCommandHandler(IWorkflowUnitOfWork unitOfWork, ILogger<CreateSubTaskCommandHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new NullReferenceException("IGenericRepository is null in CreateTaskHandler");
            _logger = logger ?? throw new NullReferenceException(nameof(logger));
        }

        public async Task<WorkflowResponse> Handle(CreateSubTaskCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null) return new WorkflowResponse(null!);
                var newTask = new UTask
                {
                    Title = request.Title,
                    Description = request.Description,
                    ParentTaskId = request.ParentId,
                    SectionId = request.SectionId,
                    CreatedAt = DateTime.UtcNow
                };

                await _unitOfWork.Tasks.AddGeneric(newTask);

                if (await _unitOfWork.SaveChangesAsync() > 0)
                {
                    return new WorkflowResponse(newTask.Title);
                }
                return new WorkflowResponse(null!);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a task");
                return new WorkflowResponse(null!);
            }
        }
    
    }
}
