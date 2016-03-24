using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ZKCloud.Domain.Models;

namespace ZKCloud.Domain.Repositories {
    public interface IAsyncReadRepository<T> : IReadRepository<T>
        where T : class, IEntity {
        Task<long> CountAsync();

        Task<long> CountAsync(Expression<Func<T, bool>> predicate);

        Task<T> ReadSingleAsync(Expression<Func<T, bool>> predicate);

        Task<IEnumerable<T>> ReadManyAsync(Expression<Func<T, bool>> predicate);
    }
}
