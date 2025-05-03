using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Application.Contracts.Authentication;
using TaskFlow.Application.Users.Responses;

namespace TaskFlow.Application.Autentication.Commands
{
    public record RegisterCommand : IRequest<UserResponse>
    {
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    } 
}
