using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Application.Contracts.Shared;
using TaskFlow.Application.Users.Responses;

namespace TaskFlow.Application.Users.Admins.Commands
{
    public class AddAdminCommand : IRequest<UserResponse>
    {
        public UserDTO? AdminField { get; set; }
    }
}
