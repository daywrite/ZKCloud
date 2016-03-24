using System;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Routing;
using Microsoft.AspNet.Mvc.Infrastructure;

namespace ZKCloud.Web.Mvc {
	public class AppUrlHelper : UrlHelper {
        public AppUrlHelper(IActionContextAccessor actionContextAccessor, IActionSelector actionSelector)
            : base(actionContextAccessor, actionSelector) {
        }

        public string AppName {
            get {
                object result;
                if (!ActionContext.RouteData.Values.TryGetValue("app", out result)) {
                    result = null;
                }
                return Convert.ToString(result);
            }
        }

        public string AppContent(string contentPath,string appName=null) {
            if (string.IsNullOrEmpty(contentPath)) {
                return null;
            } else if (contentPath[0] == '~') {
                var segment = new PathString(contentPath.Substring(1));
				if (appName == null)
					appName = AppName;
				var appPath = new PathString($"/apps/{appName}/");
                var applicationPath = HttpContext.Request.PathBase;
                return applicationPath.Add(appPath).Add(segment).Value;
            }

            return contentPath;
        }
    }
}
