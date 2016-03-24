using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.Extensions.PlatformAbstractions;
using ZKCloud.Container;
using ZKCloud.Extensions;

namespace ZKCloud.Domain.Services {
    public static class ServiceExtensions {
        public static IApplicationBuilder RegisterAllServices(this IApplicationBuilder app) {
            PlatformServices.Default.LibraryManager.GetLibraries()
                .SelectMany(e => e.Assemblies)
                .Where(e => e.Name.StartsWith("ZKCloud"))
                .Distinct()
                .Select(e => Assembly.Load(e))
                .SelectMany(e => e.GetTypes())
                .Where(e => e.GetTypeInfo().IsClass && !e.GetTypeInfo().IsAbstract && !e.GetTypeInfo().IsGenericType && typeof(IService).IsAssignableFrom(e))
                .Select(e => Tuple.Create(e, e.GetInterfaces().Where(i => i.Name.Substring(1) == e.Name).FirstOrDefault()))
                .Where(e => e.Item2 != null)
                .ToArray()
                .Foreach(e => {
                    e.Item2.CreateContainerRegisterAction(e.Item1)();
                });
            return app;
        }
    }
}
