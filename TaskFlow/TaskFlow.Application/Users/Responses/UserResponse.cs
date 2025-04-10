using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskFlow.Application.Users.Responses
{
    public record class UserResponse(int UserId, string UserEmail);
}
