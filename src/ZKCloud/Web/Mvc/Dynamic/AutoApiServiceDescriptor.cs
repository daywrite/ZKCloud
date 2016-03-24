using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading.Tasks;
using ZKCloud.Domain.Services;

namespace ZKCloud.Web.Mvc.Dynamic {
    public class AutoApiServiceDescriptor {
        private AutoApiMethodDescriptor[] _methods;

        public string AppName { get; private set; }

        public Type BaseInterfaceType { get; } = typeof(IAutoApiService);

        public Type ServiceInterfaceType { get; private set; }

        public AutoApiMethodDescriptor[] Methods {
            get {
                if (_methods == null) {
                    var baseInterfaceMethods = BaseInterfaceType.GetMethods();
                    _methods = ServiceInterfaceType.GetMethods()
                        .Where(e => !baseInterfaceMethods.Any(m => m.Name == e.Name))
                        .Select(e => new AutoApiMethodDescriptor(e))
                        .ToArray();
                }
                return _methods;
            }
        }

        public AutoApiServiceDescriptor(string appName, Type serviceInterfaceType) {
            AppName = appName;
            ServiceInterfaceType = serviceInterfaceType;
        }

        public TypeInfo CreateDynamicApiType(ModuleBuilder builder) {
            var typeBuilder = builder.DefineType(CreateControllerName());
            typeBuilder.SetParent(typeof(BaseController));
            if (!string.IsNullOrWhiteSpace(AppName)) {
                CustomAttributeBuilder attributeBuilder = new CustomAttributeBuilder(
                    typeof(AppAttribute).GetConstructor(new[] { typeof(string) }), new object[] { AppName });
                typeBuilder.SetCustomAttribute(attributeBuilder);
            }
            foreach (var item in Methods) {
                item.CreateDynamicMethod(typeBuilder);
            }
            return typeBuilder.CreateTypeInfo();
        }

        private string CreateControllerName() {
            string interfaceName = ServiceInterfaceType.Name;
            if (interfaceName.StartsWith("I") && interfaceName.Length > 1)
                interfaceName = interfaceName.Substring(1);
            if (interfaceName.EndsWith("Service") && interfaceName.Length > 7)
                interfaceName = interfaceName.Substring(0, interfaceName.Length - 7);
            return $"{interfaceName}Controller";
        }
    }
}
