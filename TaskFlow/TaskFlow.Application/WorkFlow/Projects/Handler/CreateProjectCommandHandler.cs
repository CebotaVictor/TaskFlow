using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Application.Interfaces.UnitOfWork;
using TaskFlow.Domain.Entities.Projects;
using TaskFlow.Application.WorkFlow.Projects.Command;
using Microsoft.Extensions.DependencyInjection;
using TaskFlow.Application.Users.Responses;
using TaskFlow.Application.Contracts.Shared;
using System.Diagnostics;

using Microsoft.Extensions.Logging;
using TaskFlow.Application.Interfaces.Repository;

namespace TaskFlow.Application.WorkFlow.Projects.Handler
{
    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, WorkflowResponse>
    {
        private IWorkflowUnitOfWork _unitOfWork;
        private readonly ILogger<CreateProjectCommandHandler> _logger;

        public CreateProjectCommandHandler(IWorkflowUnitOfWork unitOfWork, ILogger<CreateProjectCommandHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new NullReferenceException("IGenericRepository is null in CreateProjectCommandHandler");
            _logger = logger ?? throw new NullReferenceException(nameof(logger));
        }

        public async Task<WorkflowResponse> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            if(request == null)
            {
                return new WorkflowResponse(null!);
            }
            try
            {
                Project project = new Project
                {
                    Name = request.Name,
                    Description = request.Description,
                    UserId = request.UserId,
                };

                await _unitOfWork.Project.AddGeneric(project);

                if (await _unitOfWork.SaveChangesAsync() > 0)
                {
                    return new WorkflowResponse(project.Name);
                }
                
                throw new Exception("Save changes did not worked as indended for AddMemberHandler");
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex.Message);
                return new WorkflowResponse(null!);
            }
        }
    }
}
