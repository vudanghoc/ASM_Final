using Microsoft.EntityFrameworkCore;
using Persistence;
using Services.Contracts.DataAccess.Base;

namespace DataAccess.DataAccess.Base
{
    public abstract class GeneralDataAccess<TEntity> : IGeneralDataAcces<TEntity> where TEntity : class
    {
        protected readonly ApplicationDbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public GeneralDataAccess(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public TEntity Add(TEntity entity)
        {
            _context.Add(entity);
            return entity;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public bool Delete(TEntity entity)
        {
            _context.Remove(entity);
            _context.SaveChanges();
            return true;
        }

        public TEntity Update(TEntity entity)
        {
            _context.Update(entity);
            _context.SaveChanges();
            return entity;
        }
    }
}
