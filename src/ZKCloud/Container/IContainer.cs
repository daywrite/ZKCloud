using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZKCloud.Container {
    public interface IContainer : IDisposable {
        /// <summary>
        /// 注册对象
        /// </summary>
        /// <typeparam name="T">注册对象类型</typeparam>
        /// <param name="type">注册方式</param>
        void Register<T>(RegisterType type = RegisterType.PerCall);

        /// <summary>
        /// 注册对象
        /// </summary>
        /// <typeparam name="T">注册对象类型</typeparam>
        /// <param name="creator">对象构造器</param>
        /// <param name="type">注册方式</param>
        void Register<T>(Func<T> creator, RegisterType type = RegisterType.PerCall);

        /// <summary>
        /// 注册对象
        /// </summary>
        /// <typeparam name="TService">注册对象类型</typeparam>
        /// <typeparam name="TImplementation">注册对象实现类型</typeparam>
        /// <param name="type">注册方式</param>
        void Register<TService, TImplementation>(RegisterType type = RegisterType.PerCall)
            where TImplementation : TService;

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <returns>对象值</returns>
        T Resolve<T>();

        IEnumerable<T> ResolveAll<T>();

        IEnumerable<T> ResolveAll<T>(RegisterType type);
    }
}
