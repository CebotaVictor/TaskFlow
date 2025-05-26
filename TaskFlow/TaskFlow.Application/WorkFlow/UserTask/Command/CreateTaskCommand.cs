using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Domain.Enums;

namespace TaskFlow.Application.WorkFlow.UserTask.Command
{
    public class CreateTaskCommand
    {
        public ushort Id { get; set; }

        [Required]
        [Column("TaskTitle", Order = 1)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [Column("TaskDescription", Order = 2)]
        public string Description { get; set; } = string.Empty;
    }
}
