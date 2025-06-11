using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Application.Contracts.Shared;
using TaskFlow.Application.Interfaces.UnitOfWork;
using TaskFlow.Application.WorkFlow.Sections.Queries;
using TaskFlow.Domain.Entities.Labels;

namespace TaskFlow.Application.WorkFlow.Sections.Handler
{
    public class GetAllSectionQueryHandler : IRequestHandler<GetAllSectionQuery, IEnumerable<Section>>
    {
        private IWorkflowUnitOfWork _unitOfWork;
        private readonly ILogger<GetAllSectionQueryHandler> _logger;

        public GetAllSectionQueryHandler(IWorkflowUnitOfWork unitOfWork, ILogger<GetAllSectionQueryHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new NullReferenceException("IGenericRepository is null in GetAllSectionsCommandHandler");
            _logger = logger ?? throw new NullReferenceException(nameof(logger));
        }

        public async Task<IEnumerable<Section>> Handle(GetAllSectionQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Sections.GetAllEntities() ?? throw new NullReferenceException("GetAllEntitiesAsync returned null in GetAllProjectQueryHandler");
        }

    }
}
