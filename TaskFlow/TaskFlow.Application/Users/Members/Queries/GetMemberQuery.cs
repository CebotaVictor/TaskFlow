using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TaskFlow.Domain.Entities.Users;

namespace TaskFlow.Application.Users.Members.Queries
{
    public record GetMemberQuery : IRequest<IEnumerable<Member>>;

}
