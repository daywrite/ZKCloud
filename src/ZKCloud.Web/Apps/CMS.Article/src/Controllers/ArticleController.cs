using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZKCloud.Web.Mvc;
using Microsoft.AspNet.Http;
using ZKCloud.Web.Apps.CMS.Article.src.Domains.Services;
using ZKCloud.Web.Apps.CMS.Article.src.ViewModels;
using ZKCloud.Utils.PageList;


namespace ZKCloud.Web.Apps.CMS.Article.src.Controllers {
	/// <summary>
	/// 文章控制器
	/// </summary>
	[App("CMS.Article")]
	public class ArticleController : BaseController {
		private const int DefaultPageSize = 10;

		/// <summary>
		/// 后台文章列表
		/// </summary>
		/// <returns></returns>
		[HttpGet("admin/article/index")]
		public IActionResult AdminList(int? pageNumber, int? pageSize, string query = "") {
			var model = new AdminArticleListViewModel();
			IArticleServices service = new ArticleServices();
			int currentPageIndex = pageNumber.HasValue ? pageNumber.Value - 1 : 0;
			int itemsPerPage = pageSize.HasValue ? pageSize.Value : DefaultPageSize;
			model.Articles = service.ReadPage(currentPageIndex + 1, itemsPerPage);
			model.Paging.CurrentPage = pageNumber.HasValue ? pageNumber.Value : 1;
			model.Paging.ItemsPerPage = DefaultPageSize;
			model.Paging.TotalItems = model.Articles.RecordCount;
			model.Paging.ShowFirstLast = true;
			return View(model);
		}
		/// <summary>
		/// 后台文章添加
		/// </summary>
		/// <returns></returns>
		[HttpGet("admin/article/add")]
		public IActionResult AdminAdd() {
			return View();

		}
		[HttpPost]
		public IActionResult AdminAdd(ViewModels.ArticleViewModel model) {
			var userId = HttpContext.Session.GetInt32("UserId");
			IArticleServices service = new ArticleServices();
			var article = new Domains.Entitys.Article()
			{
				CreateTime = DateTime.Now,
				LastUpdated = DateTime.Now,
				Title = model.Title,
				Source = model.Source,
				Summary = model.Summary,
				UserId = (long)(userId ?? 0),
				Intro = model.Intro,
				State = model.State,
				DisplayOrder = model.DisplayOrder,
				Remark = model.Remark,
				ThumImages = ""
			};
			service.AddSingle(article);
			return Redirect("/CMS.Article/Article/AdminList");
		}
		/// <summary>
		/// 会员添加文章
		/// </summary>
		/// <returns></returns>
		public IActionResult UserAdd() {
			return View();
		}
		/// <summary>
		/// 会员文章列表
		/// </summary>
		/// <returns></returns>
		public IActionResult UserList() {
			return View();
		}
		/// <summary>
		/// 前台文章中心首页
		/// </summary>
		/// <returns></returns>
		public IActionResult Index() {
			return View();
		}

		public IActionResult List() {
			return View();
		}

		public IActionResult Show() {
			return View();
		}
	}
}