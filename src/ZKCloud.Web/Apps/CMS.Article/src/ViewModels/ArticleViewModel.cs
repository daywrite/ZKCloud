using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ZKCloud.Domain.Models;
using ZKCloud.Web.apps.Common.Perset.src.Enum;
using ZKCloud.Web.Apps.CMS.Article.src.Domains.Enums;

namespace ZKCloud.Web.Apps.CMS.Article.src.ViewModels {
	public class ArticleViewModel {
		/// <summary>
		/// 文章Id
		/// </summary>
		public virtual long Id { get; set; }
		/// <summary>
		/// 文章标题
		/// </summary>
		[Required]
		[DataType(DataType.Text)]
		[Display(Name = "文章标题")]
		public virtual string Title { get; set; }
		/// <summary>
		/// 文章来源
		/// </summary>

		[DataType(DataType.Text)]
		[Display(Name = "文章来源")]
		public virtual string Source { get; set; }
		/// <summary>
		/// 文章摘要
		/// </summary>
		[DataType(DataType.Text)]
		[Display(Name = "文章摘要")]
		public virtual string Summary { get; set; }
		/// <summary>
		/// 文章介绍，格式是Html
		/// </summary>
		[Required]
		[DataType(DataType.Text)]
		[Display(Name = "文章介绍")]
		public virtual string Intro { get; set; }
		/// <summary>
		/// 文章缩略图
		/// </summary>
		public virtual string ThumImages { get; set; }
		/// <summary>
		/// 浏览次数
		/// </summary>
		[DataType(DataType.Text)]
		[Display(Name = "浏览次数")]
		public virtual long ViewCount { get; set; }
		/// <summary>
		/// 文章状态
		/// </summary>

		[DataType(DataType.Text)]
		[Display(Name = "文章状态")]
		public virtual ArticleStates State { get; set; }
		/// <summary>
		/// 创建时间
		/// </summary>

		[DataType(DataType.Text)]
		[Display(Name = "创建时间")]
		public virtual DateTime CreateTime { get; set; }
		/// <summary>
		/// 最后更新时间
		/// </summary>

		[DataType(DataType.Text)]
		[Display(Name = "更新时间")]
		public virtual DateTime LastUpdated { get; set; }
			/// <summary>
			/// 删除状态
			/// </summary>
			public virtual DeleteState DeleteState { get; set; }
		/// <summary>
		/// 显示顺序，从小到大
		/// </summary>

		[DataType(DataType.Text)]
		[Display(Name = "显示顺序")]
		public virtual long DisplayOrder { get; set; }
		/// <summary>
		/// 备注，格式是Html
		/// </summary>

		[DataType(DataType.Text)]
		[Display(Name = "备注")]
		public virtual string Remark { get; set; }

		[DataType(DataType.Text)]
		[Display(Name = "作者")]
		public virtual string Author { get; set; }

	} 
}
