using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc.Controllers;
using Microsoft.AspNet.Mvc.Infrastructure;
using ZKCloud.Web.Mvc.Dynamic;

namespace ZKCloud.Web.Mvc {
    public class DynamicControllerTypeProvider : DefaultControllerTypeProvider {
        /// <summary>
        /// Initializes a new instance of <see cref="DefaultControllerTypeProvider"/>.
        /// </summary>
        /// <param name="assemblyProvider"><see cref="IAssemblyProvider"/> that provides assemblies to look for
        /// controllers in.</param>
        public DynamicControllerTypeProvider(IAssemblyProvider assemblyProvider)
            : base(assemblyProvider) {
        }

        public override IEnumerable<TypeInfo> ControllerTypes {
            get {
                var list = new List<TypeInfo>(base.ControllerTypes);
                DynamicApiControllerBuilder.AddDynamicApiControllerTypes(list);
                return list;
            }
        }
    }
}
