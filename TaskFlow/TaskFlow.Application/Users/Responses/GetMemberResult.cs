using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Domain.Entities.Users;

namespace TaskFlow.Application.Users.Responses
{
    public record GetMemberResult(IEnumerable<Member> members);

}
