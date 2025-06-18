using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Application.Contracts.Shared;

namespace TaskFlow.Infrastructure.UserTask.Command  
{
    public class DeleteTaskCommand : IRequest<WorkflowResponse>
    {
        public ushort Id { get; set; }
    }
}
