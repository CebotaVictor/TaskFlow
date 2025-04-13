using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Application.Contracts.Shared;
using TaskFlow.Application.Users.Responses;

namespace TaskFlow.Application.Users.Members.Commands
{
    public class UpdateMemberCommand : IRequest<UserResponse>
    {
        public ushort Id { get; set; }
        public MemberDTO ? MemberField { get; set; }
    }
}
