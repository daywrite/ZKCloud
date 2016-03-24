using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZKCloud.Container {
    public enum RegisterType : int {
        /// <summary>
        /// 单一对象共享
        /// </summary>
        Singleton = 1,
        /// <summary>
        /// 延迟加载的单一对象共享
        /// </summary>
        LazySingleton,
        /// <summary>
        /// 每次获取创建新的对象
        /// </summary>
        PerCall,
        /// <summary>
        /// 为每个线程创建单一共享对象
        /// </summary>
        PerThread,
        /// <summary>
        /// 每个http请求
        /// </summary>
        PerHttpRequest
    }
}
