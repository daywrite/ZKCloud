using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNet.Razor.Runtime.TagHelpers;
using Microsoft.AspNet.Razor.TagHelpers;

namespace ZKCloud.Web.apps.demo01.src.TagHelpers {
    // You may need to install the Microsoft.AspNet.Razor.Runtime package into your project
    [HtmlTargetElement("textarea")]
    public class TestTagHelper : TagHelper {
        public string Url { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output) {
            using (HttpClient client = new HttpClient()) {
                string html = await client.GetStringAsync(Url);
                output.Content.SetContent(html);
            }
        }
    }
}
