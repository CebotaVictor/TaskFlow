using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TaskFlow.Application.Interfaces.Repository;
using TaskFlow.Infrastructure.BL;

namespace TaskFlow.Infrastructure.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private DbContext _context;
        private DbSet<TEntity>? _dbSet;
        private readonly ILogger<GenericRepository<TEntity>> _logger;

        public GenericRepository(UsersDBContext context, ILogger<GenericRepository<TEntity>> logger)
        {
            this._context = context ?? throw new ArgumentNullException($"the PacientiDbContext is null ${nameof(context)}");
            this._dbSet = context.Set<TEntity>();
            this._logger = logger;
        }

        public async Task AddGeneric(TEntity entity)
        {
            if (_dbSet != null)
                await _dbSet.AddAsync(entity);
            else throw new NullReferenceException($"_dbSet is null");
        }

        public async Task<bool> DeletByIdGenericAsync(int Id)
        {
            try
            {
                if (_dbSet != null)
                {
                    var entityToDelete = await _dbSet.FindAsync(Id);
                    if (entityToDelete != null)
                    {
                        _dbSet.Remove(entityToDelete);
                        _logger.LogInformation($"Pacient with id {Id} was deleted successfully");
                        return true;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while deleting pacient {Id} {ex.Message}");
                return false;
            }
            

        }

        public async Task<TEntity> GetEntityById(int Id)
        {
            if(_dbSet != null)
                return await _dbSet.FindAsync(Id) ?? throw new NullReferenceException($"GetEntityById query returned null");
            throw new NullReferenceException("GetEntityById got a null _dbSet");
        }


        public async Task<IEnumerable<TEntity>> GetAllEntities()
        {
            if( _dbSet != null)
                return await _dbSet.ToListAsync() ?? throw new NullReferenceException($"GetAllEntities query returned null");
            throw new NullReferenceException("GetAllEntities got a null _dbSet");
        }
    }
}
