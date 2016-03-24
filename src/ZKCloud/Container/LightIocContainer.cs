using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Reflection;
using System.Threading;
using ZKCloud.Extensions;

namespace ZKCloud.Container {
    /// <summary>
    /// 基于对象缓存的轻量级Ioc容器
    /// </summary>
    public class LightIocContainer : IContainer {
        private class LoaderCacheBase {
            public static IDictionary<Type, HashSet<RegisterType>> TypeCache { get; } = new Dictionary<Type, HashSet<RegisterType>>();

            public static bool ContainsType(Type type, RegisterType registerType) {
                HashSet<RegisterType> find = null;
                if (TypeCache.TryGetValue(type, out find)) {
                    return find.Contains(registerType);
                }
                return false;
            }

            public static void AddType(Type type, RegisterType registerType) {
                HashSet<RegisterType> find = null;
                if (TypeCache.TryGetValue(type, out find)) {
                    if (!find.Contains(registerType))
                        find.Add(registerType);
                } else {
                    TypeCache.Add(type, new HashSet<RegisterType>() { registerType });
                }
            }
        }

        private sealed class PerCallLoaderCache<T> : LoaderCacheBase {
            public static IList<Func<T>> Instanse { get; } = new List<Func<T>>();

            static PerCallLoaderCache() {
                AddType(typeof(T), RegisterType.PerCall);
            }

            public static void AddRegister(Func<T> creator) {
                Instanse.Add(creator);
            }

            public static void Clear() {
                Instanse.Clear();
            }
        }

        private sealed class SingletonLoaderCache<T> : LoaderCacheBase {
            public static IList<T> Instanse { get; } = new List<T>();

            static SingletonLoaderCache() {
                AddType(typeof(T), RegisterType.Singleton);
            }

            public static void AddRegister(T data) {
                Instanse.Add(data);
            }

            public static void Clear() {
                foreach (var item in Instanse) {
                    if (item is IDisposable)
                        (item as IDisposable).Dispose();
                }
                Instanse.Clear();
            }
        }

        private sealed class LazySingletonLoaderCache<T> : LoaderCacheBase {
            public static IList<Lazy<T>> Instanse { get; } = new List<Lazy<T>>();

            static LazySingletonLoaderCache() {
                AddType(typeof(T), RegisterType.LazySingleton);
            }

            public static void AddRegister(Func<T> creator) {
                Instanse.Add(new Lazy<T>(creator, true));
            }

            public static void Clear() {
                foreach (var item in Instanse) {
                    if (item.IsValueCreated && item.Value is IDisposable)
                        (item.Value as IDisposable).Dispose();
                }
                Instanse.Clear();
            }
        }

        private sealed class PerThreadLoaderCache<T> : LoaderCacheBase {
            public static IList<ThreadLocal<T>> Instanse { get; } = new List<ThreadLocal<T>>();

            static PerThreadLoaderCache() {
                AddType(typeof(T), RegisterType.PerThread);
            }

            public static void AddRegister(Func<T> creator) {
                Instanse.Add(new ThreadLocal<T>(creator));
            }

            public static void Clear() {
                foreach (var item in Instanse) {
                    item.Dispose();
                }
                Instanse.Clear();
            }
        }

        private class PerHttpRequestLoaderCacheBase : LoaderCacheBase {
            protected static Func<ConcurrentDictionary<Type, IList<object>>> _cacheFromHttpFunc = null;

            public static void SetCacheFromFunc(Func<ConcurrentDictionary<Type, IList<object>>> func) {
                _cacheFromHttpFunc = func;
            }

            public static void DisposeCurrentRequestCache() {
                var cache = _cacheFromHttpFunc();
                foreach (var listItem in cache) {
                    foreach (var item in listItem.Value) {
                        if (item is IDisposable)
                            (item as IDisposable).Dispose();
                    }
                }
                cache.Clear();
            }
        }

