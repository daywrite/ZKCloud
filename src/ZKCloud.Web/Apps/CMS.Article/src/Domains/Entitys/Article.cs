using Microsoft.Data.Entity;
using System;
using ZKCloud.Domain.Models;
using ZKCloud.Domain.Repositories;
using ZKCloud.Web.apps.Common.Perset.src.Enum;
using ZKCloud.Web.Apps.CMS.Article.src.Domains.Enums;

namespace ZKCloud.Web.Apps.CMS.Article.src.Domains.Entitys {
	/// <summary>
	/// 文章
	/// </summary>
	public class Article:EntityBase
    {
		/// <summary>
		/// 文章Id
		/// </summary>
		public virtual long Id { get; set; }
		/// <summary>
		/// 文章标题
		/// </summary>
		public virtual string Title { get; set; }
		/// <summary>
		/// 文章来源
		/// </summary>
		public virtual string Source { get; set; }
		/// <summary>
		/// 文章摘要
		/// </summary>
		public virtual string Summary { get; set; }
		/// <summary>
		/// 发布会员ID
		/// </summary>
		public virtual long UserId { get; set; }
		/// <summary>
		/// 文章介绍，格式是Html
		/// </summary>
		public virtual string Intro { get; set; }
		/// <summary>
		/// 文章缩略图
		/// </summary>
		public virtual string ThumImages { get; set; }
		/// <summary>
		/// 浏览次数
		/// </summary>
		public virtual long ViewCount { get; set; }
		/// <summary>
		/// 文章状态
		/// </summary>
		public virtual ArticleStates State { get; set; }
		/// <summary>
		/// 创建时间
		/// </summary>
		public virtual DateTime CreateTime { get; set; }
		/// <summary>
		/// 最后更新时间
		/// </summary>
		public virtual DateTime LastUpdated { get; set; }
		/// <summary>
		/// 删除状态
		/// </summary>
		public virtual DeleteState DeleteState { get; set; }
		/// <summary>
		/// 显示顺序，从小到大
		/// </summary>
		public virtual long DisplayOrder { get; set; }
		/// <summary>
		/// 备注，格式是Html
		/// </summary>
		public virtual string Remark { get; set; }
	}

	/// <summary>
	/// 创建实体
	/// </summary>
	public class ArticleCreator : IModelCreator {
		public void CreateModel(ModelBuilder modelBuilder) {
			modelBuilder.Entity<Article>(d =>
			{
				d.ToTable("CMS_Article");
				d.HasKey(e => e.Id);
				d.Property(e=>e.Title).IsRequired();
				d.Property<DeleteState>(e => e.DeleteState);
				d.Property<ArticleStates>(e => e.State);
			});
		}
	}
}

