using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Builder;

namespace ZKCloud.Container {
    public static class ContainerExtensions {
        private static readonly string _lightIocPerHttpRequestCacheKey = "lightioc_perhttprequest_cache";

        [ThreadStatic]
        private static HttpContext _currentHttpContext;

        public static IApplicationBuilder UsePerHttpRequestContainer(this IApplicationBuilder app) {
            Func<HttpContext, Func<Task>, Task> middleware = (context, func) => {
                _currentHttpContext = context;
                _currentHttpContext.Items.Add(_lightIocPerHttpRequestCacheKey, new ConcurrentDictionary<Type, IList<object>>());
                LightIocContainer.SetPerHttpRequestCacheFunc(() => {
                    return _currentHttpContext.Items[_lightIocPerHttpRequestCacheKey] as ConcurrentDictionary<Type, IList<object>>;
                });
                var task = func();
                LightIocContainer.DisposeCurrentRequestCache();
                context.Items.Remove(_lightIocPerHttpRequestCacheKey);
                return task;
            };
            return app.Use(middleware);
        }

        public static Action CreateContainerRegisterAction(this Type type, RegisterType registerType = RegisterType.Singleton) {
            var instanseExpression = Expression.Constant(ContainerManager.Default);
            var method = ContainerManager.Default.GetType()
                .GetMethods()
                .Where(e => e.IsGenericMethod && e.GetGenericArguments().Length == 1 && e.GetParameters().Length == 1)
                .FirstOrDefault()
                .MakeGenericMethod(type);
            var registerTypeExpression = Expression.Constant(registerType);
            var callExpression = Expression.Call(instanseExpression, method, registerTypeExpression);
            var lambaExpression = Expression.Lambda<Action>(callExpression);
            return lambaExpression.Compile();
        }

        public static Action CreateContainerRegisterAction(this Type serviceType, Type implementationType, RegisterType registerType = RegisterType.Singleton) {
            var instanseExpression = Expression.Constant(ContainerManager.Default);
            var method = ContainerManager.Default.GetType()
                .GetMethods()
                .Where(e => e.IsGenericMethod && e.GetGenericArguments().Length == 2 && e.GetParameters().Length == 1)
                .FirstOrDefault()
                .MakeGenericMethod(serviceType, implementationType);
            var registerTypeExpression = Expression.Constant(registerType);
            var callExpression = Expression.Call(instanseExpression, method, registerTypeExpression);
            var lambaExpression = Expression.Lambda<Action>(callExpression);
            return lambaExpression.Compile();
        }
    }
}
