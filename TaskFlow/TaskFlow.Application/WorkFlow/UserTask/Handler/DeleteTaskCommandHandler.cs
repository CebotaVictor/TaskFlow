using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Application.Contracts.Shared;
using TaskFlow.Application.Interfaces.UnitOfWork;
using TaskFlow.Application.WorkFlow.Projects.Handler;
using TaskFlow.Infrastructure.UserTask.Command;

namespace TaskFlow.Application.WorkFlow.UserTask.Handler
{
    public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand, WorkflowResponse>
    {
        private IWorkflowUnitOfWork _unitOfWork;
        private readonly ILogger<DeleteTaskCommandHandler> _logger;

        public DeleteTaskCommandHandler(IWorkflowUnitOfWork unitOfWork, ILogger<DeleteTaskCommandHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new NullReferenceException("IGenericRepository is null in CreateProjectCommandHandler");
            _logger = logger ?? throw new NullReferenceException(nameof(logger));
        }
        public async Task<WorkflowResponse> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            if (request == null) { return new WorkflowResponse(null!); }
            try
            {
                await _unitOfWork.Tasks.DeleteByIdGenericAsync(request.Id);
                if (await _unitOfWork.SaveChangesAsync() > 0)
                {
                    return new WorkflowResponse($"Successfully deleted task with id {request.Id}");
                }
                throw new Exception($"Error while deleting the taks with id {request.Id}");
            }
            catch (Exception ex)
            {
                return new WorkflowResponse(ex.Message);
            }
        }
    }
}
