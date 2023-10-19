using System.Linq.Expressions;
using SkinetV2.Application.common.Interfaces;

namespace SkinetV2.Application.common.Specifications
{
    public class BaseSpecification<T> : ISpecification<T>
    {
        public BaseSpecification()
        {
        }

        public BaseSpecification(Expression<Func<T, bool>> createria)
        {
            Createria = createria;
        }

        public Expression<Func<T, bool>> Createria { get; }

        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();

        protected void AddIncludes(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }
    }
}