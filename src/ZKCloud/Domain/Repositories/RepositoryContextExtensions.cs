using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.PlatformAbstractions;
using ZKCloud.Extensions;
using ZKCloud.Container;
using ZKCloud.Runtime;
using ZKCloud.Runtime.Config;
using Microsoft.AspNet.Builder;

namespace ZKCloud.Domain.Repositories {
    public static class RepositoryContextExtensions {
        /// <summary>
		/// 从数据库中获取满足条件的单个对象，找不到时返回null
		/// </summary>
        /// <param name="context">存储上下文对象</param>
		/// <typeparam name="T">数据类型</typeparam>
		/// <param name="expression">表达式</param>
		/// <returns></returns>
		public static T Get<T>(this IRepositoryContext context, Expression<Func<T, bool>> expression)
            where T : class {
            return context.Query<T>().FirstOrDefault(expression);
        }

        /// <summary>
		/// 获取满足条件的对象数量
		/// </summary>
		/// <typeparam name="T">数据类型</typeparam>
        /// <param name="context">存储上下文对象</param>
		/// <returns></returns>
		public static long Count<T>(this IRepositoryContext context)
            where T : class {
            return context.Query<T>().LongCount();
        }

        /// <summary>
		/// 获取满足条件的对象数量
		/// </summary>
		/// <typeparam name="T">数据类型</typeparam>
        /// <param name="context">存储上下文对象</param>
		/// <param name="expression">表达式</param>
		/// <returns></returns>
		public static long Count<T>(this IRepositoryContext context, Expression<Func<T, bool>> expression)
            where T : class {
            return context.Query<T>().LongCount(expression);
        }

        /// <summary>
		/// 批量更新
		/// 返回更新的数量
		/// </summary>
		/// <typeparam name="T">数据类型</typeparam>
        /// <param name="context">存储上下文对象</param>
		/// <param name="expression">更新条件</param>
		/// <param name="update">更新函数</param>
		public static long UpdateWhere<T>(this IRepositoryContext context, Expression<Func<T, bool>> expression, Action<T> update)
            where T : class {
            long count = 0;
            context.Query<T>().Where(expression).Foreach(d => { update(d); context.Update(d); count++; });
            return count;
        }

        /// <summary>
		/// 批量删除
		/// 返回删除的数量
		/// </summary>
		/// <typeparam name="T">数据类型</typeparam>
        /// <param name="context">存储上下文对象</param>
		/// <param name="expression">删除条件</param>
		public static long DeleteWhere<T>(this IRepositoryContext context, Expression<Func<T, bool>> expression)
            where T : class {
            long count = 0;
            context.Query<T>().Where(expression).Foreach(d => { context.Delete(d); ++count; });
            return count;
        }
    }
}
