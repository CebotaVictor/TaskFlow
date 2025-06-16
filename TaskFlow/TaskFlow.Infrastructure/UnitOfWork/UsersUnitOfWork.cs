using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Application.Interfaces.Repository;
using TaskFlow.Application.Interfaces.UnitOfWork;
using TaskFlow.Domain.Entities.Users;
using TaskFlow.Infrastructure.BL;
using TaskFlow.Infrastructure.Repositories;

namespace TaskFlow.Infrastructure.UnitOfWork
{
    public class UsersUnitOfWork : IUsersUnitOfWork
    {
        private readonly UsersDBContext _dbContext;
        public IUsersGenericRepository<Member> Users { get; }

        public IMemberRepository Members { get; }
        public IAdminRepository AdminRepo { get; }

        public IUsersGenericRepository<Admin> Admins { get; }

        public UsersUnitOfWork(UsersDBContext context, IUsersGenericRepository<Member> users, IMemberRepository member, IAdminRepository adminRepo, IUsersGenericRepository<Admin> admin)
        {
           _dbContext = context;
            Users = users ?? throw new ArgumentNullException(nameof(adminRepo), "Member generic repository cannot be null");
            Members = member ?? throw new ArgumentNullException(nameof(adminRepo), "Member repository cannot be null");
            Admins = admin ?? throw new ArgumentNullException(nameof(adminRepo), "Admin generic repository cannot be null");
            AdminRepo = adminRepo ?? throw new ArgumentNullException(nameof(adminRepo), "Admin repository cannot be null");
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
