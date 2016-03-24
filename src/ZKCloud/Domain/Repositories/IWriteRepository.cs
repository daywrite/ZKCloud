using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ZKCloud.Domain.Models;

namespace ZKCloud.Domain.Repositories {
    public interface IWriteRepository<T> : IRepository
        where T : class, IEntity {
        void AddSingle(T entity);

        void AddMany(IEnumerable<T> source);

        void UpdateSingle(T entity);

        void UpdateMany(Expression<Func<T, bool>> predicate, Action<T> updateAction);

        void Delete(Expression<Func<T, bool>> predicate);
    }
}
