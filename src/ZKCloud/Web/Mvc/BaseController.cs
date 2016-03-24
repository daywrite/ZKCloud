using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZKCloud.Container;
using Microsoft.AspNet.Mvc;

namespace ZKCloud.Web.Mvc {
    public abstract class BaseController : Controller {
        protected BaseController() { }

        protected T Resolve<T>() {
            return ContainerManager.Default.Resolve<T>();
        }

        protected IEnumerable<T> ResolveAll<T>() {
            return ContainerManager.Default.ResolveAll<T>();
        }
    }
}
