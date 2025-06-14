using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required]
        [StringLength(50, MinimumLength = 20, ErrorMessage = "Name must be at least 6 characters long")]
        public string? Name { get; set; } = string.Empty;
        [Required]
        public ushort? ProjectId { get; set; }
    }
}
