using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Application.Users.Responses;

namespace TaskFlow.Application.Users.Members.Commands
{
    public class DeleteMemberCommand : IRequest<UserResponse>
    {
        public ushort Id;
    }
}
