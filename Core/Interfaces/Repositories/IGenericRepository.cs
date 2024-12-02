using Core.Entities;
using Core.Interfaces.Services;
using System.Linq.Expressions;

namespace Core.Interfaces.Repositories
{
    public interface IGenericRepository<T> where T :class, IBaseEntity
    {
        Task<T?> GetByIdAsync(int id);
        Task<T?> GetOneAsync(Expression<Func<T, bool>> predicate);
        Task<T?> GetOneAsync(ISpecifications<T> specifications);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetAllAsync(ISpecifications<T> specifications);
        Task<int> GetCountAsync();
        Task<int> GetCountAsync(ISpecifications<T> specifications);
        Task<int> GetCountAsync(Expression<Func<T, bool>> predicate);

        Task<T> AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
