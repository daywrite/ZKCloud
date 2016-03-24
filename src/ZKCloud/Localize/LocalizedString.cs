using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Threading.Tasks;
using ZKCloud.Container;

namespace ZKCloud.Localize
{
    public struct LocalizedString
    {
        public string Text { get; private set; }

        public LocalizedString(string text) {
            Text = text;
        }

        /// <summary>
		/// 获取翻译后的文本
		/// </summary>
		/// <param name="s"></param>
		public static implicit operator string(LocalizedString s) {
            return s.ToString();
        }

        /// <summary>
		/// 获取翻译后的文本
		/// </summary>
		/// <returns></returns>
		public override string ToString() {
            // 文本是空白时不需要翻译
            if (string.IsNullOrEmpty(Text)) {
                return Text ?? "";
            }
            // 获取当前线程的语言
            var cluture = CultureInfo.CurrentCulture;
            // 获取翻译提供器并进行翻译
            // 传入 {语言}-{地区}
            var providers = ContainerManager.Default.ResolveAll<ITranslateProvider>()
                .Where(p => p.CanTranslate(cluture.Name))
                .Reverse()
                .ToArray();
            // 翻译文本，先注册的后翻译
            foreach (var provider in providers) {
                var translated = provider.Translate(Text);
                if (translated != null) {
                    return translated;
                }
            }
            // 没有找到翻译，返回原有的文本
            return Text ?? "";
        }
    }
}
