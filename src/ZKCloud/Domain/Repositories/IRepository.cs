using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZKCloud.Domain.Models;

namespace ZKCloud.Domain.Repositories {
    public interface IRepository {
        IRepositoryContext RepositoryContext { get; }
    }
}
