using Microsoft.Data.Entity;
using System;
using ZKCloud.Domain.Models;
using ZKCloud.Domain.Repositories;

namespace ZKCloud.Web.Apps.Common.Base.src.Domains.Entitys{
	/// <summary>
	/// 定时任务
	/// </summary>
	public class ScheduledTask : EntityBase {
		/// <summary>
		/// 主键，没有意义
		/// </summary>
		public virtual long Id { get; set; }
		/// <summary>
		/// 所属插件 + 任务键名
		/// </summary>
		public virtual string Key { get; set; }
		/// <summary>
		/// 创建时间
		/// </summary>
		public virtual DateTime CreateTime { get; set; }
		/// <summary>
		/// 最后一次执行的时间
		/// </summary>
		public virtual DateTime LastExecuted { get; set; }
		/// <summary>
		/// 所属应用名称
		/// </summary>
		public virtual string AppName { get; set; }
	}

	/// <summary>
	/// 创建实体
	/// </summary>
	public class ScheduledTaskCreator : IModelCreator {
		public void CreateModel(ModelBuilder modelBuilder) {
			modelBuilder.Entity<ScheduledTask>(d =>
			{
				d.ToTable("Common_ScheduledTask");
				d.HasKey(e => e.Id);
				d.Property(e => e.Key).HasMaxLength(255).IsRequired();
			});
		}
	}
}
