using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TaskFlow.Application.Contracts.Shared;

namespace TaskFlow.Application.WorkFlow.Projects.Command
{
    public class CreateProjectCommand : IRequest<WorkflowResponse>
    {
        public ushort Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Name must be at least 6 characters long")]
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        [Required]
        public ushort UserId { get; set; }
    }
}
