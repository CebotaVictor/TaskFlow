using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace TaskFlow.Application.Users.Members.Commands
{
    public record  CreateMemeberCommand(
        int MemeberId,
        string Username,
        string Password,
        string Email
    );
}
