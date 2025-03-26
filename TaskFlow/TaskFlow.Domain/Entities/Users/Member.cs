using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Domain.Enums;
using TaskFlow.Domain.Entities.Users;

namespace TaskFlow.Domain.Entities.Users
{
    internal sealed class Member : User
    {
        public Roles Role { get; set; } = Roles.employee;
    }
}
