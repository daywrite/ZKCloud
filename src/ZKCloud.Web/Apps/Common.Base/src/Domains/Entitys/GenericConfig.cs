using Microsoft.Data.Entity;
using System;
using ZKCloud.Domain.Models;
using ZKCloud.Domain.Repositories;

namespace ZKCloud.Web.Apps.Common.Base.src.Domains.Entitys{
	/// <summary>
	/// 通用配置
	/// </summary>
	public class GenericConfig : EntityBase {
		/// <summary>
		/// 主键，没有意义
		/// </summary>
		public virtual long Id { get; set; }
		/// <summary>
		/// 所属插件 + 配置键名
		/// </summary>
		public virtual string Key { get; set; }
		/// <summary>
		/// 配置值（json）
		/// </summary>
		public virtual string Value { get; set; }
		/// <summary>
		/// 所属应用名称
		/// </summary>
		public virtual string AppName { get; set; }
		/// <summary>
		/// 最后更新时间
		/// </summary>
		public virtual DateTime LastUpdated { get; set; }
	}

	/// <summary>
	/// 参考教程
	/// https://msdn.microsoft.com/zh-cn/data/jj591617.aspx#1.1 
	/// </summary>
	public class GenericConfigCreator : IModelCreator {
		public void CreateModel(ModelBuilder modelBuilder) {
			modelBuilder.Entity<GenericConfig>(d =>
			{
				d.ToTable("Common_GenericConfig");
				d.HasKey(e => e.Id);
				d.HasIndex(e => e.Key).IsUnique();
			});
		}
	}
}
