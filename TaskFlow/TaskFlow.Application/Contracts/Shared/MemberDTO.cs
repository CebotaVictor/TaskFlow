using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TaskFlow.Application.Users.Responses;

namespace TaskFlow.Application.Contracts.Shared
{
    public record  MemberDTO(
        string Username,
        string Password,
        string Email
    ) : IRequest<UserResponse>;
}
