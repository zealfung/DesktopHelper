using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace Andwho.Windows.Forms
{
    /// <summary>
    /// 表示 ToolBar 控件中的单个项。
    /// </summary>
    public class ToolItem
    {
        #region 属性
        /// <summary>
        /// Item 显示的图片
        /// </summary>
        public Image Image { get; set; }
        /// <summary>
        /// 获取或设置包含有关控件的数据的对象。 
        /// </summary>
        public object Tag { get; set; }
        /// <summary>
        /// Item 上显示的文字信息
        /// </summary>
        [DefaultValue("toolItem")]
        public string Text { get; set; }
        /// <summary>
        /// 当前 Item 在 ToolBar 中的 Rectangle
        /// </summary>
        internal Rectangle Rectangle { get; set; }
        /// <summary>
        /// Item 当前的鼠标状态
        /// </summary>
        internal EMouseState MouseState { get; set; }

        #endregion
    }
}
