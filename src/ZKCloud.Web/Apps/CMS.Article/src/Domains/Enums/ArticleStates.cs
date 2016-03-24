using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZKCloud.Web.Apps.CMS.Article.src.Domains.Enums {
	/// <summary>
	/// 文章发布状态
	/// </summary>
	public enum ArticleStates {
		/// <summary>
		/// 已发布
		/// </summary>
		Publish = 0,
		/// <summary>
		/// 未发布
		/// </summary>
		UnPublish = 1,
	}
}
