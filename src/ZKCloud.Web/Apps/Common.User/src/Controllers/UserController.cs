using Microsoft.AspNet.Mvc;
using System;
using System.Threading.Tasks;
using ZKCloud.Web.Mvc;
using ViewUser = ZKCloud.Web.Apps.Common.User.src.Model.RegModel;
using ZKCloud.Web.Apps.Common.User.src.Services;
using ZKCloud.Web.Apps.Common.Base.src.Domains.Services;
namespace ZKCloud.Web.Apps.Common.User.src.Controllers {
	/// <summary>
	/// 用户控制器
	/// </summary>
	[App("Common.User")]
	public class UserController : BaseController {

		string captchaKey = "CommonUser";
		/// <summary>
		/// 用户登录页面
		/// </summary>
		/// <returns></returns>
		[HttpGet("user/login")]
		public IActionResult Login() {
			ViewData["captchaKey"] = captchaKey;
			return View();
		}
		[HttpPost("user/login")]
		public IActionResult Login(Model.LoginViewModel model) {
			IUserService service = new UserService();
			var captchaServices = new CaptchaServices();
			bool IsValidCaptcha = captchaServices.CheckCaptcha(captchaKey, model.CaptCha,HttpContext.Session);
			if (!IsValidCaptcha)
			{
				ModelState.AddModelError(string.Empty, "验证码错误！");
				return View(model);
			}
			if (service.Login(model.Username, model.Password, model.RememberMe,HttpContext.Session))
			{
				return Redirect("/Index");
			}
			else {
				ModelState.AddModelError(string.Empty, "用户名或密码错误！");
				return View(model);
			}
		}

		/// <summary>
		/// 注册页面
		/// </summary>
		/// <returns></returns>
		[HttpGet("user/reg")]
		public IActionResult Reg() {
			return View();
		}
		//
		// POST: /Account/Register
		[HttpPost("user/reg")]
		public async Task<IActionResult> Reg(ViewUser model) {
			IUserService service = new UserService();
			if (ModelState.IsValid)
			{
				var user = new Entity.User { Username = model.Username, Password = model.Password, CreateTime = DateTime.Now };
				var result = service.UserRegedit(user);
				if (result.Item1)
				{
					return Redirect("/user/login");
				}
				else {
					ModelState.AddModelError(string.Empty, result.Item2);
				}
			}
			return View(model);
		}
		/// <summary>
		/// 退出
		/// </summary>
		/// <returns></returns>
		[HttpGet("user/logout")]
		public IActionResult LogOut() {
			IUserService service = new UserService();
			service.Logout();
			return View();
		}

		/// <summary>
		/// 重置密码
		/// </summary>
		/// <returns></returns>
		[HttpGet("user/resetpassword")]
		public IActionResult ResetPassword() {
			return View();
		}
		[HttpPost]
		public IActionResult ResetPassword(string useName) {

			return View();
		}

		/// <summary>
		/// 用户中心首页
		/// </summary>
		/// <returns></returns>
		[HttpGet("user/index")]
		public IActionResult Index() {
			return View();
		}
        /// <summary>
		/// 用户中心首页
		/// </summary>
		/// <returns></returns>
		[HttpGet("admin/user/index")]
        public IActionResult adminIndex()
        {
            return View();
        }
    }
}
