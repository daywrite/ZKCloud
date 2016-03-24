using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZKCloud.Utils.Functions;
using ZKCloud.Web.Mvc;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Http.Features;

namespace ZKCloud.Web.Apps.Common.Base.src.Domains.Services
{
    public class CaptchaServices : BaseController {
		public string GetCaptcha(string key,ISession httpSession) {
			var captchaManager = new CaptchaManager();
			var generate = captchaManager.Generate(key);
			httpSession.SetString(key,generate);
			return generate;
        }
		public bool CheckCaptcha(string key,string code, ISession httpSession) {
			bool flag = false;
			string captcha = httpSession.GetString(key);
			httpSession.Remove(key);
			flag = (captcha == code);
			return flag;
		} 
	}
}
