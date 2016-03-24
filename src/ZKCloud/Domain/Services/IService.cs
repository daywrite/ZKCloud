using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZKCloud.Domain.Repositories;

namespace ZKCloud.Domain.Services {
    public interface IService {
        T Repository<T>() where T : IRepository;

        T Service<T>() where T : IService;
    }
}
