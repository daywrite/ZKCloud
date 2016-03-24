using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZKCloud.Container {
    public static class ContainerManager {
        public static IContainer Default { get; } = new LightIocContainer();
    }
}
