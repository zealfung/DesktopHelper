using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace Andwho.Windows.Forms.Metro
{
    /// <summary>
    /// Metro 风格的右键菜单
    /// </summary>
    public class MetroToolStripRenderer : ToolStripRenderer
    {
        #region 变量

        /// <summary>
        /// 边框颜色
        /// </summary>
        private Color _borderColor = Color.FromArgb(51, 51, 55);
        /// <summary>
        /// 背景颜色
        /// </summary>
        private Color _backColor = Color.FromArgb(27, 27, 28);
        /// <summary>
        /// 文字颜色
        /// </summary>
        private Color _textColor = Color.White;

        #endregion

        #region 构造函数
        /// <summary>
        /// 
        /// </summary>
        public MetroToolStripRenderer()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="backColor"></param>
        public MetroToolStripRenderer(Color backColor)
            : this()
        {
            this._backColor = backColor;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="borderColor"></param>
        /// <param name="backColor"></param>
        public MetroToolStripRenderer(Color borderColor, Color backColor)
            : this(backColor)
        {
            this._borderColor = borderColor;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="borderColor"></param>
        /// <param name="backColor"></param>
        /// <param name="textColor"></param>
        public MetroToolStripRenderer(Color borderColor, Color backColor, Color textColor)
            : this(borderColor, backColor)
        {
            this._textColor = textColor;
        }
        #endregion

        #region Override Methods

        /// <summary>
        /// 绘制菜单背景
        /// </summary>
        /// <param name="e"></param>
        protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle bounds = e.AffectedBounds;
            Rectangle rect = new Rectangle(bounds.X, bounds.Y, bounds.Width - 1, bounds.Height - 1);
            
            using (Brush brush = new SolidBrush(this._backColor))
            {
                g.FillRectangle(brush, rect);
            }
            using (Pen pen = new Pen(this._borderColor))
            {
                g.DrawRectangle(pen, rect);
            }
            //base.OnRenderToolStripBackground(e);
        }

        /// <summary>
        /// 隐藏父类中绘制的边框
        /// </summary>
        /// <param name="e"></param>
        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
        {
            //base.OnRenderToolStripBorder(e);
        }

        /// <summary>
        /// 绘制鼠标划过时的样式
        /// </summary>
        /// <param name="e"></param>
        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            Graphics g = e.Graphics;
            ToolStripItem item = e.Item;
            ToolStrip toolstrip = e.ToolStrip;

            if (toolstrip is ToolStrip)
            {
                g.SmoothingMode = SmoothingMode.HighQuality;
                //绘制选中项
                if (item.Enabled && item.Selected)
                {
                    item.ForeColor = Color.White;
                    Rectangle rect = new Rectangle(
                        item.ContentRectangle.X + 1,
                        item.ContentRectangle.Y,
                        item.ContentRectangle.Width - 1,
                        item.ContentRectangle.Height);
                    using (Brush brush = new SolidBrush(this._borderColor))
                    {
                        g.FillRectangle(brush, rect);
                    }
                }
                else
                {
                    item.ForeColor = Color.Black;
                }
            }
            else
            {
                base.OnRenderMenuItemBackground(e);
            }
        }

        /// <summary>
        /// 水平线
        /// </summary>
        /// <param name="e"></param>
        protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
        {
            if (!e.Vertical)
            {
                using (Pen pen = new Pen(this._borderColor))
                {
                    Rectangle rect = e.Item.ContentRectangle;
                    e.Graphics.DrawLine(pen, rect.X + 25, rect.Y, rect.Width, rect.Height);
                }
            }
            else
            {
                base.OnRenderSeparator(e);
            }
        }

        /// <summary>
        /// 菜单上的文字
        /// </summary>
        /// <param name="e"></param>
        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            e.TextColor = Color.White;
            base.OnRenderItemText(e);
        }
        #endregion
    }
}
