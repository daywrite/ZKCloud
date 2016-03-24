using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ZKCloud.Domain.Models;

namespace ZKCloud.Domain.Repositories {
    public interface IAsyncPagedReadRepository<T> : IPagedReadRepository<T>
        where T : class, IEntity {
        Task<PagedList<T>> ReadManyAsync(Expression<Func<T, bool>> predicate, int pageSize, int pageIndex);

        Task<PagedList<T>> ReadManyAsync(Expression<Func<T, bool>> predicate, int pageSize, int pageIndex, Func<IQueryable<T>, IQueryable<T>> keySelectorCallback);

    }
}
