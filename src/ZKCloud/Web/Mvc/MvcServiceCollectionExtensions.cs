using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Razor;
using Microsoft.AspNet.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;

namespace ZKCloud.Web.Mvc {
    public static class MvcServiceCollectionExtensions {
        public static IServiceCollection AddAppRazorViewEngine(this IServiceCollection services) {
            //var find = services.FirstOrDefault(e => e.ServiceType == typeof(IRazorViewEngine));
            //services.Remove(find);
            services.AddTransient<IRazorViewEngine, AppRazorViewEngine>();
            return services;
        }

        public static IServiceCollection AddAppUrlHelper(this IServiceCollection services) {
            //var find = services.FirstOrDefault(e => e.ServiceType == typeof(IUrlHelper));
            //services.Remove(find);
            services.AddTransient<IUrlHelper, AppUrlHelper>();
            return services;
        }

        public static IServiceCollection AddDynamicControllerTypeProvider(this IServiceCollection services) {
            //var find = services.FirstOrDefault(e => e.ServiceType == typeof(IControllerTypeProvider));
            //services.Remove(find);
            services.AddTransient<IControllerTypeProvider, DynamicControllerTypeProvider>();
            return services;
        }
    }
}
