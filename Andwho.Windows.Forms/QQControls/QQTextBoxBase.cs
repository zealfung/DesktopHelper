using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using Andwho.Windows.Win32;

namespace Andwho.Windows.Forms
{
    /// <summary>
    /// 
    /// </summary>
    [ToolboxBitmap(typeof(TextBox))]
    public class QQTextBoxBase : TextBox
    {
        #region 变量
        /// <summary>
        /// 水印文字
        /// </summary>
        private string _waterText = string.Empty;
        /// <summary>
        /// 水印文字的颜色
        /// </summary>
        private Color _waterColor = Color.DarkGray;
        #endregion

        #region 属性
        /// <summary>
        /// 
        /// </summary>
        [Description("水印文字"), Category("自定义属性")]
        public string WaterText
        {
            get { return this._waterText; }
            set
            {
                this._waterText = value;
                base.Invalidate();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Description("水印的颜色"), Category("自定义属性")]
        public Color WaterColor
        {
            get {return this._waterColor;}
            set
            {
                this._waterColor = value;
                base.Invalidate();
            }
        }
        #endregion

        #region 方法
        /// <summary>
        /// 绘制水印
        /// </summary>
        private void WmPaintWater(ref Message m)
        {
            using (Graphics g = Graphics.FromHwnd(base.Handle))
            {
                if (this.Text.Length == 0 &&
                    !string.IsNullOrEmpty(this._waterText) &&
                    !this.Focused)
                {
                    TextFormatFlags flags = TextFormatFlags.EndEllipsis | TextFormatFlags.VerticalCenter;
                    if (this.RightToLeft == System.Windows.Forms.RightToLeft.Yes)
                    {
                        flags |= TextFormatFlags.RightToLeft | TextFormatFlags.Right;
                    }
                    TextRenderer.DrawText(g, this._waterText, new Font("宋体", 8.5f), this.ClientRectangle, this._waterColor, flags);
                }
            }
        }
        #endregion

        #region Override Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == (int)WindowsMessage.WM_PAINT)
                this.WmPaintWater(ref m);//绘制水印
        }
        #endregion
    }
}
