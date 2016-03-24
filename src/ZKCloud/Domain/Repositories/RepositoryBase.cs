using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZKCloud.Container;
using ZKCloud.Domain.Models;

namespace ZKCloud.Domain.Repositories {
    public abstract class RepositoryBase<T> : IRepository
        where T : class, IEntity {
        public IRepositoryContext RepositoryContext {
            get {
                return ContainerManager.Default.Resolve<IRepositoryContext>();
            }
        }
    }
}
