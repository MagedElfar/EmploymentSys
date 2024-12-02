using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class SpecificationsEvaluate<TEntity> where TEntity :class,IBaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> query , ISpecifications<TEntity> specifications) {
            var newQuery = query;

            if (specifications.Criteria is not null)
            {
                newQuery = newQuery.Where(specifications.Criteria);
            }

            if (specifications.OrderBy is not null) {
                newQuery = newQuery.OrderBy(specifications.OrderBy);
            } else if (specifications.OrderByDesc is not null)
            {
                newQuery = newQuery.OrderByDescending(specifications.OrderByDesc);
            }

            if (specifications.IsPagingEnabled == true) {
                newQuery = newQuery.Skip(specifications.Skip)
                    .Take(specifications.Take);
            }

            foreach (var include in specifications.Includes)
            {
                newQuery = newQuery.Include(include);
            }

            //foreach (var include in specifications.ThenIncludes)
            //{
            //    newQuery = newQuery.Include(include);
            //}
            return newQuery;
        }
    }
}
