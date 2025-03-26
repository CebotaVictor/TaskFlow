using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskFlow.Application.Interfaces.Repository
{
    internal interface IGenericRepository<TEntity, ITd> where TEntity : class
    {
        public Task AddGeneric(TEntity pacient);
        public Task<bool> DeletByIdGenericAsync(int Id);
        public Task<TEntity> GetEntityById(ITd Id);
        public Task<IEnumerable<TEntity>> GetAllEntities();
    }
}
