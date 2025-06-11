using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Application.Contracts.Shared;
using TaskFlow.Application.Interfaces.Repository;
using TaskFlow.Application.Interfaces.UnitOfWork;
using TaskFlow.Application.WorkFlow.Projects.Command;
using TaskFlow.Application.WorkFlow.Projects.Queries;
using TaskFlow.Application.WorkFlow.Sections.Queries;
using TaskFlow.Domain.Entities.Projects;

namespace TaskFlow.Application.WorkFlow.Projects.Handler
{
    public class GetAllProjectsQueryHandler : IRequestHandler<GetAllProjectsQuery, IEnumerable<Project>>
    {
        private IWorkflowUnitOfWork _unitOfWork;
        private IProjectRepository _projectRepository;
        private readonly ILogger<GetAllProjectsQueryHandler> _logger;

        public GetAllProjectsQueryHandler(IWorkflowUnitOfWork unitOfWork, ILogger<GetAllProjectsQueryHandler> logger, IProjectRepository projectRepository)
        {
            _unitOfWork = unitOfWork ?? throw new NullReferenceException("IGenericRepository is null in CreateProjectCommandHandler");
            _logger = logger ?? throw new NullReferenceException(nameof(logger));
            _projectRepository = projectRepository ?? throw new NullReferenceException("IProjectRepository is null in GetAllSectionQueryHandler");
        }

       
        public async Task<IEnumerable<Project>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
        {
            return await _projectRepository.GetAllProjectsAsync() ?? throw new NullReferenceException("GetAllEntitiesAsync returned null in GetAllProjectQueryHandler");
        }
    }
}
