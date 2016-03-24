using System;
using Microsoft.AspNet.Mvc;

namespace ZKCloud.Web.Mvc {
	public static class AppUrlHelperExtensions {
		public static string AppContent(this IUrlHelper url, string contentPath,string appName=null) {
			if (url is AppUrlHelper)
				return (url as AppUrlHelper).AppContent(contentPath, appName);
			else
				return url.Content(contentPath);
		}

		/// <summary>
		/// 获取App静态文件下下面的路径
		/// </summary>
		/// <param name="url"></param>
		/// <param name="staticCotnentPath"></param>
		/// <returns></returns>
		public static string AppStaticContent(this IUrlHelper url, string staticCotnentPath) {
			if (staticCotnentPath == null)
				throw new ArgumentNullException(nameof(staticCotnentPath));
			if (staticCotnentPath.StartsWith("~"))
				staticCotnentPath = staticCotnentPath.Substring(1);
			while ((staticCotnentPath.StartsWith("/") || staticCotnentPath.StartsWith("\\")) && staticCotnentPath.Length > 1)
				staticCotnentPath = staticCotnentPath.Substring(1);
			return url.AppContent($"~/template/static/{staticCotnentPath}");
		}

		/// <summary>
		/// 获取App模板下面的路径
		/// template下路径
		/// </summary>
		/// <param name="url"></param>
		/// <param name="staticCotnentPath"></param>
		/// <returns></returns>
		public static string AppTemplateContent(this IUrlHelper url, string staticCotnentPath) {
			if (staticCotnentPath == null)
				throw new ArgumentNullException(nameof(staticCotnentPath));
			if (staticCotnentPath.StartsWith("~"))
				staticCotnentPath = staticCotnentPath.Substring(1);
			while ((staticCotnentPath.StartsWith("/") || staticCotnentPath.StartsWith("\\")) && staticCotnentPath.Length > 1)
				staticCotnentPath = staticCotnentPath.Substring(1);
			return url.AppContent($"~/template/{staticCotnentPath}");
		}

		/// <summary>
		/// 获取App应用下面的css路径
		/// css必须存方到template/static/css目录下面
		/// </summary>
		/// <param name="url"></param>
		/// <param name="staticCotnentPath">css名称</param>
		/// <param name="appName">App名称</param>
		/// <returns></returns>
		public static string AppCssContent(this IUrlHelper url, string cssCotnentPath, string appName=null) {
			if (cssCotnentPath == null)
				throw new ArgumentNullException(nameof(cssCotnentPath));
			if (cssCotnentPath.StartsWith("~"))
				cssCotnentPath = cssCotnentPath.Substring(1);
			while ((cssCotnentPath.StartsWith("/") || cssCotnentPath.StartsWith("\\")) && cssCotnentPath.Length > 1)
				cssCotnentPath = cssCotnentPath.Substring(1);
			return url.AppContent($"~/template/static/css/{cssCotnentPath}", appName);
		}


		/// <summary>
		/// 获取App应用下面的js路径
		/// css必须存方到template/static/js目录下面
		/// </summary>
		/// <param name="url"></param>
		/// <param name="staticCotnentPath">css名称</param>
		/// <param name="appName">App名称</param>
		/// <returns></returns>
		public static string AppJsContent(this IUrlHelper url, string jsCotnentPath, string appName = null) {
			if (jsCotnentPath == null)
				throw new ArgumentNullException(nameof(jsCotnentPath));
			if (jsCotnentPath.StartsWith("~"))
				jsCotnentPath = jsCotnentPath.Substring(1);
			while ((jsCotnentPath.StartsWith("/") || jsCotnentPath.StartsWith("\\")) && jsCotnentPath.Length > 1)
				jsCotnentPath = jsCotnentPath.Substring(1);
			return url.AppContent($"~/template/static/css/{jsCotnentPath}", appName);
		}

		/// <summary>
		/// 获取App应用下面的图片路径
		/// css必须存方到template/static/images目录下面
		/// </summary>
		/// <param name="url"></param>
		/// <param name="staticCotnentPath">css名称</param>
		/// <param name="appName">App名称</param>
		/// <returns></returns>
		public static string AppImageContent(this IUrlHelper url, string imgCotnentPath, string appName = null) {
			if (imgCotnentPath == null)
				throw new ArgumentNullException(nameof(imgCotnentPath));
			if (imgCotnentPath.StartsWith("~"))
				imgCotnentPath = imgCotnentPath.Substring(1);
			while ((imgCotnentPath.StartsWith("/") || imgCotnentPath.StartsWith("\\")) && imgCotnentPath.Length > 1)
				imgCotnentPath = imgCotnentPath.Substring(1);
			return url.AppContent($"~/template/static/images/{imgCotnentPath}", appName);
		}
	}
}
