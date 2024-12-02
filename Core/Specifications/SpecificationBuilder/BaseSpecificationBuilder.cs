using Core.Entities;
using Core.Helper;
using Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications.SpecificationBuilder
{
    public abstract class BaseSpecificationBuilder<T , TDerived> 
        where T:class, IBaseEntity
        where TDerived : BaseSpecificationBuilder<T, TDerived>
    {
        protected Expression<Func<T, bool>> _criteria = PredicateBuilder.New<T>(true); // Always true by default
        protected readonly List<Expression<Func<T, object>>> _includes = new List<Expression<Func<T, object>>>();
        protected readonly List<Func<IQueryable<T>, IQueryable<T>>> _thenIncludes = new List<Func<IQueryable<T>, IQueryable<T>>>();
        protected string _orderBy;
        protected bool _ascending = true;
        protected int _limit = 0;
        protected int _page = 0;


        public virtual TDerived WithId(int id)
        {
            _criteria = PredicateBuilder.And(_criteria, x=> x.Id == id);
            return (TDerived)this;
        }

        public virtual TDerived WithOrderBy(string? orderBy, bool ascending = true)
        {

            if (!string.IsNullOrEmpty(orderBy))
            {
                _orderBy = orderBy;
                _ascending = ascending;
            }
            return (TDerived)this;
        }

        public virtual TDerived WithLimit(int? limit)
        {
            if (limit != null)
            {
                _limit = limit.Value;
            };
            return (TDerived)this;
        }

        public virtual TDerived WithPage(int? page)
        {
            if (page != null)
            {
                _page = page.Value;
            };
            return (TDerived)this;
        }

        public virtual TDerived Include(Expression<Func<T, object>> include)
        {
           
            _includes.Add(include);
            
            return (TDerived)this;
        }

        public virtual TDerived ThenInclude(Func<IQueryable<T>, IQueryable<T>> include)
        {
            
              _thenIncludes.Add(include);
            return (TDerived)this;
        }

        public virtual BaseSpecifications<T> Build()
        {
            return new BaseSpecifications<T>(
                _criteria,
                _includes,
                _thenIncludes,
                _orderBy,
                _ascending,
                _limit,
                _page
            );
        }

    }
}
