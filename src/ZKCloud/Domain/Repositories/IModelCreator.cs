using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;

namespace ZKCloud.Domain.Repositories {
    public interface IModelCreator {
        void CreateModel(ModelBuilder builder);
    }
}
