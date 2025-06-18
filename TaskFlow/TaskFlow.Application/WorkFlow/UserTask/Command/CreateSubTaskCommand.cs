using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Application.Contracts.Shared;

namespace TaskFlow.Application.WorkFlow.UserTask.Command
{
    public class CreateSubTaskCommand : IRequest<WorkflowResponse>
    {
        public ushort Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
        [Required]
        public ushort SectionId { get; set; }

        [Required]
        public ushort ParentId { get; set; }
    }
}
