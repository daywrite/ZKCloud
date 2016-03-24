using Microsoft.Data.Entity;
using System;
using ZKCloud.Domain.Models;
using ZKCloud.Domain.Repositories;
using ZKCloud.Web.apps.Common.Perset.src.Enum;
using ZKCloud.Web.Apps.Common.Perset.src.Enum;

namespace ZKCloud.Web.apps.Common.Perset.src.Entity
{
	/// <summary>
	/// 通用附属数据
	/// 删除：先到回收站，再从数据库中删除
	/// </summary>
	public class GenericAttached :EntityBase{
		/// <summary>
		/// 附属数据Id
		/// </summary>
		public virtual long Id { get; set; }
		/// <summary>
		/// 附属数据类型
		/// 格式请使用 "插件.类型"
		/// </summary>
		public virtual string Type { get; set; }
		/// <summary>
		/// 附属数据名称
		/// </summary>
		public virtual string Name { get; set; }
		/// <summary>
		/// 表名
		/// 格式请使用："表名"
		/// </summary>
		public virtual string Table { get; set; }
		/// <summary>
		/// 创建时间
		/// </summary>
		public virtual DateTime CreateTime { get; set; }
		/// <summary>
		/// 删除状态
		/// </summary>
		public virtual DeleteState DeleteState { get; set; }
		/// <summary>
		/// 显示顺序，从小到大
		/// </summary>
		public virtual long DisplayOrder { get; set; }
		/// <summary>
		/// 备注
		/// </summary>
		public virtual string Remark { get; set; }
	}

    public class GenericAttachedCreator : IModelCreator
    {
        public void CreateModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GenericAttached>(d =>
            {
                d.ToTable("Common_GenericAttached");
                d.HasKey(e => e.Id);
                d.Property(c => c.Type);
                d.Property(c => c.Name).IsRequired();
                d.Property(c => c.Table);
                d.Property(c => c.CreateTime);
                d.Property(c => c.DeleteState);
                d.Property(c => c.DisplayOrder);
                d.Property(c => c.Remark);
             
            });
        }
    }
}
