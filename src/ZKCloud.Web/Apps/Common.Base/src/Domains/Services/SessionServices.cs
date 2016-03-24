using DryIoc;
using DryIocAttributes;
using System;
using ZKCloud.Web.Apps.Common.Base.src.Domains.Entitys;
using Microsoft.AspNet.Http;
using ZKCloud.Web.Apps.Common.Base.src.Domains.Repositories;
using ZKCloud.Container;
using ZKCloud.Web.Mvc;
using Microsoft.AspNet.Http.Features;

namespace ZKCloud.Web.Apps.Common.Base.src.Domains.Services {
	/// <summary>
	/// 会话管理器
	/// </summary>
	[ExportMany, SingletonReuse]
	public class SessionServices : BaseController {
		/// <summary>
		/// 用于HttpContext.Items时，储存Session对象
		/// 用于Cookies时，储存会话Id
		/// </summary>
		public const string SessionKey = "Common.Base.Session";

		public bool SetSession(Common.User.src.Entity.User user, ISession httpSession, bool rememberMe=false) {
			bool flag = false;
			var session = new Session();  
			session.ReleatedId = user.Id;
			session.Expires = DateTime.UtcNow.AddHours(1);
			session.RememberLogin = rememberMe;
            session.ReGenerateId();
			ContainerManager.Default.Resolve<SessionRepository>().AddSingle(session);
			httpSession.SetInt32("UserId", (int)user.Id);
			httpSession.SetString("Username", user.Username);
			httpSession.SetString("SessionId", session.Id.ToString());
			flag = true;
			return flag;
		}
		/// <summary>
		/// 获取当前Http请求对应的会话
		/// 当前没有会话时返回新的会话
		/// </summary>
		/// <returns></returns>
		public virtual Session GetSession() {
			// 从HttpContext中获取会话 
			// 因为一次请求中可能会调用多次GetSession，应该确保返回同一个对象
			string sessionId=HttpContext.Session.GetString("SessionId").ToString();
			var session = new Session();
			if (sessionId != null)
			{
				var id = Guid.Parse(sessionId);
				session = ContainerManager.Default.Resolve<SessionRepository>().ReadSingle(e => e.Id == id);  
			}  
			return session;
		}

		/// <summary>
		/// 添加或更新当前的会话
		/// 必要时发送Cookie到浏览器
		/// </summary>
		public virtual void SaveSession(Session newSession) {
			var session = ContainerManager.Default.Resolve<SessionRepository>().ReadSingle(e => e.Id == newSession.Id);
			if (session == null)
			{
				throw new NullReferenceException("session is null");
			}
			ContainerManager.Default.Resolve<SessionRepository>().UpdateSingle(newSession);
		}

		/// <summary>
		/// 删除当前会话
		/// 同时删除浏览器中的Cookie
		/// </summary>
		public virtual void RemoveSession() {
			var sessionId = Guid.Parse(HttpContext.Session.GetString("SessionId"));
			ContainerManager.Default.Resolve<SessionRepository>().Delete(e => e.Id == sessionId);
			HttpContext.Session.Remove("SessionId"); 
		}
	}
}
