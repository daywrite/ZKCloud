using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using ZKCloud.Domain.Models;
using ZKCloud.Domain.Repositories;

namespace ZKCloud.Web.Apps.Common.Base.src.Domains.Entitys{
	/// <summary>
	///回话数据
	/// </summary>
	public class Session : EntityBase {
		/// <summary>
		/// 会话Id
		/// </summary>
		public virtual Guid Id { get; set; }
		/// <summary>
		/// 关联Id
		/// 一般是用户Id
		/// </summary>
		public virtual long ReleatedId { get; set; }
		/// <summary>
		/// 会话数据
		/// </summary>
		public virtual string Items { get; set; }
		/// <summary>
		/// 会话对应的Ip地址
		/// </summary>
		public virtual string IpAddress { get; set; }
		/// <summary>
		/// 是否记住登录
		/// </summary>
		public virtual bool RememberLogin { get; set; }
		/// <summary>
		/// 过期时间
		/// </summary>
		public virtual DateTime Expires { get; set; }
		/// <summary>
		/// 过期时间是否已更新
		/// 这个值只用于检测是否应该把新的过期时间发送到客户端
		/// </summary>
		public virtual bool ExpiresUpdated { get; set; }
		/// <summary>
		/// 初始化
		/// </summary>
		public Session() {
			
		}
	}
	/// <summary>
	/// 会话的扩展函数
	/// </summary>
	public static class SessionExtensions {
		/// <summary>
		/// 重新生成Id
		/// </summary>
		public static void ReGenerateId(this Session session) {
			session.Id = System.Guid.NewGuid();
		}

		/// <summary>
		/// 设置会话最少在指定的时间后过期
		/// 当前会话的过期时间比指定的时间要晚时不更新当前的过期时间
		/// </summary>
		/// <param name="span">最少在这个时间后过期</param>
		public static void SetExpiresAtLeast(this Session session, TimeSpan span) {
			var expires = DateTime.UtcNow + span;
			if (session.Expires < expires)
			{
				session.Expires = expires;
				session.ExpiresUpdated = true;
			}
		}


		/// <summary>
		/// 创建实体
		/// </summary>
		public class SessionTaskCreator : IModelCreator {
			public void CreateModel(ModelBuilder modelBuilder) {
				modelBuilder.Entity<Session>(d =>
				{
					d.ToTable("Common_Session");
					d.HasIndex(e => e.ReleatedId);
					d.HasKey(e => e.Id);
					d.HasIndex(e => e.Expires);
					d.Ignore(e => e.Items); 
					d.Property<string>(e => e.Items);
				});
			}
		}
	}
}