        private sealed class PerHttpRequestLoaderCache<T> : PerHttpRequestLoaderCacheBase {
            public static IList<Func<T>> Instanse { get; } = new List<Func<T>>();

            static PerHttpRequestLoaderCache() {
                AddType(typeof(T), RegisterType.PerHttpRequest);
            }

            public static void AddRegister(Func<T> creator) {
                Instanse.Add(creator);
            }

            public static IEnumerable<T> GetValues() {
                var cache = _cacheFromHttpFunc();
                IList<object> find = cache.GetOrAdd(typeof(T), t => Instanse.Select(e => e() as object).ToList());
                return find.OfType<T>().ToArray();
            }

            public static void Clear() {
                Instanse.Clear();
            }
        }

        public void Register<T>(RegisterType type = RegisterType.Singleton) {
            switch (type) {
                case RegisterType.LazySingleton:
                    LazySingletonLoaderCache<T>.AddRegister(CreateCreatorFactory<T>());
                    break;
                case RegisterType.PerCall:
                    PerCallLoaderCache<T>.AddRegister(CreateCreatorFactory<T>());
                    break;
                case RegisterType.PerHttpRequest:
                    PerHttpRequestLoaderCache<T>.AddRegister(CreateCreatorFactory<T>());
                    break;
                case RegisterType.PerThread:
                    PerThreadLoaderCache<T>.AddRegister(CreateCreatorFactory<T>());
                    break;
                case RegisterType.Singleton:
                default:
                    SingletonLoaderCache<T>.AddRegister(CreateCreatorFactory<T>()());
                    break;
            }
        }

        public void Register<T>(Func<T> creator, RegisterType type = RegisterType.Singleton) {
            switch (type) {
                case RegisterType.LazySingleton:
                    LazySingletonLoaderCache<T>.AddRegister(creator);
                    break;
                case RegisterType.PerCall:
                    PerCallLoaderCache<T>.AddRegister(creator);
                    break;
                case RegisterType.PerHttpRequest:
                    PerHttpRequestLoaderCache<T>.AddRegister(creator);
                    break;
                case RegisterType.PerThread:
                    PerThreadLoaderCache<T>.AddRegister(creator);
                    break;
                case RegisterType.Singleton:
                default:
                    SingletonLoaderCache<T>.AddRegister(creator());
                    break;
            }
        }

        public void Register<TSerivce, TImplementation>(RegisterType type = RegisterType.Singleton)
            where TImplementation : TSerivce {
            switch (type) {
                case RegisterType.LazySingleton:
                    LazySingletonLoaderCache<TSerivce>.AddRegister(() => CreateCreatorFactory<TImplementation>()());
                    break;
                case RegisterType.PerCall:
                    PerCallLoaderCache<TSerivce>.AddRegister(() => CreateCreatorFactory<TImplementation>()());
                    break;
                case RegisterType.PerHttpRequest:
                    PerHttpRequestLoaderCache<TSerivce>.AddRegister(() => CreateCreatorFactory<TImplementation>()());
                    break;
                case RegisterType.PerThread:
                    PerThreadLoaderCache<TSerivce>.AddRegister(() => CreateCreatorFactory<TImplementation>()());
                    break;
                case RegisterType.Singleton:
                default:
                    SingletonLoaderCache<TSerivce>.AddRegister(CreateCreatorFactory<TImplementation>()());
                    break;
            }
        }

        public T Resolve<T>() {
            HashSet<RegisterType> registerTypes = null;
            if (!LoaderCacheBase.TypeCache.TryGetValue(typeof(T), out registerTypes) || registerTypes.Count <= 0)
                return default(T);
            return Resolve<T>(registerTypes.First());
        }

