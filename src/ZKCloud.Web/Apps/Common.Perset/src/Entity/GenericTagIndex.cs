using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZKCloud.Domain.Models;
using ZKCloud.Domain.Repositories;

namespace ZKCloud.Web.apps.Common.Perset.src.Entity
{
    /// <summary>
    /// 通用标签索引
    /// </summary>
    public class GenericTagIndex : EntityBase{
        /// <summary>
		/// 索引ID
		/// </summary>
		public virtual long Id { get; set; }
        /// <summary>
        /// 实体类型
        /// </summary>
        public virtual string Type { get; set; }
        /// <summary>
        /// 实体ID
        /// </summary>
        public virtual long EntityId { get; set; }
        /// <summary>
        /// 标签ID
        /// </summary>
        public virtual long ClassId { get; set; }
    }

    /// <summary>
    /// 创建实体
    /// </summary>
    public class GenericTagIndexCreator : IModelCreator
    {
        public void CreateModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GenericTagIndex>(d =>
            {
                d.ToTable("Common_GenericTagIndex");
                d.HasKey(e => e.Id);
            });
        }
    }
}
