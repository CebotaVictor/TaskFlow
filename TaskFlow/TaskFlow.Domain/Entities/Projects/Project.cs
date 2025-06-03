using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Domain.Entities.Labels;

namespace TaskFlow.Domain.Entities.Projects
{
    [Table("project", Schema = "TaskFlow")]
    public class Project
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ProjectId", Order = 0, TypeName = "smallint")]
        public ushort Id { get; set; }

        [Column("ProjectName", Order = 1)]
        public string? Name { get; set; } = string.Empty;

        [Column("ProjectDescription", Order = 2)]
        public string? Description { get; set; } = string.Empty;

        [Column(Order = 3, TypeName = "smallint")]
        public ushort? UserId { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public DateTime? EndDate { get; set; }
        public ICollection<Section> Sections { get; } =     new List<Section>();
    }
}
