using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ZKCloud.Domain.Models;

namespace ZKCloud.Domain.Repositories {
	public abstract class ReadRepositoryBase<T> : RepositoryBase<T>, IReadRepository<T>
		where T : class, IEntity {
		public long Count() {
			return RepositoryContext.Count<T>();
		}

		public long Count(Expression<Func<T, bool>> predicate) {
			return RepositoryContext.Count(predicate);
		}

		public IEnumerable<T> ReadMany(Expression<Func<T, bool>> predicate) {
			return RepositoryContext.Query<T>().Where(predicate).ToList();
		}

		public T ReadSingle(Expression<Func<T, bool>> predicate) {
			return RepositoryContext.Get(predicate);
		}
	}
}
