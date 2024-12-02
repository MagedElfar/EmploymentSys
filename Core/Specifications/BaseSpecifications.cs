using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using System.Linq.Expressions;
using System.Reflection;

namespace Core.Specifications
{
    public class BaseSpecifications<T> : ISpecifications<T>
        where T : class, IBaseEntity
    {
        public BaseSpecifications(
            Expression<Func<T, bool>> criteria = null,
            List<Expression<Func<T, object>>> includes = null,
            List<Func<IQueryable<T>, IQueryable<T>>> thenIncludes = null,
            string orderBy = null,
            bool ascending = true,
            int take = 0,
            int skip = 0
        ){
            Criteria = criteria;
            Includes = includes ?? new List<Expression<Func<T, object>>>();
            ThenIncludes = thenIncludes ?? new List<Func<IQueryable<T>, IQueryable<T>>>();
            ApplySorting(orderBy, ascending);
            ApplyPaging(take, skip);
        }

        public Expression<Func<T, bool>> Criteria { get; private set; }
        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();
        public List<Func<IQueryable<T>, IQueryable<T>>> ThenIncludes { get; } = new List<Func<IQueryable<T>, IQueryable<T>>>();
        public Expression<Func<T, object>> OrderBy { get; private set; }
        public Expression<Func<T, object>> OrderByDesc { get; private set; }
        public int Take { get; private set; }
        public int Skip { get; private set; }
        public bool IsPagingEnabled { get; set; }

        // Apply methods
        protected void ApplySorting(string propertyName, bool ascending = true)
        {
            if (string.IsNullOrEmpty(propertyName)) return;

            ValidateProperty(propertyName);

            if (ascending)
                AddOrderBy(propertyName);
            else
                AddOrderByDesc(propertyName);
        }

        protected void ApplyIncludes(Expression<Func<T, object>>[] includes = null)
        {
            if (includes == null) return;

            foreach (var include in includes)
            {
                AddInclude(include);
            }
        }

        protected void ApplyThenIncludes(Func<IQueryable<T>, IQueryable<T>>[] thenIncludes = null)
        {
            if (thenIncludes == null) return;

            foreach (var include in thenIncludes)
            {
                AddThenInclude(include);
            }
        }

        protected void ApplyPaging(int limit, int page)
        {
            if (limit > 0 && page > 0)
            {
                Take = limit;
                Skip = (page - 1) * limit; // Ensure Skip is zero-based.
                IsPagingEnabled = true;
            }
        }

        // Add methods
        protected virtual void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }

        protected virtual void AddThenInclude(Func<IQueryable<T>, IQueryable<T>> thenIncludeExpression)
        {
            ThenIncludes.Add(thenIncludeExpression);
        }
        protected virtual void AddOrderBy(string propertyName)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, propertyName);
            var orderByExpression = Expression.Lambda<Func<T, object>>(Expression.Convert(property, typeof(object)), parameter);
            OrderBy = orderByExpression;
        }

        protected virtual void AddOrderByDesc(string propertyName)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, propertyName);
            var orderByDescExpression = Expression.Lambda<Func<T, object>>(Expression.Convert(property, typeof(object)), parameter);
            OrderByDesc = orderByDescExpression;
        }

        protected void ValidateProperty(string propertyName)
        {
            var propertyInfo = typeof(T).GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (propertyInfo == null)
            {
                throw new ArgumentException($"Property '{propertyName}' does not exist on type '{typeof(T).Name}'");
            }
        }
    }
}
