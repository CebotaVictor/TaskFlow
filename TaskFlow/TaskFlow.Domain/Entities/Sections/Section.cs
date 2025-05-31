using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TaskFlow.Domain.Entities.Projects;

namespace TaskFlow.Domain.Entities.Labels
{
    [Table("sections", Schema = "TaskFlow")]
    public class Section
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("SectionId", Order = 0, TypeName = "smallint")]
        public int Id { get; set; }

        [Column("SectionName", Order = 1, TypeName = "smallint")]
        public string Name { get; set; } = string.Empty;

        [Column(TypeName = "smallint")]
        public ushort ProjectId { get; set; }

        public ICollection<Task> Tasks { get;} = new List<Task>();
        public Project Project { get; set; } = null!;
    }
}
