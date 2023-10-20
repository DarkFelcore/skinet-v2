using System.Linq.Expressions;

namespace SkinetV2.Application.common.Interfaces
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Createria { get; }
        List<Expression<Func<T, object>>> Includes { get; }
        Expression<Func<T, object>> OrderBy { get; }
        Expression<Func<T, object>> OrderByDescending { get; }
        int Take { get; }
        int Skip { get; }
        bool IsPagingEnabled { get; }
    }
}