using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;
using TaskFlow.Application.WorkFlow.Projects.Queries;
using TaskFlow.Domain.Entities.Projects;
namespace TaskFlow.Application.WorkFlow.Projects.Handler
{

    internal class GetProjectByIdQueryHandler : IRequestHandler<GetProjectByIdQuery, Project>
    {
        public Task<Project> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }

}
