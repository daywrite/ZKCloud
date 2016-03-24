using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using ZKCloud.Container;
using ZKCloud.Domain.Repositories;
using ZKCloud.Web.Mvc;
using ZKCloud.Web.Apps.Demo01.Domain.Models;
using ZKCloud.Web.Apps.Demo01.Domain.Repositories;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ZKCloud.Web.Apps.Demo01.Controllers {
	[App("demo01")]
	public class TestController : BaseController {
		// GET: /<controller>/
		public IActionResult Index() {
			string result = "demo01::test::index called.\n";
			var list = Resolve<TestDataRepository>().ReadMany(e => e.Id < 1000);
			result += string.Join("", list.Select(e => $"id:{e.Id}, name:{e.Name}\n").ToArray());
			return View((object)result);
		}

		public IActionResult Add() {
			Resolve<TestDataRepository>().AddSingle(new TestData()
			{
				Name = "aaaaaaaaaaaa"
			});
			return Content("demo01::test::testadd called.");
		}

		public IActionResult TestView() {
			return View();
		}

		public IActionResult Login() {
			return View();
		}
	}
}
