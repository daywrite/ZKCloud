using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc.Infrastructure;

namespace ZKCloud.Web.Mvc {
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class AppAttribute : RouteConstraintAttribute {
        public AppAttribute(string appName)
            : base("app", appName, blockNonAttributedActions: true) {
            if (string.IsNullOrEmpty(appName)) {
                throw new ArgumentException("app name must not be empty", nameof(appName));
            }
        }
    }
}
