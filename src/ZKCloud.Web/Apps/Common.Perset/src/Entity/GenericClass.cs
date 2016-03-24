using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZKCloud.Domain.Models;
using ZKCloud.Domain.Repositories;

namespace ZKCloud.Web.apps.Common.Perset.src.Entity {
	/// <summary>
	/// 通用分类
	/// </summary>
	public class GenericClass : EntityBase {
		/// <summary>
		/// 分类Id
		/// </summary>
		public virtual long Id { get; set; }
		/// <summary>
		/// 分类类型
		/// </summary>
		public virtual string Type { get; set; }
		/// <summary>
		/// 上级分类，根节点时等于null
		/// </summary>
		public virtual GenericClass Parent { get; set; }
		/// <summary>
		/// 名称
		/// </summary>
		public virtual string Name { get; set; }
		/// <summary>
		/// 搜索引擎优化标题
		/// </summary>
		public virtual string SeoTitle { get; set; }
		/// <summary>
		/// 搜索引擎优化关键字
		/// </summary>
		public virtual string KeyWords { get; set; }
		/// <summary>
		/// 搜索引擎优化描述
		/// </summary>
		public virtual string Description { get; set; }
		/// <summary>
		/// 创建时间
		/// </summary>
		public virtual DateTime CreateTime { get; set; }
		/// <summary>
		/// 显示顺序，从小到大
		/// </summary>
		public virtual long DisplayOrder { get; set; }
		/// <summary>
		/// 备注
		/// </summary>
		public virtual string Remark { get; set; }
		/// <summary>
		/// 是否已删除
		/// </summary>
		public virtual bool Deleted { get; set; }
	}

	public class GenericClassCreator : IModelCreator {
		public void CreateModel(ModelBuilder modelBuilder) {
			modelBuilder.Entity<GenericClass>(d =>
			{
				d.ToTable("Common_GenericClass");
				d.HasKey(e => e.Id);
				d.Property(c => c.Type);
				d.Property(c => c.Name).IsRequired();
				d.Property(c => c.SeoTitle);
				d.Property(c => c.KeyWords);
				d.Property(c => c.Description);
				d.Property(c => c.CreateTime);
				d.Property(c => c.DisplayOrder);
				d.Property(c => c.Remark);
				d.Property(c => c.Deleted);

			});
		}
	}
}
