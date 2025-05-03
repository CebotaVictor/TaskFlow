using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TaskFlow.Application.Interfaces.Repository;
using TaskFlow.Domain.Entities.Users;
using TaskFlow.Infrastructure.BL;

namespace TaskFlow.Infrastructure.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private DbSet<TEntity>? _dbSet;
        private readonly ILogger<GenericRepository<TEntity>> _logger;

        public GenericRepository(UsersDBContext context, ILogger<GenericRepository<TEntity>> logger)
        {
            this._dbSet = context.Set<TEntity>();
            this._logger = logger;
        }

        public async Task AddGeneric(TEntity entity)
        {
            try
            {
                if (_dbSet != null)
                    await _dbSet.AddAsync(entity);
                else throw new NullReferenceException($"_dbSet is null");
            }

            catch(Exception ex)
            {
                _logger.LogError($"Failed to create a new TEntity {ex.Message}");
                return;
            }
        }

        public async Task<bool> DeletByIdGenericAsync(ushort Id)
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
                throw new NullReferenceException($"Error while deleting pacient {Id}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while deleting pacient {Id} {ex.Message}");
                return false;
            }
        }

        public async Task<TEntity> GetEntityById(ushort Id)
        {
            try
            {
                if (_dbSet != null)
                    return await _dbSet.FindAsync(Id) ?? throw new NullReferenceException($"GetEntityById query returned null for the GetAllEntitiesById");
                throw new NullReferenceException("GetEntityById got a null _dbSet for the GetAllEntitiesById");
            }
            catch (Exception ex) 
            {
                _logger.LogError($"Error while deleting pacient {Id} {ex.Message}");
                return null!;
            }
        }


        public async Task<IEnumerable<TEntity>> GetAllEntities()
        {
            try
            {
                if (_dbSet != null)
                    return await _dbSet.ToListAsync() ?? throw new NullReferenceException($"GetAllEntities query returned null for the GetAllEntities");
                throw new NullReferenceException("GetAllEntities got a null _dbSet for the GetAllEntities");
            }
            catch (Exception ex) 
            { 
                _logger.LogError($"Error in GetAllEntities method \n{ex.Message}");
                return Enumerable.Empty<TEntity>();
            }
        }

    }
}
