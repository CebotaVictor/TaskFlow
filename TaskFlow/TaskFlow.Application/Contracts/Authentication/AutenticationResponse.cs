using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Domain.Entities.Users;

namespace TaskFlow.Application.Contracts.Authentication
{
    public record AutenticationResponse(string Email, string Token);
}
