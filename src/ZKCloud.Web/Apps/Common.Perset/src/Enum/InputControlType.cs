using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZKCloud.Web.Apps.Common.Perset.src.Enum
{
    public enum InputControlType
    {
        /// <summary>
		/// Label类型
		/// </summary>
        Label = 0,
        /// <summary>
        /// 文本框
        /// </summary>
        TextBox = 1,
        /// <summary>
        /// 多行文本
        /// </summary>
        TextArea = 2,
        /// <summary>
        /// 多选框
        /// </summary>
        CheckBox = 3,
        /// <summary>
        /// 单选框
        /// </summary>
        RadioButton = 4,
        /// <summary>
        /// 下拉列表
        /// </summary>
        DropdownList = 5,
        /// <summary>
        /// 数字框
        /// </summary>
        NumberTextBox = 6,
        /// <summary>
        /// HTMl文本
        /// </summary>
        HtmlEditBox = 7,
        /// <summary>
        /// 时间框
        /// </summary>
        DateTimePicker = 8,
        /// <summary>
        /// 图片类型
        /// </summary>
        AlbumUploder = 9,
        /// <summary>
        ///附件类型
        /// </summary>
        FileUploder = 10,
        /// <summary>
        ///密码类型
        /// </summary>
        Password = 11,
        /// <summary>
        ///隐藏类型
        /// </summary>
        Hidden = 12,
        /// <summary>
        /// 可编辑的下拉列表
        /// </summary>
        EditableDropdownList = 13,
    }
}
