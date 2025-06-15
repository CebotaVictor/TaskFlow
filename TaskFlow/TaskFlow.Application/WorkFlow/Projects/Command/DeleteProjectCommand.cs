using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Application.Contracts.Shared;

namespace TaskFlow.Application.WorkFlow.Projects.Command
{
    public record DeleteProjectCommand : IRequest<WorkflowResponse>
    {
        [Required]
        public ushort Id { get; set; }
    }
}
