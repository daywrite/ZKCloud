using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZKCloud.Web.Apps.Common.Admin.src.CallBacks {
	/// <summary>
	/// 后台图标
	/// </summary>
	public class AdminAppIcon {
		/// <summary>
		/// 应用名称
		/// </summary>
		public virtual string Name { get; set; }
		/// <summary>
		/// url链接
		/// </summary>
		public virtual string Url { get; set; }
		/// <summary>
		/// 格式的css类名
		/// </summary>
		public virtual string TileClass { get { return "tile bg-grey-gallery"; }
			set { } }
		/// <summary>
		/// 图标的css类名
		/// </summary>
		public virtual string IconClass { get { return "fa fa-archive"; } set { } }
		/// <summary>
		/// 图标的排序号
		/// </summary>
		public virtual long DisPalyOrder { get; set; }
	}
}