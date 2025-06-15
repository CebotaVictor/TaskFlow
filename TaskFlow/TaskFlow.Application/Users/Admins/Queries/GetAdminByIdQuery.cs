using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Domain.Entities.Users;

namespace TaskFlow.Application.Users.Admins.Queries
{
    public class GetAdminByIdQuery : IRequest<Admin>
    {
        public ushort id { get; set; }
    }
}
