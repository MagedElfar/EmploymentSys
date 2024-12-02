using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, IBaseEntity
    {
        protected readonly AdbContext context;
        protected readonly DbSet<T> dbSet;

        public GenericRepository(AdbContext context)
        {
            this.context = context;
            dbSet = this.context.Set<T>();
        }

        public async Task<T> AddAsync(T entity)
        {
            await dbSet.AddAsync(entity);

            return entity;
        }

        public void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(ISpecifications<T> specifications)
        {
            return await ApplaySpecifications(specifications).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate)
        {
            return await dbSet.Where(predicate).ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task<int> GetCountAsync()
        {
            return await dbSet.CountAsync();
        }

        public async Task<int> GetCountAsync(ISpecifications<T> specifications)
        {
            return await ApplaySpecifications(specifications).CountAsync();
        }

        public async Task<int> GetCountAsync(Expression<Func<T, bool>> predicate)
        {
            return await dbSet.Where(predicate).CountAsync();
        }

        public async Task<T?> GetOneAsync(ISpecifications<T> specifications)
        {
            return await ApplaySpecifications(specifications).FirstOrDefaultAsync();
        }

        public async Task<T?> GetOneAsync(Expression<Func<T, bool>> predicate)
        {
            return await dbSet.FirstOrDefaultAsync(predicate);
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }

        public void Update (T entity)
        {

            dbSet.Attach(entity);
            dbSet.Entry(entity).State = EntityState.Modified;
            return;
            //dbSet.Update(entity);

            //return;
        }

        protected IQueryable<T> ApplaySpecifications(ISpecifications<T> specifications)
        {
            return SpecificationsEvaluate<T>.GetQuery(dbSet.AsQueryable() , specifications);
        }
    }
}
