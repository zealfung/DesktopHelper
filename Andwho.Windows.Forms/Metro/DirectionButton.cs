using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using Andwho.HX.Enums;

namespace Andwho.Windows.Forms.Metro
{
    /// <summary>
    /// 方向按钮
    /// </summary>
    public class DirectionButton : Control
    {
        #region 变量
        /// <summary>
        /// 上下按钮的默认大小
        /// </summary>
        private Size _defaultSize = new Size(80, 20);

        private EMouseState _mouseState = EMouseState.Normal;
        /// <summary>
        /// 箭头所指向的方向
        /// </summary>
        private EDirection _direction = EDirection.Up;
        #endregion

        #region 构造函数

        /// <summary>
        /// 用默认设置初始化 Andwho.Windows.Forms.DirectionButton 类的新实例。
        /// </summary>
        public DirectionButton()
        {
            this.SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.SupportsTransparentBackColor |
                ControlStyles.OptimizedDoubleBuffer, true);
            this.UpdateStyles();
        }

        #endregion

        #region 属性
        /// <summary>
        /// 控件的默认大小
        /// </summary>
        protected override Size DefaultSize
        {
            get { return this._defaultSize; }
        }

        /// <summary>
        /// 针对于控件的鼠标状态
        /// </summary>
        protected EMouseState MouseState
        {
            get { return this._mouseState; }
            set
            {
                this._mouseState = value;
                this.Invalidate();
            }
        }

        /// <summary>
        /// 指定箭头所指向的方向
        /// </summary>
        [Browsable(true)]
        public EDirection Direction
        {
            get { return this._direction; }
            set
            {
                this._direction = value;
                this.Invalidate();
            }
        }
        #endregion

        #region 方法
        /// <summary>
        /// 绘制三角型
        /// </summary>
        /// <param name="g">画板，Graphics 对象</param>
        private void DrawTriangle(Graphics g)
        {
            //获取控件中心点和三角型所绘画的位置
            int tWidth = 11;
            int tHeight = 6;
            int hCenter = this.Width / 2;
            int vCenter = this.Height / 2 - 1;
            int x = hCenter - tWidth / 2;
            int y = vCenter - tHeight / 2;
            //构建三角型的路径
            Point[] pointArray = new Point[3];
            switch (this._direction)
            {
                case EDirection.Up:
                    pointArray[0] = new Point(hCenter, vCenter);
                    pointArray[1] = new Point(x + tWidth, vCenter + tHeight);
                    pointArray[2] = new Point(x, vCenter + tHeight);
                    break;
                case EDirection.Down:
                    pointArray[0] = new Point(x, vCenter);
                    pointArray[1] = new Point(x + tWidth, vCenter);
                    pointArray[2] = new Point(hCenter, vCenter + tHeight);
                    break;
                case EDirection.Left:
                    break;
                case EDirection.Right:
                    break;
            }
            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddLines(pointArray);
                path.CloseFigure();
                //using (Brush brush = new SolidBrush(this._foreColor))
                //{
                //    g.FillPath(brush, path);
                //}
            }

            //Point point1 = new Point(0, 0);
            //Point point2 = new Point(11, 0);
            //Point point3 = new Point(5, 8);
            //Point[] pointArray = { point1, point2, point3 };

            //g.FillPolygon(Brushes.White, pointArray);
        }
        #endregion

        #region Override Methods
        /// <summary>
        /// 引发 System.Windows.Forms.Control.Paint 事件。
        /// </summary>
        /// <param name="e">包含事件数据的 System.Windows.Forms.PaintEventArgs。</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            DrawTriangle(e.Graphics);
        }

        /// <summary>
        /// 引发 System.Windows.Forms.Control.MouseMove 事件。
        /// </summary>
        /// <param name="e">包含事件数据的 System.Windows.Forms.MouseEventArgs。</param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            this.MouseState = EMouseState.Move;
        }

        /// <summary>
        /// 引发 System.Windows.Forms.Control.MouseDown 事件。
        /// </summary>
        /// <param name="e">包含事件数据的 System.Windows.Forms.MouseEventArgs。</param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            this.MouseState = EMouseState.Down;
        }

        /// <summary>
        /// 引发 System.Windows.Forms.Control.MouseUp 事件。
        /// </summary>
        /// <param name="e">包含事件数据的 System.Windows.Forms.MouseEventArgs。</param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            this.MouseState = EMouseState.Up;
        }

        /// <summary>
        /// 引发 System.Windows.Forms.Control.MouseLeave 事件。
        /// </summary>
        /// <param name="e">包含事件数据的 System.EventArgs。</param>
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            this.MouseState = EMouseState.Leave;
        }
        #endregion
    }
}
