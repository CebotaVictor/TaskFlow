using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Application.Contracts.Shared;
using TaskFlow.Application.Interfaces.UnitOfWork;
using TaskFlow.Application.WorkFlow.Projects.Command;
using TaskFlow.Application.WorkFlow.Projects.Handler;
using TaskFlow.Application.WorkFlow.Sections.Commands;

namespace TaskFlow.Application.WorkFlow.Sections.Handlers
{
    internal class DeleteSectionCommandHandler : IRequestHandler<DeleteSectionCommand, WorkflowResponse>
    {
        private IWorkflowUnitOfWork _unitOfWork;
        private readonly ILogger<CreateProjectCommandHandler> _logger;

        public DeleteSectionCommandHandler(IWorkflowUnitOfWork unitOfWork, ILogger<CreateProjectCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<WorkflowResponse> Handle(DeleteSectionCommand request, CancellationToken cancellationToken)
        {
            if (request == null) { return new WorkflowResponse(null!); }
            try
            {
                await _unitOfWork.Sections.DeleteByIdGenericAsync(request.Id);
                if (await _unitOfWork.SaveChangesAsync() > 0)
                {
                    return new WorkflowResponse($"Successfully deleted section with id {request.Id}");
                }
                throw new Exception($"Error while deleting the section with id {request.Id}");
            }
            catch (Exception ex)
            {
                return new WorkflowResponse(ex.Message);
            }
        }

    }
}
