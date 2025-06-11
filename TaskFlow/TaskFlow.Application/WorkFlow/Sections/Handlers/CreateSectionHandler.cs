using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Application.Contracts.Shared;
using TaskFlow.Application.Interfaces.UnitOfWork;
using TaskFlow.Application.WorkFlow.Projects.Handler;
using TaskFlow.Application.WorkFlow.Sections.Commands;
using TaskFlow.Domain.Entities.Labels;
using TaskFlow.Domain.Entities.Projects;

namespace TaskFlow.Application.WorkFlow.Sections.Handlers
{
    internal sealed class CreateSectionHandler : IRequestHandler<CreateSectionCommand, WorkflowResponse>
    {
        private IWorkflowUnitOfWork _unitOfWork;
        private readonly ILogger<CreateProjectCommandHandler> _logger;

        public CreateSectionHandler(IWorkflowUnitOfWork unitOfWork, ILogger<CreateProjectCommandHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new NullReferenceException("IGenericRepository is null in CreateProjectCommandHandler");
            _logger = logger ?? throw new NullReferenceException(nameof(logger));
        }

        public async Task<WorkflowResponse> Handle(CreateSectionCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                return new WorkflowResponse(null!);
            }
            try
            {
                Section section = new Section
                {
                    Name = request.Name,
                    ProjectId = request.ProjectId
                };

                await _unitOfWork.Sections.AddGeneric(section);

                if (await _unitOfWork.SaveChangesAsync() > 0)
                {
                    return new WorkflowResponse(section.Name!);
                }


                throw new Exception("Save changes did not worked as intended for AddMemberHandler");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new WorkflowResponse(null!);
            }
        }
    }
}
