using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZKCloud.Domain.Repositories {
    public interface IRepositoryContext : IDisposable {
        string ConnectionString { get; }

        object DbContext { get; }

        void Open();

        void Close();

        void SaveChanges();

        IQueryable<T> Query<T>() where T : class;

        void Add<T>(T data) where T : class;

        void Update<T>(T data) where T : class;

        void Delete<T>(T data) where T : class;
    }
}
