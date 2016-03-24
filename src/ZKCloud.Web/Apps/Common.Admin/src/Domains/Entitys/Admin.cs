using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZKCloud.Domain.Models;

namespace ZKCloud.Web.Apps.Common.Admin.src.Domains.Entitys
{
	/// <summary>
	/// 管理员
	/// </summary>
	public class Admin : EntityBase {
		/// <summary>
		/// 关联的用户Id
		/// </summary>
		public virtual long Id { get; set; }
		/// <summary>
		/// 是否超级管理员
		/// </summary>
		public virtual bool SuperAdmin { get; set; }
		/// <summary>
		/// 创建时间
		/// </summary>
		public virtual DateTime CreateTime { get; set; }
		/// <summary>
		/// 备注
		/// </summary>
		public virtual string Remark { get; set; }
	}
}
