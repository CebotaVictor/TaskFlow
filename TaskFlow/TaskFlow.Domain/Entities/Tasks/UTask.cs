using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using TaskFlow.Domain.Enums;

namespace TaskFlow.Domain.Entities.Tasks
{
    [Table("tasks", Schema = "TaskFlow")]
    public class UTask
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("TaskId", Order = 0, TypeName = "smallint")]
        public ushort Id { get; set; }
        [Required]
        [Column("TaskTitle",Order = 1)]
        public string Title { get; set; } = string.Empty;
        [Column("TaskDescription", Order = 2)]
        public string Description { get; set; } = string.Empty;
        [Column("TaskStatus", Order = 3)]
        public TaskState Status { get; set; } = TaskState.InProgres;
        [Column(Order = 4)]
        public DateTime? CreatedAt { get; set; } = DateTime.Now.Date;
        [Column(Order = 5)]
        public DateTime ?DueDate { get; set; }  
        [Column(TypeName = "smallint")]
        public ushort ?ParentTaskId { get; set; }
        [Column(TypeName = "smallint")]
        public ushort ?SectionId { get; set; }
        public ICollection<UTask> Tasks { get; } = new List<UTask>();
    }
}
