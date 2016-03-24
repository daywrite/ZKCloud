using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZKCloud.Web.apps.Common.Perset.src.Enum {
	/// <summary>
	/// 删除状态
	/// </summary>
	public enum DeleteState {
		/// <summary>
		/// 正常
		/// </summary>
		Normal = 0,
		/// <summary>
		/// 已删除（可回收）
		/// </summary>
		Deleted = 1,
		/// <summary>
		/// 待审核
		/// </summary>
		IsCheck = 2
	}
}
