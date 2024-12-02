using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Helper
{
    public class ReplaceParameterVisitor : ExpressionVisitor
    {
        public ParameterExpression OldParameter { get; set; }
        public ParameterExpression NewParameter { get; set; }
        protected override Expression VisitParameter(ParameterExpression node) => node == OldParameter ? NewParameter : base.VisitParameter(node);
    }
}
