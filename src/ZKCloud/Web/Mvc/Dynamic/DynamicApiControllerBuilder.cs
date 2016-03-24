using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using ZKCloud.Extensions;

namespace ZKCloud.Web.Mvc.Dynamic {
    public class DynamicApiControllerBuilder {
        private static AssemblyBuilder _apiControllerAssemblyBuilder =
            AssemblyBuilder.DefineDynamicAssembly(new AssemblyName("ZKCloud.Web.Mvc.Dynamic.Controllers"), AssemblyBuilderAccess.Run);

        private static ModuleBuilder _apiControllerModuleBuilder = _apiControllerAssemblyBuilder.DefineDynamicModule("main");

        private static IList<TypeInfo> DynamicApiControllerTypeList { get; } = new List<TypeInfo>();

        public static void AddDynamicApiControllerTypes(IList<TypeInfo> typeList) {
            DynamicApiControllerTypeList.Foreach(e => typeList.Add(e));
        }

        public static DynamicApiControllerBuilder For<T>() {
            return new DynamicApiControllerBuilder(typeof(T));
        }

        private Type _serviceType;

        private string _appName;

        private string _routeUrl;

        private DynamicApiControllerBuilder(Type serviceType) {
            _serviceType = serviceType;
        }

        public DynamicApiControllerBuilder WithApp(string appName) {
            _appName = appName;
            return this;
        }

        public DynamicApiControllerBuilder WithRoute(string routeUrl) {
            _routeUrl = routeUrl;
            return this;
        }

        public void Build() {
            var autoApiServiceDescriptor = new AutoApiServiceDescriptor(_appName, _serviceType);
            DynamicApiControllerTypeList.Add(autoApiServiceDescriptor.CreateDynamicApiType(_apiControllerModuleBuilder));
        }
    }
}
