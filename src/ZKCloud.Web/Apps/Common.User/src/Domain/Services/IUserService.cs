using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Http.Features;
using ZKCloud.Domain.Services;

namespace ZKCloud.Web.Apps.Common.User.src.Services {
	public interface IUserService : IAutoApiService {
		IList<Common.User.src.Entity.User> GetList();

		/// <summary>
		/// 根据用户名查找用户
		/// 找不到时返回null
		/// </summary>
		Common.User.src.Entity.User FindUser(string username);
		// <summary>
		/// 以用户名和密码登录
		/// 注意登陆时会把原会话删除，需要继承部分数据时请添加回调处理
		/// </summary>
		/// <param name="username">用户名</param>
		/// <param name="password">密码</param>
		/// <param name="rememberLogin">是否记住登录</param>
		/// <param name="extraCallback">额外的回调</param>
		/// <returns></returns>
		bool Login(string username, string password, bool rememberLogin,ISession httpSession);
		/// <summary>
		/// 以指定用户登录
		/// 跳过密码等检查
		/// </summary>
		bool LoginWithUser(Common.User.src.Entity.User user, bool rememberLogin, ISession httpSession);
		/// <summary>
		/// 检查用户是否存在
		/// </summary>
		/// <param name="username"></param>
		/// <returns></returns>
		bool Exists(string username);
		/// <summary>
		/// 退出登录
		/// </summary>
		void Logout();
		/// <summary>
		/// 修改密码
		/// </summary>
		/// <param name="userId">用户Id</param>
		/// <param name="oldPassword">原密码</param>
		/// <param name="newPassword">新密码</param>
		Tuple<bool, string> ChangePassword(long userId, string oldPassword, string newPassword);
		/// <summary>
		/// 生成随机密码
		/// </summary>
		/// <returns></returns>
		string CreateRandomPassword();

		/// <summary>
		/// 保存头像，返回是否成功和错误信息
		/// </summary>
		/// <param name="userId">用户Id</param>
		/// <param name="imageStream">图片数据流</param>
		void SaveAvatar(long userId, Stream imageStream);
		/// <summary>
		/// 删除头像
		/// </summary>
		/// <param name="userId">用户Id</param>
		void DeleteAvatar(long userId);
		/// <summary>
		/// 用户注册
		/// </summary>
		/// <returns></returns>
		Tuple<bool,string> UserRegedit(Entity.User user);
		/// <summary>
		/// 重置密码
		/// </summary>
		/// <returns></returns>
		void ResetPassword(string useName);

	}
}
