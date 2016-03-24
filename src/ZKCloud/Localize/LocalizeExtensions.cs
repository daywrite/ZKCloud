using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.Extensions.PlatformAbstractions;
using ZKCloud.Container;
using ZKCloud.Extensions;

namespace ZKCloud.Localize {
	public static class LocalizeExtensions {
		public static IApplicationBuilder RegisterAllTranslateProviders(this IApplicationBuilder app) {
			PlatformServices.Default.LibraryManager.GetLibraries()
				.SelectMany(e => e.Assemblies)
				.Where(e => e.Name.StartsWith("ZKCloud"))
				.Distinct()
				.Select(e => Assembly.Load(e))
				.SelectMany(e => e.GetTypes())
				.Where(e => e.GetTypeInfo().IsClass && !e.GetTypeInfo().IsAbstract && !e.GetTypeInfo().IsGenericType && typeof(ITranslateProvider).IsAssignableFrom(e))
				.Select(e => Tuple.Create(e, e.GetInterfaces().FirstOrDefault()))
				.Where(e => e.Item2 != null)
				.ToArray()
				.Foreach(e =>
				{
					e.Item2.CreateContainerRegisterAction(e.Item1)();
				});
			return app;
		}
	}
}

