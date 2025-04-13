using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TaskFlow.Application.Users.Responses;

namespace TaskFlow.Application.Users.Members.Commands
{
    public record  CreateMemeberCommand(
        int MemberId,
        string Username,
        string Password,
        string Email
    ) : IRequest<UserResponse>;
}
