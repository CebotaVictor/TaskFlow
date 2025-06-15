using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Application.Contracts.Shared;

namespace TaskFlow.Application.WorkFlow.Sections.Commands
{
    public class DeleteSectionCommand : IRequest<WorkflowResponse>
    {
        [Required]
        public ushort Id { get; set; }
    }
}
