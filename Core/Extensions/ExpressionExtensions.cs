using Core.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public static class ExpressionExtensions
    {
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            var parameter = Expression.Parameter(typeof(T));
            var combined = new ReplaceParameterVisitor
            {
                OldParameter = expr2.Parameters[0],
                NewParameter = parameter
            }.Visit(expr2.Body);
            var body = Expression.AndAlso(expr1.Body, combined!);
            return Expression.Lambda<Func<T, bool>>(body, parameter);
        }
    }

}
