using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZKCloud.Domain.Services;

namespace ZKCloud.Web.Apps.Common.Admin.src.Domains.Services {
	public interface IAdminService : IAutoApiService {
		/// <summary>
		/// 添加管理员
		/// </summary>
		/// <param name="article"></param>
		void AddSingle(Entitys.Admin admin);
		/// <summary>
		/// 返回管理员列表
		/// </summary>
		/// <returns></returns>
		IList<Entitys.Admin> GetList();
		/// <summary>
		/// 编辑管理员
		/// </summary>
		/// <param name=""></param>
		void UpdateSingle(Entitys.Admin admin);
		/// <summary>
		/// 获取管理员
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		Entitys.Admin Read(long id);
		/// <summary>
		/// 删除管理员，用‘，’隔开Id
		/// </summary>
		/// <param name="ids"></param>
		void Delete(params int[] ids);

		IList<Entitys.Admin> ReadMany();

		/// <summary>
		/// 分配管理员权限给现有用户
		/// </summary>
		void AssignToExistUser(string username, bool superAdmin, long roleId);
	}
}
