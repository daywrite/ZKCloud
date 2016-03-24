using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZKCloud.Runtime.Config;

namespace ZKCloud.Runtime {
    public class RuntimeContext {
        private RuntimeContext() { }

        public static RuntimeContext Current { get; } = new RuntimeContext();

        public RuntimePath Path { get; } = new RuntimePath();

        private Lazy<WebsiteConfig> _websiteConfig = new Lazy<WebsiteConfig>(() => WebsiteConfig.FromFile(Current.Path.WebsiteConfigPath), true);

        public WebsiteConfig WebsiteConfig { get { return _websiteConfig.Value; } }
    }
}
