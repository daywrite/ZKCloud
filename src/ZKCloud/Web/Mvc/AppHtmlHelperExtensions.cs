using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Html.Abstractions;
using Microsoft.AspNet.Mvc.Rendering;

namespace ZKCloud.Web.Mvc
{
    public static class AppHtmlHelperExtensions {
        public static IHtmlContent T(this IHtmlHelper html, string input) {
            return new LocalizedHtmlString(input);
        }
    }
}
