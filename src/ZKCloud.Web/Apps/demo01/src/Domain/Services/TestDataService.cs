using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZKCloud.Domain.Services;
using ZKCloud.Web.Apps.Demo01.Domain.Models;
using ZKCloud.Web.Apps.Demo01.Domain.Repositories;

namespace ZKCloud.Web.Apps.Demo01.Domain.Services {
    public class TestDataService : ServiceBase, ITestDataService {
        public IList<TestData> GetList() {
            return Repository<TestDataRepository>().ReadMany(e => true).ToList();
        }
    }
}
