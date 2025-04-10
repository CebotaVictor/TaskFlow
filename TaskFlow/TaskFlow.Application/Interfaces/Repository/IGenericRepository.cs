﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskFlow.Application.Interfaces.Repository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        public Task AddGeneric(TEntity user);
        public Task<bool> DeletByIdGenericAsync(int Id);
        public Task<TEntity> GetEntityById(int Id);
        public Task<IEnumerable<TEntity>> GetAllEntities();
    }
}
