using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Domain.Entities.Users;

namespace TaskFlow.Application.Interfaces.Repository
{
    public interface IAdminRepository
    {
        Task<Admin> GetAdminByEmail(string Email);
    }
}
