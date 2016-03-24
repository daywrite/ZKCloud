
using System;
//using System.Drawing;
//using System.Drawing.Drawing2D;
//using System.Drawing.Imaging;
using System.IO;

namespace ZKCloud.Utils.Functions {
	/// <summary>
	/// 验证码管理器
	/// </summary>

	public class CaptchaManager {
		/// <summary>
		/// 默认的验证码位数
		/// </summary>
		public const int DefaultDigits = 4;
		/// <summary>
		/// 生成验证码后最小保留会话的时间
		/// </summary>
		public const int MakeSessionAliveAtLeast = 30;
		/// <summary>
		/// 保存到会话时使用的键名前缀
		/// </summary>
		public const string SessionItemKeyPrefix = "Common.Base.Captcha.";

		/// <summary>
		/// 生成验证码 
		/// </summary>
		public virtual string Generate(string key, int digits = DefaultDigits, string chars = null) {
			// 生成验证码
			var captchaCode = RandomUtils.RandomString(digits, "23456789ABCDEFGHJKLMNPQRSTUWXYZ");
			return captchaCode;
			#region 暂时备注使用.net451的实现代码
			//var image = new Bitmap(digits * 20, 32);
			//var imageRect = new Rectangle(0, 0, image.Width, image.Height);
			//var font = new Font("Arial", 20, FontStyle.Regular);
			//var brush = new SolidBrush(Color.Black);
			//using (var graphic = Graphics.FromImage(image)) {
			//	// 描画背景
			//	HatchStyle backgroundStyle = (HatchStyle)RandomUtils.Generator.Next(18, 52);
			//	while (!Enum.IsDefined(typeof(HatchStyle), backgroundStyle)) {
			//		backgroundStyle = (HatchStyle)RandomUtils.Generator.Next(18, 52);
			//	}
			//	graphic.FillRectangle(
			//		new HatchBrush(backgroundStyle, Color.Gray, Color.White), imageRect);
			//	// 描画文本，然后不规则拉伸
			//	GraphicsPath path = new GraphicsPath();
			//	path.AddString(captchaCode, font.FontFamily, (int)font.Style, font.Size, imageRect,
			//		new StringFormat()
			//		{
			//			Alignment = StringAlignment.Center,
			//			LineAlignment = StringAlignment.Center
			//		});
			//	int padding = 5;
			//	Func<int> randomPadding = () => RandomUtils.Generator.Next(padding);
			//	var points = new PointF[] {
			//			new PointF(randomPadding(), randomPadding()),
			//			new PointF(image.Width - randomPadding(), randomPadding()),
			//			new PointF(randomPadding(), image.Height - randomPadding()),
			//			new PointF(image.Width - randomPadding(), image.Height - randomPadding()),
			//		};
			//	path.Warp(points, imageRect);
			//	graphic.FillPath(
			//		new HatchBrush(HatchStyle.LargeConfetti, Color.Black), path);
			//}

			//MemoryStream stream = new MemoryStream();
			//image.Save(stream, ImageFormat.Jpeg);
			////输出图片流
			////return stream.ToArray();
			//return Tuple.Create(stream.ToArray(), captchaCode);
			#endregion

		}

	}
}
