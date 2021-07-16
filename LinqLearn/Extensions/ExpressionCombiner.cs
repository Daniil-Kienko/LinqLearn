using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LinqLearn.Extensions {
    public static class ExpressionCombiner {
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> exp, Expression<Func<T, bool>> newExp) {
            var visitor = new ParameterUpdateVisitor(newExp.Parameters.First(), exp.Parameters.First());
            newExp = visitor.Visit(newExp) as Expression<Func<T, bool>>;

            var binExp = Expression.And(exp.Body, newExp.Body);
            return Expression.Lambda<Func<T, bool>>(binExp, newExp.Parameters);
        }
    }
}
