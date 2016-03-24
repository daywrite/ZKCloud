using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZKCloud.Domain.Models;
using ZKCloud.Utils.PageList;

namespace ZKCloud.Web.Apps.CMS.Article.src.ViewModels {
	public class AdminArticleListViewModel {

		public AdminArticleListViewModel() {
			Paging = new PaginationSettings();
		}

		public string Query { get; set; } = string.Empty;

		public PagedList<Domains.Entitys.Article> Articles { get; set; } = null;

		public PaginationSettings Paging { get; set; }
	}
}
