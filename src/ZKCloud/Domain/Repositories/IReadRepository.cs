using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ZKCloud.Domain.Models;

namespace ZKCloud.Domain.Repositories {
	public interface IReadRepository<T> : IRepository
		where T : class, IEntity {
		long Count();

		long Count(Expression<Func<T, bool>> predicate);

		T ReadSingle(Expression<Func<T, bool>> predicate);

		IEnumerable<T> ReadMany(Expression<Func<T, bool>> predicate);
	}
}
