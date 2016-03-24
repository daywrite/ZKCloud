using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Html.Abstractions;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Extensions.WebEncoders;
using ZKCloud.Localize;

namespace ZKCloud.Web.Mvc
{
    public class LocalizedHtmlString : IHtmlContent
    {
        private LocalizedString _localizedString;

        public LocalizedHtmlString(string input) {
            _localizedString = new LocalizedString(input);
        }

        public void WriteTo(TextWriter writer, IHtmlEncoder encoder) {
            if (writer == null) {
                throw new ArgumentNullException(nameof(writer));
            }

            if (encoder == null) {
                throw new ArgumentNullException(nameof(encoder));
            }

            writer.Write(_localizedString.ToString());
        }

        public override string ToString() {
            return _localizedString.ToString();
        }
    }
}
