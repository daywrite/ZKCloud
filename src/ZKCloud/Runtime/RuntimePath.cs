using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;

namespace ZKCloud.Runtime {
    public class RuntimePath {
        internal RuntimePath() { }

        /// <summary>
        /// 系统运行根目录（绝对路径，如c:\\wwwroot\zkcloud)
        /// </summary>
        public string BaseDirectory { get; } = AppContext.BaseDirectory;

        /// <summary>
		/// App_Data目录
		/// </summary>
		public string AppDataDirectory {
            get { return Path.Combine(BaseDirectory, "App_Data"); }
        }

        /// <summary>
        /// 日志文件目录
        /// </summary>
        public string LogsDirectory {
            get { return Path.Combine(AppDataDirectory, "logs"); }
        }

        /// <summary>
        /// 网站配置文件路径
        /// </summary>
        public string WebsiteConfigPath {
            get { return Path.Combine(AppDataDirectory, "config.json"); }
        }

        public string Combine(string path1) {
            return Path.Combine(BaseDirectory, path1);
        }

        public string Combine(string path1, string path2) {
            return Path.Combine(BaseDirectory, path1, path2);
        }

        public string Combine(params string[] path1) {
            string[] fullPathArray = new string[path1.Length + 1];
            fullPathArray[0] = BaseDirectory;
            for (int i = 0; i < path1.Length; i++) {
                fullPathArray[i + 1] = path1[i];
            }
            return Path.Combine(fullPathArray);
        }
    }
}
