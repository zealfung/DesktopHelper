using System;
using System.Drawing;
using System.Windows.Forms;

namespace Andwho.Windows.Forms
{
    internal class TabStripCloseButton
    {
        #region 变量
        private Rectangle _bounds = Rectangle.Empty;
        private EMouseState _mouseState = EMouseState.Normal;
        private ToolStripProfessionalRenderer _renderer = null;
        #endregion

        #region 构造函数
        public TabStripCloseButton() { }
        public TabStripCloseButton(ToolStripProfessionalRenderer renderer)
        {
            this._renderer = renderer;
        }
        public TabStripCloseButton(Rectangle bounds, EMouseState mouseState)
        {
            this._bounds = bounds;
            this._mouseState = mouseState;
        }
        #endregion

        #region 属性

        public Rectangle Bounds
        {
            get { return this._bounds; }
            set { this._bounds = value; }
        }

        public EMouseState MouseState
        {
            get { return this._mouseState; }
            set { this._mouseState = value; }
        }
        #endregion

        #region 方法
        public void DrawCross(Graphics g)
        {
            if (this._mouseState == EMouseState.Move)
            {
                Color fill = this._renderer.ColorTable.ButtonSelectedHighlight;
                using (Brush brush = new SolidBrush(fill))
                {
                    g.FillRectangle(brush, this._bounds);
                    Rectangle borderRect = this._bounds;

                    borderRect.Width--;
                    borderRect.Height--;

                    g.DrawRectangle(SystemPens.Highlight, borderRect);
                }
            }
            using (Pen pen = new Pen(Color.Black))
            {
                g.DrawLine(
                    pen,
                    this._bounds.Left + 3,
                    this._bounds.Top + 3,
                    this._bounds.Right - 5,
                    this._bounds.Bottom - 4);
                g.DrawLine(
                    pen,
                    this._bounds.Right - 5,
                    this._bounds.Top + 3,
                    this._bounds.Left + 3,
                    this._bounds.Bottom - 4);
            }
        }
        #endregion
    }
}
