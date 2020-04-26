using System;
using System.Linq.Expressions;

namespace ReusableGenericRepository
{
  public static class Utilities
  {
    public static Expression<Func<TEntity, bool>> BuildLambdaForFindByKey<TEntity>(int id) {
      // type that is quering
      var item = Expression.Parameter(typeof(TEntity), "entity");
      // property of that entity, 'exampleId'
      var prop = Expression.Property(item, typeof(TEntity).Name + "Id");
      // variable to compare with
      var value = Expression.Constant(id);
      // here value and property are compared
      var equal = Expression.Equal(prop, value);
      // create lambda
      var lambda = Expression.Lambda<Func<TEntity, bool>>(equal, item);
      return lambda;
    }
  }
}