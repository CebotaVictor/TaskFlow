using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Application.Contracts.Shared;
using TaskFlow.Application.Interfaces.UnitOfWork;
using TaskFlow.Application.Users.Responses;
using TaskFlow.Application.WorkFlow.Projects.Command;

namespace TaskFlow.Application.WorkFlow.Projects.Handler
{
    internal class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand, WorkflowResponse>
    {
        private IWorkflowUnitOfWork _unitOfWork;
        private readonly ILogger<CreateProjectCommandHandler> _logger;

        public DeleteProjectCommandHandler(IWorkflowUnitOfWork unitOfWork, ILogger<CreateProjectCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<WorkflowResponse> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            if (request == null) { return new WorkflowResponse(null!); }
            try
            {
                await _unitOfWork.Project.DeleteByIdGenericAsync(request.Id);
                if (await _unitOfWork.SaveChangesAsync() > 0)
                {
                    return new WorkflowResponse($"Successfully deleted project with id {request.Id}");
                }
                throw new Exception($"Error while deleting the project with id {request.Id}");
            }
            catch (Exception ex)
            {
                return new WorkflowResponse(ex.Message);
            }
        }
    }
}
