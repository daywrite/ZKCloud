using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ZKCloud.Domain.Models;

namespace ZKCloud.Domain.Repositories {
	public interface IPagedReadRepository<T> : IRepository
		where T : class, IEntity {
		PagedList<T> ReadMany(Expression<Func<T, bool>> predicate, int pageSize, int pageIndex);

		PagedList<T> ReadMany(Expression<Func<T, bool>> predicate, int pageSize, int pageIndex, Func<IQueryable<T>, IQueryable<T>> keySelectorCallback);
	}
}
