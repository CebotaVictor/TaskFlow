using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Domain.Enums;

namespace TaskFlow.Application.Contracts.Shared
{
    public record JwtUserDTO(
     ushort Id,
     string Email,
     Roles Role
  );

}
