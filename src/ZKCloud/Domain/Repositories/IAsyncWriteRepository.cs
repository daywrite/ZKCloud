using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ZKCloud.Domain.Models;

namespace ZKCloud.Domain.Repositories {
    public interface IAsyncWriteRepository<T> : IWriteRepository<T>
        where T : class, IEntity {
        Task AddSingleAsync(T entity);

        Task AddManyAsync(IEnumerable<T> source);

        Task UpdateSingleAsync(T entity);

        Task UpdateManyAsync(Expression<Func<T, bool>> predicate, Action<T> updateAction);

        Task DeleteAsync(Expression<Func<T, bool>> predicate);
    }
}
