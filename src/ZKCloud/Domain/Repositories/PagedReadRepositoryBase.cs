using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ZKCloud.Domain.Models;

namespace ZKCloud.Domain.Repositories {
    public abstract class PagedReadRepositoryBase<T> : ReadRepositoryBase<T>, IPagedReadRepository<T>
        where T : class, IEntity {
        public PagedList<T> ReadMany(Expression<Func<T, bool>> predicate, int pageSize, int pageIndex) {
            if (pageSize < 1)
                pageSize = 1;
            if (pageIndex < 1)
                pageIndex = 1;
            var source = RepositoryContext.Query<T>()
                .Where(predicate)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            return PagedList<T>.Create(source, Count(predicate), pageSize, pageIndex);
        }

        public PagedList<T> ReadMany(Expression<Func<T, bool>> predicate, int pageSize, int pageIndex, Func<IQueryable<T>, IQueryable<T>> keySelectorCallback) {
            if (pageSize < 1)
                pageSize = 1;
            if (pageIndex < 1)
                pageIndex = 1;
            var query = RepositoryContext.Query<T>()
                .Where(predicate);
            if (keySelectorCallback != null) {
                query = keySelectorCallback(query);
            }
            var source = query.Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            return PagedList<T>.Create(source, Count(predicate), pageSize, pageIndex);
        }
    }
}
