using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Domain.Entities.Users;
using TaskFlow.Domain.Enums;

namespace TaskFlow.Domain.Entities.Users
{
    internal sealed class Admin : User
    {
        public Roles Role { get; set; } = Roles.Admin;
    }
}
