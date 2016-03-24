using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ZKCloud.Web.Apps.Common.User.src.Model {
 
    public class LoginViewModel
    {
        [Required]
		[DataType(DataType.Text)]
		[Display(Name = "用户名")]
		public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
		[Display(Name = "密码")]
		public string Password { get; set; }

        [Display(Name = "记住密码")]
        public bool RememberMe { get; set; }
		[Display(Name = "验证码")]
		public string CaptCha { get; set; }
    }
}
