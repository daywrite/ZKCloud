using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Threading;
using EF = Microsoft.Data.Entity;

namespace ZKCloud.Domain.Repositories {
    public static class AsyncRepositoryExtensions {
        public static async Task<bool> AllAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken)) {
            return await EF.EntityFrameworkQueryableExtensions.AllAsync(source, predicate, cancellationToken);
        }
    }
}
