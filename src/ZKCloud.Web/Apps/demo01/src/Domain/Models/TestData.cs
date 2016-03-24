using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZKCloud.Domain.Models;

namespace ZKCloud.Web.Apps.Demo01.Domain.Models {
    public class TestData : EntityBase {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
