using System.Linq.Expressions;

namespace SkinetV2.Application.common.Interfaces
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Createria { get; }
        List<Expression<Func<T, object>>> Includes { get; }
    }
}