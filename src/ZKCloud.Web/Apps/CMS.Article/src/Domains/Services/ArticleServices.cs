using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZKCloud.Domain.Models;
using ZKCloud.Domain.Repositories;
using ZKCloud.Domain.Services;
using ZKCloud.Web.Apps.CMS.Article.src.Domains.Entitys;
using ZKCloud.Web.Apps.CMS.Article.src.Domains.Repositories;

namespace ZKCloud.Web.Apps.CMS.Article.src.Domains.Services {
	public class ArticleServices : ServiceBase, IArticleServices {
		public void AddSingle(Entitys.Article article) {
			Repository<ArticleRepositories>().AddSingle(article);
        }

		public void DelSingle(string ids) {
			if (!string.IsNullOrEmpty(ids))
			{
				var arrayId = ids.Split(',');
				List<long> ListId = new List<long>();
				foreach (var item in arrayId) {
					if (!string.IsNullOrEmpty(item))
						ListId.Add(long.Parse(item));
				}
				Repository<ArticleRepositories>().Delete(x=> ListId.Contains(x.Id));
            }
		}

		public void UpdateSingle(Entitys.Article article) {
			Repository<ArticleRepositories>().UpdateSingle(article);
		}

		public List<Entitys.Article> GetList() {
			return Repository<ArticleRepositories>().ReadMany(e=>true).ToList();
        }

		public Entitys.Article GetSingle(long id) {
			return Repository<ArticleRepositories>().ReadSingle(e => e.Id == id);
        }

		public Entitys.Article Read(long id) {
			throw new NotImplementedException();
		}

		public void Delete(params int[] ids) {
			throw new NotImplementedException();
		}

		public IList<Entitys.Article> ReadMany() {
			return Repository<ArticleRepositories>().ReadMany(e => true).ToList();
		}

		public PagedList<Entitys.Article> ReadPage(int pageIndex = 1, int pageSize = 10) {
			return new ArticlePageRepositories().ReadMany(e => true, pageSize, pageIndex);
        } 
    }
}
