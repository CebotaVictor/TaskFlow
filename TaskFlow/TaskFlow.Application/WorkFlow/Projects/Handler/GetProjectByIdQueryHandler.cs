using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;
using Microsoft.Extensions.Logging;
using TaskFlow.Application.Interfaces.Repository;
using TaskFlow.Application.Interfaces.UnitOfWork;
using TaskFlow.Application.WorkFlow.Projects.Queries;
using TaskFlow.Domain.Entities.Projects;
namespace TaskFlow.Application.WorkFlow.Projects.Handler
{

    internal class GetProjectByIdQueryHandler : IRequestHandler<GetProjectByIdQuery, Project>
    {
        private IWorkflowUnitOfWork _unitOfWork;
        private IProjectRepository _projectRepository;
        private readonly ILogger<GetProjectByIdQueryHandler> _logger;

        public GetProjectByIdQueryHandler(IWorkflowUnitOfWork unitOfWork, ILogger<GetProjectByIdQueryHandler> logger, IProjectRepository projectRepository)
        {
            _unitOfWork = unitOfWork ?? throw new NullReferenceException("IGenericRepository is null in CreateProjectCommandHandler");
            _logger = logger ?? throw new NullReferenceException(nameof(logger));
            _projectRepository = projectRepository ?? throw new NullReferenceException("IProjectRepository is null in GetAllSectionQueryHandler");
        }

        public async Task<Project> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _projectRepository.GetProjectByIdAsync(request.Id);
            if (result == null)
            {
                _logger.LogError($"Project with ID {request.Id} not found.");
                throw new NullReferenceException($"Project with ID {request.Id} not found.");
            }
            return result;
        }
    }

}
