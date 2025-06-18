using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Domain.Entities.Tasks;

namespace TaskFlow.Application.WorkFlow.UserTask.Queries
{
    public record GetAllTasksQuery : IRequest<IEnumerable<UTask>>;
}
