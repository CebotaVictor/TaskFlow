using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskFlow.Application.WorkFlow.Project.Command
{
    public class CreateProjectCommand
    {
        public ushort Id { get; set; }        
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ushort UserId { get; set; }
    }
}
