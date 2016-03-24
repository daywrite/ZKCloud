using ZKCloud.Domain.Repositories;

namespace ZKCloud.Web.Apps.CMS.Article.src.Domains.Repositories {
	/// <summary>
	/// 文章数据仓储
	/// </summary>
	public class ArticleRepositories : ReadWriteRepositoryBase<src.Domains.Entitys.Article>{

	}
	public class ArticlePageRepositories : PagedReadRepositoryBase<src.Domains.Entitys.Article> {

	}
}