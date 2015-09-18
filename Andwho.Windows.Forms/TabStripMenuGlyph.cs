using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Andwho.Windows.Forms
{
    internal class TabStripMenuGlyph
    {
        #region 变量
        private Rectangle _bounds = Rectangle.Empty;

        private EMouseState _mouseState = EMouseState.Normal;

        private ToolStripProfessionalRenderer _renderer;
        #endregion

        #region 构造函数
        internal TabStripMenuGlyph() { }
        internal TabStripMenuGlyph(ToolStripProfessionalRenderer renderer)
        {
            this._renderer = renderer;
        }
        internal TabStripMenuGlyph(Rectangle bounds, EMouseState mouseState)
        {
            this._bounds = bounds;
            this._mouseState = mouseState;
        }
        #endregion

        #region 属性
        /// <summary>
        /// 当前鼠标状态
        /// </summary>
        public EMouseState MouseState
        {
            get { return this._mouseState; }
            set { this._mouseState = value; }
        }

        /// <summary>
        /// 菜单按钮的区域
        /// </summary>
        public Rectangle Bounds
        {
            get { return this._bounds; }
            set { this._bounds = value; }
        }
        #endregion

        #region 方法
        /// <summary>
        /// 绘制菜单图型
        /// </summary>
        /// <param name="g"></param>
        public void DrawGlyph(Graphics g)
        {
            if (this._mouseState == EMouseState.Move)
            {
                Color fill = this._renderer.ColorTable.ButtonSelectedHighlight;
                using (Brush brush = new SolidBrush(fill))
                {
                    g.FillRectangle(brush, this._bounds);
                }
                Rectangle borderRect = this._bounds;
                borderRect.Width--;
                borderRect.Height--;

                g.DrawRectangle(SystemPens.Highlight, borderRect);
            }
            SmoothingMode bak = g.SmoothingMode;

            using (Pen pen = new Pen(Color.Black))
            {
                pen.Width = 2;
                g.DrawLine(pen,
                    new Point(this._bounds.Left + (this._bounds.Width / 3) - 2, this._bounds.Height / 2 - 1),
                    new Point(this._bounds.Right - (this._bounds.Width / 3), this._bounds.Height / 2 - 1));
            }
            g.FillPolygon(Brushes.Black, new Point[]
            {
                new Point(this._bounds.Left + (this._bounds.Width / 3) - 2, this._bounds.Height / 2+ 2),
                new Point(this._bounds.Right - (this._bounds.Width / 3), this._bounds.Height / 2 + 2),
                new Point(this._bounds.Left + this._bounds.Width / 2-1, this._bounds.Bottom -4)
            });
            g.SmoothingMode = bak;
        }
        #endregion
    }
}
