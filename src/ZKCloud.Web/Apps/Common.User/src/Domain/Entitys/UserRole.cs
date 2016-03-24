using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using ZKCloud.Domain.Models;
using ZKCloud.Domain.Repositories;
using ZKCloud.Web.apps.Common.Perset.src.Enum;
using ZKCloud.Web.Apps.Common.Perset.src.Enum;

namespace ZKCloud.Web.Apps.Common.Perset.src.Entity {
	/// <summary>
	/// 用户角色
	/// </summary>
	public class UserRole {
		/// <summary>
		/// 角色Id
		/// </summary>
		public virtual long Id { get; set; }
		/// <summary>
		/// 角色名称
		/// </summary>
		public virtual string Name { get; set; }
		/// <summary>
		/// 权限列表的Json
		/// </summary>
		public virtual string PrivilegesContent { get; set; }
		/// <summary>
		///  权限列表
		/// 从数据库获取对象后需要先调用DeserializeContents
		/// 保存对象到数据库前时需要调用SerializeContents
		/// </summary>
		public virtual HashSet<string> Privileges { get; set; }
		/// <summary>
		/// 创建时间
		/// </summary>
		public virtual DateTime CreateTime { get; set; }
		/// <summary>
		/// 更新时间
		/// </summary>
		public virtual DateTime LastUpdated { get; set; }
		/// <summary>
		/// 备注
		/// </summary>
		public virtual string Remark { get; set; }
		/// <summary>
		/// 是否已删除
		/// </summary>
		public virtual DeleteState DeleteState { get; set; }

		/// <summary>
		/// 初始化
		/// </summary>
		public UserRole() {
			Privileges = new HashSet<string>();
		}
	}

	/// <summary>
	/// 创建实体
	/// </summary>
	public class UserRoleCreator : IModelCreator {
		public void CreateModel(ModelBuilder modelBuilder) {
			modelBuilder.Entity<UserRole>(d =>
			{
				d.ToTable("Common_UserRole");
				d.HasKey(e => e.Id);
				d.Property(e => e.Name).IsRequired();
				d.Property(e => e.CreateTime).IsRequired();
				d.Property(e => e.PrivilegesContent).IsRequired();
				d.Property<DeleteState>(e => e.DeleteState);
				d.Ignore(e => e.Privileges);
			});
		}
	}
}