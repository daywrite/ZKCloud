using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZKCloud.Container;
using ZKCloud.Domain.Repositories;

namespace ZKCloud.Domain.Services {
    public abstract class ServiceBase : IService {
        public T Repository<T>() where T : IRepository {
            return ContainerManager.Default.Resolve<T>();
        }

        public T Service<T>() where T : IService {
            return ContainerManager.Default.Resolve<T>();
        }
    }
}
