using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Application.Contracts.Authentication;
using TaskFlow.Application.Users.Responses;

namespace TaskFlow.Application.Autentication.Queries
{
    public record LoginQuery(string Email, string password) : IRequest<AutenticationResponse>;
}
