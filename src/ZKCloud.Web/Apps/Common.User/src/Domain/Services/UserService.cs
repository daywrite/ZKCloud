using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Http.Features;
using ZKCloud.Domain.Services;
using ZKCloud.Web.Apps.Common.User.src.Entity;
using ZKCloud.Web.Apps.Common.User.src.Repositories;

namespace ZKCloud.Web.Apps.Common.User.src.Services {

	public class UserService : ServiceBase, IUserService {
		public IList<Entity.User> GetList() {
			return Repository<UserRepository>().ReadMany(e => true).ToList();
		}

		/// <summary>
		/// 生成随机密码
		/// </summary>
		/// <returns></returns>
		public  string CreateRandomPassword() {
			string random = new Random().ToString();
			return random; 
		}

		public Entity.User FindUser(string username) {
			var user = new Entity.User();
            if (!string.IsNullOrEmpty(username)){ 
				user = Repository<UserRepository>().ReadSingle(x => x.Username == username);
			}
			return user;
		}

		public bool Login(string username, string password, bool rememberLogin, ISession httpSession) {
			bool flag = false;
			var user = Repository<UserRepository>().ReadSingle(x => x.Username == username & x.Password == password);
			if (user != null) {
				flag = new Base.src.Domains.Services.SessionServices().SetSession(user, httpSession, rememberLogin);
            }
            return flag; 
		}

		public bool LoginWithUser(Entity.User user, bool rememberLogin, ISession httpSession) {
			bool flag = false; 
			if (user != null){
				flag = new Base.src.Domains.Services.SessionServices().SetSession(user,httpSession);
			}
			return flag;
		}

		public void Logout() { 
			new Base.src.Domains.Services.SessionServices().RemoveSession(); 
		}

		public Tuple<bool,string> ChangePassword(long userId, string oldPassword, string newPassword) {
			var result = Tuple.Create(false,"原密码错误！"); 
			var user = Repository<UserRepository>().ReadSingle(x=>x.Id==userId);
			if (user.Password == oldPassword){
				user.Password = newPassword;
				Repository<UserRepository>().UpdateSingle(user);
				result = Tuple.Create(true,"密码修改成功！");
			}
            return result;
        }

		public void SaveAvatar(long userId, Stream imageStream) {
			//var path = Stream.
		}

		public void DeleteAvatar(long userId) {
			var user = Repository<UserRepository>().ReadSingle(x => x.Id == userId);
			if (user != null)
			{
				user.Items = "";
				Repository<UserRepository>().UpdateSingle(user);
            } 
		}

		public bool Exists(string username) {
			bool flag = false;
			flag = Repository<UserRepository>().ReadSingle(x => x.Username == username) != null;
			return flag;
		}
		public Tuple<bool, string> UserRegedit(Entity.User user) {
			Tuple<bool,string> result = Tuple.Create(false,"注册失败，用户名已经存在！");
			if (Repository<UserRepository>().ReadSingle(x => x.Username == user.Username) == null) { 
				Repository<UserRepository>().AddSingle(user);
				result = Tuple.Create(true,"注册成功！");
            }
			return result;
		}
		public void ResetPassword(string useName) {
			var user = Repository<UserRepository>().ReadSingle(x => x.Username == useName);
			if(user!=null){
				user.Password = user.Username;
				Repository<UserRepository>().UpdateSingle(user);
            }
        }
    }
}
