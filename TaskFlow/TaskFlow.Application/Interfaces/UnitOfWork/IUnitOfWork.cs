using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Application.Interfaces.Repository;
using TaskFlow.Domain.Entities.Users;

namespace TaskFlow.Application.Interfaces.UnitOfWork
{
    public interface IUnitOfWork<TEntity>   where TEntity : class
    {
        public IGenericRepository<TEntity> Users { get; }


        Task<int> SaveChangesAsync();
    }
}