        public T Resolve<T>(RegisterType type) {
            if (!LoaderCacheBase.ContainsType(typeof(T), type))
                return default(T);
            switch (type) {
                case RegisterType.LazySingleton:
                    return LazySingletonLoaderCache<T>.Instanse.First().Value;
                case RegisterType.PerCall:
                    return PerCallLoaderCache<T>.Instanse.First()();
                case RegisterType.PerHttpRequest:
                    return PerHttpRequestLoaderCache<T>.GetValues().First();
                case RegisterType.PerThread:
                    return PerThreadLoaderCache<T>.Instanse.First().Value;
                case RegisterType.Singleton:
                default:
                    return SingletonLoaderCache<T>.Instanse.First();
            }
        }

        public IEnumerable<T> ResolveAll<T>(RegisterType type) {
            if (!LoaderCacheBase.ContainsType(typeof(T), type))
                return null;
            switch (type) {
                case RegisterType.LazySingleton:
                    return LazySingletonLoaderCache<T>.Instanse.Select(e => e.Value).ToArray();
                case RegisterType.PerCall:
                    return PerCallLoaderCache<T>.Instanse.Select(e => e()).ToArray();
                case RegisterType.PerHttpRequest:
                    return PerHttpRequestLoaderCache<T>.GetValues();
                case RegisterType.PerThread:
                    return PerThreadLoaderCache<T>.Instanse.Select(e => e.Value).ToArray();
                case RegisterType.Singleton:
                default:
                    return SingletonLoaderCache<T>.Instanse.ToArray();
            }
        }

        public IEnumerable<T> ResolveAll<T>() {
            IList<T> list = new List<T>();
            list.AddRange(ResolveAll<T>(RegisterType.LazySingleton));
            list.AddRange(ResolveAll<T>(RegisterType.PerCall));
            list.AddRange(ResolveAll<T>(RegisterType.PerHttpRequest));
            list.AddRange(ResolveAll<T>(RegisterType.PerThread));
            list.AddRange(ResolveAll<T>(RegisterType.Singleton));
            return list.ToArray();
        }

        private Func<T> CreateCreatorFactory<T>() {
            var newExpression = Expression.New(typeof(T));
            //var convertExpression = Expression.Convert(newExpression, typeof(object));
            var lambdaExpression = Expression.Lambda<Func<T>>(newExpression);
            return lambdaExpression.Compile();
        }

        private Action CreateClearAction(Type cacheType, Type dataType) {
            Type fullCacheType = cacheType.MakeGenericType(new[] { dataType });
            var method = fullCacheType.GetMethod("Clear", BindingFlags.Static | BindingFlags.Public);
            var clearExpression = Expression.Call(null, method);
            var lambdaExpression = Expression.Lambda<Action>(clearExpression);
            return lambdaExpression.Compile();
        }

        private Action CreateClearAction(RegisterType registerType, Type dataType) {
            switch (registerType) {
                case RegisterType.LazySingleton:
                    return CreateClearAction(typeof(LazySingletonLoaderCache<>), dataType);
                case RegisterType.PerCall:
                    return CreateClearAction(typeof(PerCallLoaderCache<>), dataType);
                case RegisterType.PerHttpRequest:
                    return CreateClearAction(typeof(PerHttpRequestLoaderCache<>), dataType);
                case RegisterType.PerThread:
                    return CreateClearAction(typeof(PerThreadLoaderCache<>), dataType);
                case RegisterType.Singleton:
                default:
                    return CreateClearAction(typeof(SingletonLoaderCache<>), dataType);
            }
        }

        public void Dispose() {
            foreach (var typeItem in LoaderCacheBase.TypeCache) {
                foreach (var registerType in typeItem.Value) {
                    CreateClearAction(registerType, typeItem.Key)();
                }
            }
        }

        public static void SetPerHttpRequestCacheFunc(Func<ConcurrentDictionary<Type, IList<object>>> func) {
            PerHttpRequestLoaderCacheBase.SetCacheFromFunc(func);
        }

        public static void DisposeCurrentRequestCache() {
            PerHttpRequestLoaderCacheBase.DisposeCurrentRequestCache();
        }
    }
}
