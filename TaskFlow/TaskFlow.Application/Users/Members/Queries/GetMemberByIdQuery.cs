using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Domain.Entities.Users;

namespace TaskFlow.Application.Users.Members.Queries
{
    public class GetMemberByIdQuery : IRequest<Member>
    {
        public ushort id { get; set; }
    }
}
