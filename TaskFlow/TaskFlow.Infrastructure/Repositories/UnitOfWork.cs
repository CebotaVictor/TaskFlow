using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Application.Interfaces.Repository;
using TaskFlow.Application.Interfaces.UnitOfWork;
using TaskFlow.Domain.Entities.Users;
using TaskFlow.Infrastructure.BL;

namespace TaskFlow.Infrastructure.Repositories
{
    internal class UnitOfWork<TEntity> : IUnitOfWork<TEntity> where TEntity : class
    {
        private readonly UsersDBContext _dbContext;

        public IGenericRepository<TEntity> Users { get; }

        public UnitOfWork(UsersDBContext context, IGenericRepository<TEntity> users)
        {
           _dbContext = context;
            Users = users;
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
