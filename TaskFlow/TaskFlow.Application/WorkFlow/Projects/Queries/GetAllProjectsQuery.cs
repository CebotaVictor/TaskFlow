using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Domain.Entities.Projects;

namespace TaskFlow.Application.WorkFlow.Projects.Queries
{
    public record GetAllProjectsQuery : IRequest<IEnumerable<Project>>;
}
