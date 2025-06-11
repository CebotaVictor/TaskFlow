using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Domain.Entities.Labels;

namespace TaskFlow.Application.WorkFlow.Sections.Queries
{
    public record GetAllSectionQuery : IRequest<IEnumerable<Section>>;
}
