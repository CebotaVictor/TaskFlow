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
using TaskFlow.Application.WorkFlow.Sections.Queries;
using TaskFlow.Domain.Entities.WSections;

namespace TaskFlow.Application.WorkFlow.Sections.Handler
{
    public class GetAllSectionQueryHandler : IRequestHandler<GetAllSectionQuery, IEnumerable<Section>>
    {
        private ISectionRepository _sectionRepository;
        private readonly ILogger<GetAllSectionQueryHandler> _logger;

        public GetAllSectionQueryHandler(ISectionRepository sectionRepository, ILogger<GetAllSectionQueryHandler> logger)
        {
            _sectionRepository = sectionRepository ?? throw new NullReferenceException("ISectionRepository is null in GetAllSectionsCommandHandler");
            _logger = logger ?? throw new NullReferenceException(nameof(logger));
        }

        public async Task<IEnumerable<Section>> Handle(GetAllSectionQuery request, CancellationToken cancellationToken)
        {
            return await _sectionRepository.GetAllSectionsAsync() ?? throw new NullReferenceException("GetAllEntitiesAsync returned null in GetAllProjectQueryHandler");
        }

    }
}
