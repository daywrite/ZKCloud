using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ZKCloud.Web.Apps.Common.User.src.Model {
	/// <summary>
	/// 注册Model
	/// </summary>
	public class RegModel {
		/// <summary>
		/// 用户名
		/// </summary>  
		[Required]
		[StringLength(100, MinimumLength = 3)]
		[DataType(DataType.Text )]
		[Display(Name = "用户名")]
		public string Username { get; set; }
		/// <summary>
		/// 密码
		/// </summary>
		[Required]
		[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
		[DataType(DataType.Password)]
		[Display(Name = "密码")]
		public string Password { get; set; }
		/// <summary>
		/// 确认密码
		/// </summary>
		[DataType(DataType.Password)]
		[Display(Name = "确认密码")]
		[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
		public string ConfirmPassword { get; set; }
	}
}
