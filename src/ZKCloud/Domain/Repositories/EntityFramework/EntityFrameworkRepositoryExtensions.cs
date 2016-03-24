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
using EF = Microsoft.Data.Entity;

namespace ZKCloud.Domain.Repositories.EntityFramework {
    public static class EntityFrameworkRepositoryExtensions {
        public static IApplicationBuilder RegisterEntityFrameworkRepositoryContext(this IApplicationBuilder app) {
            Func<IRepositoryContext> createFunc = () => new EntityFrameworkRepositoryContext(RuntimeContext.Current.WebsiteConfig.Database,
                RuntimeContext.Current.WebsiteConfig.ConnectionString);
            ContainerManager.Default.Register(createFunc, RegisterType.PerHttpRequest);
            return app;
        }
    }
}
