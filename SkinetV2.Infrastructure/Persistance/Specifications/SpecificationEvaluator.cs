using Microsoft.EntityFrameworkCore;
using SkinetV2.Application.common.Interfaces;

namespace SkinetV2.Infrastructure.Persistance.Specifications
{
    public class SpecificationEvaluator<TEntity>
        where TEntity : class
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> spec)
        {
            var query = inputQuery;

            if (spec.Createria is not null)
            {
                query = query.Where(spec.Createria);
            }

            // Chaining includes
            query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));

            return query;
        }
    }
}