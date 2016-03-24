using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using ZKCloud.Web.Mvc;
using Microsoft.AspNet.Http;
using ZKCloud.Web.Apps.Common.Base.src.Domains.Services;

namespace ZKCloud.Web.Apps.Common.Base.src.Domains {
	[App("Common.Base")]
	public class CommonController : BaseController {
		// GET: /<controller>/
		[HttpGet("index")]
		public IActionResult Index() {
			new ZKCloud.Web.Apps.Common.Perset.src.Entity.InitRegion().InitRegionData();

			var sessionId = HttpContext.Session.GetString("SessionId");
			if (sessionId == null)
			{
				return Redirect("/user/login");
			}

			return View();
		}
		/// <summary>
		/// 获取验证码
		/// </summary>
		/// <returns></returns> 
		[HttpGet("getCaptcha")]
		public IActionResult Captcha() {
			var query = HttpContext.Request.Query;
			var key = query["key"].ToString() ?? "CaptchaKey";
			var captchaServices = new CaptchaServices();
			var code = captchaServices.GetCaptcha(key,HttpContext.Session);
			return Content(code);
		}
	}
}
