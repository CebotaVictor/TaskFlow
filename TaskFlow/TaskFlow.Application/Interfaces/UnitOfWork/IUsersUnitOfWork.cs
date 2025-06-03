using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Application.Interfaces.Repository;
using TaskFlow.Domain.Entities.Users;

namespace TaskFlow.Application.Interfaces.UnitOfWork
{
    public interface IUsersUnitOfWork
    {
        public IUsersGenericRepository<Member> Users { get; }
        public IUsersGenericRepository<Admin> Admins { get; }
        public IMemberRepository Members{ get; }


        Task<int> SaveChangesAsync();
    }
}
