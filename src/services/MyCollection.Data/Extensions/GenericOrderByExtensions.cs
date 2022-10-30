using System.Globalization;
using System.Linq.Expressions;

namespace MyCollection.Data.Extensions
{
    public static class GenericOrderByExtensions
    {
        public static IQueryable<TEntity> GenericOrderBy<TEntity>(
           this IQueryable<TEntity> source,
           string property,
           bool desc
       )
        {
            var type = typeof(TEntity);
            string command = desc ? "OrderByDescending" : "OrderBy";

            var propertyQuery = type.GetProperty(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(property));

            if (propertyQuery == null)
                throw new Exception($"Propriedade {property} inválida!");

            var parameter = Expression.Parameter(type, "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, propertyQuery);
            var orderByExpression = Expression.Lambda(propertyAccess, parameter);
            var resultExpression = Expression.Call(
                typeof(Queryable),
                command,
                new Type[] { type, propertyQuery.PropertyType },
                source.Expression,
                Expression.Quote(orderByExpression)
            );

            return source.Provider.CreateQuery<TEntity>(resultExpression);
        }
    }
}
