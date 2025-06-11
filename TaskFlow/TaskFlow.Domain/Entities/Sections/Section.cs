using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TaskFlow.Domain.Entities.Projects;
using TaskFlow.Domain.Entities.Tasks;

namespace TaskFlow.Domain.Entities.Labels
{
    [Table("sections", Schema = "TaskFlow")]
    public class Section
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("SectionId", Order = 0, TypeName = "smallint")]
        public ushort ?Id { get; set; }

        [Column("SectionName", Order = 1)]
        public string ?Name { get; set; } = string.Empty;

        [Column(TypeName = "smallint")]
        public ushort? ProjectId { get; set; }

        public ICollection<UTask> Tasks { get;} = new List<UTask>();
    }
}
