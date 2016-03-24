using Microsoft.Data.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using ZKCloud.Domain.Models;
using ZKCloud.Domain.Repositories;
using ZKCloud.Web.apps.Common.Perset.src.Enum;
using ZKCloud.Web.Apps.Common.Perset.src.Entity;
using ZKCloud.Web.Apps.Common.Perset.src.Enum;

namespace ZKCloud.Web.Apps.Common.User.src.Entity {
	/// <summary>
	/// 用户
	/// </summary>
	public class User :EntityBase{
		/// <summary>
		/// 用户Id
		/// </summary>
		public virtual long Id { get; set; }
		/// <summary>
		/// 用户名
		/// </summary>
		public virtual string Username { get; set; }
		/// <summary>
		/// 密码信息，json
		/// </summary>
		public virtual string Password { get; set; }
		/// <summary>
		/// 创建时间
		/// </summary>
		public virtual DateTime CreateTime { get; set; }
		/// <summary>
		/// 关联的用户角色
		/// </summary>
		public virtual ICollection<UserRole> Roles { get; set; }
		/// <summary>
		/// 附加数据
		/// </summary>
		public virtual string  Items { get; set; }
		/// <summary>
		/// 是否已删除
		/// </summary>
		public virtual DeleteState DeleteState { get; set; }
	}

	/// <summary>
	/// 用户的扩展函数
	/// </summary>
	public static class UserExtensions {
		/// <summary>
		/// 设置密码
		/// </summary>
		/// <param name="password"></param>
		public static void SetPassword(this User user, string password) {
			if (string.IsNullOrEmpty(password)) {
				throw new System.ArgumentNullException("password");
			}
			//user.Password = JsonConvert.SerializeObject(PasswordInfo.FromPassword(password));
		}

		/// <summary>
		/// 检查密码
		/// </summary>
		/// <param name="password"></param>
		/// <returns></returns>
		public static bool CheckPassword(this User user, string password) {
			if (string.IsNullOrEmpty(user.Password) || string.IsNullOrEmpty(password)) {
				return false;
			}
			//var passwordInfo = JsonConvert.DeserializeObject<PasswordInfo>(user.Password);
			//return passwordInfo.Check(password);
			return true;
		}
	}

	/// <summary>
	/// 创建实体
	/// </summary>
	public class UserCreator : IModelCreator {
		public void CreateModel(ModelBuilder modelBuilder) {
			modelBuilder.Entity<User>(d =>
			{
				d.ToTable("Common_User");
				d.HasKey(e => e.Id);
				d.HasIndex(e => e.Username).IsUnique();
				d.Property(e => e.Password).IsRequired();
				d.Property<DeleteState>(e => e.DeleteState);
			});
		}
	}
}
