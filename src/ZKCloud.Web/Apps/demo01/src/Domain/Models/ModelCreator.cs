using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using ZKCloud.Container;
using ZKCloud.Domain.Repositories;

namespace ZKCloud.Web.Apps.Demo01.Domain.Models.Matadata {
    public class ModelCreator : IModelCreator {
        public void CreateModel(ModelBuilder modelBuilder) {
            modelBuilder.Entity<TestData>(d => {
                d.HasKey(e => e.Id);
                d.Property(e => e.Name);
            });
        }
    }
}
