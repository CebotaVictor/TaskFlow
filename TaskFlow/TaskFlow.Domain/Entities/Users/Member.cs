using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Domain.Enums;
using TaskFlow.Domain.Entities.Users;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskFlow.Domain.Entities.Users
{

    public sealed class Member : User
    {
        [Column("MembersRole", Order = 5, TypeName = "smallint")]
        public Roles Role { get; set; } = Roles.employee;
    }
}
    