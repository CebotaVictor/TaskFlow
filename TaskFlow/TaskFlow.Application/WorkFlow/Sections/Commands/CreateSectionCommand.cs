using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Application.Contracts.Shared;

namespace TaskFlow.Application.WorkFlow.Sections.Commands
{
    public record CreateSectionCommand : IRequest<WorkflowResponse>
    {
        public ushort? Id { get; set; }
        public string? Name { get; set; } = string.Empty;
        public ushort? ProjectId { get; set; }
    }
}
