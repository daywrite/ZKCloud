using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.Extensions.PlatformAbstractions;
using ZKCloud.Container;

namespace ZKCloud.Domain.Repositories {
    public static class RepositoryExtensions {
        public static IApplicationBuilder RegisterAllRepositories(this IApplicationBuilder app) {
            var repositoryTypes = PlatformServices.Default.LibraryManager.GetLibraries()
                .SelectMany(e => e.Assemblies)
                .Where(e => e.Name.StartsWith("ZKCloud"))
                .Distinct()
                .Select(e => Assembly.Load(e))
                .SelectMany(e => e.GetTypes())
                .Where(e => e.GetTypeInfo().IsClass && !e.GetTypeInfo().IsAbstract && !e.GetTypeInfo().IsGenericType && typeof(IRepository).IsAssignableFrom(e))
                .ToArray();
            foreach (var type in repositoryTypes) {
                type.CreateContainerRegisterAction(type)();
            }
            return app;
        }

        public static IApplicationBuilder RegisterAllModelCreators(this IApplicationBuilder app) {
            Type serviceType = typeof(IModelCreator);
            var creatorTypes = PlatformServices.Default.LibraryManager.GetLibraries()
                .SelectMany(e => e.Assemblies)
                .Where(e => e.Name.StartsWith("ZKCloud"))
                .Distinct()
                .Select(e => Assembly.Load(e))
                .SelectMany(e => e.GetTypes())
                .Where(e => e.GetTypeInfo().IsClass && !e.GetTypeInfo().IsAbstract && !e.GetTypeInfo().IsGenericType && serviceType.IsAssignableFrom(e))
                .ToArray();
            foreach (var type in creatorTypes) {
                serviceType.CreateContainerRegisterAction(type)();
            }
            return app;
        }
    }
}
