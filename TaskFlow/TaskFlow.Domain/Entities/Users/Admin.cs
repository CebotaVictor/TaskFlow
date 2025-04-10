using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Domain.Entities.Users;
using TaskFlow.Domain.Enums;

namespace TaskFlow.Domain.Entities.Users
{
    public sealed class Admin : User
    {
        [Column("AdminRole", Order = 5, TypeName = "ushort")]
        public Roles Role { get; set; } = Roles.Admin;
    }
}
