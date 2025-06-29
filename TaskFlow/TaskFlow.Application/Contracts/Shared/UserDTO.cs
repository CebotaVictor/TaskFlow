﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TaskFlow.Application.Users.Responses;

namespace TaskFlow.Application.Contracts.Shared
{
    public record  UserDTO(
        string Username,
        string Email,
        string Password
    ) : IRequest<UserResponse>;
}
