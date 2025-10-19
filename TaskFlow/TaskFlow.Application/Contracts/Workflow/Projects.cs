using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Domain.Entities.WSections;

namespace TaskFlow.Application.Contracts.Workflow
{
    public class ProjectDTO
    {
        public ushort Id { get; set; }
        public string? Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public ushort? UserId { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public DateTime? EndDate { get; set; }
        public ICollection<Section> ?Sections { get; set; }
    }
}
