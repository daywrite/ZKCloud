using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ZKCloud.Domain.Models;

namespace ZKCloud.Domain.Repositories {
    public abstract class ReadWriteRepositoryBase<T> : PagedReadRepositoryBase<T>, IWriteRepository<T>
        where T : class, IEntity {
        public void AddMany(IEnumerable<T> source) {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            foreach (var item in source) {
                RepositoryContext.Add(item);
            }
            RepositoryContext.SaveChanges();
        }

		/// <summary>
		/// 添加单个实体数据
		/// </summary>
		/// <param name="entity">实体对象</param>
        public void AddSingle(T entity) {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            RepositoryContext.Add(entity);
            RepositoryContext.SaveChanges();
        }

        public void Delete(Expression<Func<T, bool>> predicate) {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));
            RepositoryContext.DeleteWhere(predicate);
        }

        public void UpdateMany(Expression<Func<T, bool>> predicate, Action<T> updateAction) {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));
            if (updateAction == null)
                throw new ArgumentNullException(nameof(updateAction));
            RepositoryContext.UpdateWhere(predicate, updateAction);
        }

        public void UpdateSingle(T entity) {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            RepositoryContext.Update(entity);
        }
    }
}
