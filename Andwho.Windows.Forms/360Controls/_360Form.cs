using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using Andwho.Windows.Win32;
using Andwho.Windows.Helper;
using Andwho.Windows.Resource;

namespace Andwho.Windows.Forms
{
    /// <summary>
    /// 360 窗体
    /// </summary>
    public class _360Form : FormBase
    {
        #region 变量

        #region 资源图片

        /// <summary>
        /// 边框图片
        /// </summary>
        private Image _borderImage = AssemblyHelper.GetImage("_360.Form.framemod.png");
        /// <summary>
        /// 关闭按钮图片
        /// </summary>
        private Image _closeImage = AssemblyHelper.GetImage("_360.SysButton.sys_button_close.png");
        /// <summary>
        /// 最小化按钮图片
        /// </summary>
        private Image _minImage = AssemblyHelper.GetImage("_360.SysButton.sys_button_min.png");
        /// <summary>
        /// 最大化按钮图片
        /// </summary>
        private Image _maxImage = AssemblyHelper.GetImage("_360.SysButton.sys_button_max.png");
        /// <summary>
        /// 还原按钮图片
        /// </summary>
        private Image _restoreImage = AssemblyHelper.GetImage("_360.SysButton.sys_button_restore.png");
        /// <summary>
        /// 标题栏菜单按钮图片
        /// </summary>
        private Image _titleBarMenuImage = AssemblyHelper.GetImage("_360.SysButton.title_bar_menu.png");

        #endregion

        /// <summary>
        /// 系统按钮与窗体右边边缘的间距
        /// </summary>
        private int _sysButtonPos = 4;
        /// <summary>
        /// 标题栏菜单按钮的鼠标状态
        /// </summary>
        private EMouseState _titleBarMenuState = EMouseState.Normal;

        #endregion

        #region 构造函数
        /// <summary>
        /// 实例化 Andwho.Windows.Forms._360form 新的实例。
        /// </summary>
        public _360Form()
            : base()
        {

        }

        #endregion

        #region 属性
        /// <summary>
        /// 系统控制按钮与右边框之间的距离
        /// </summary>
        [Description("系统控制按钮与右边框之间的距离")]
        public int SysButtonPos
        {
            get { return this._sysButtonPos; }
            set
            {
                this._sysButtonPos = value;
                this.Invalidate(this.SysBtnRect);
            }
        }
        /// <summary>
        /// 关闭按钮的矩形区域
        /// </summary>
        protected override Rectangle CloseRect
        {
            get
            {
                int width = this._closeImage.Width / 4;
                int height = this._closeImage.Height;
                int x = this.Width - width - this._sysButtonPos;
                int y = -1;

                return new Rectangle(x, y, width, height);
            }
        }
        /// <summary>
        /// 最大化按钮的矩形区域
        /// </summary>
        protected override Rectangle MaxRect
        {
            get
            {
                int width = this._maxImage.Width / 4;
                int height = this._maxImage.Height;
                int x = 0;
                int y = this.CloseRect.Y;
                switch (base.SysButton)
                {
                    case ESysButton.Normal:
                        x = this.Width - width - this.CloseRect.Width - this._sysButtonPos;
                        break;
                }
                return new Rectangle(x, y, width, height);
            }
        }
        /// <summary>
        /// 最小化按钮的矩形区域
        /// </summary>
        protected override Rectangle MiniRect
        {
            get
            {
                int width = this._minImage.Width / 4;
                int height = this._minImage.Height;
                int x = 0;
                int y = this.CloseRect.Y;
                switch (base.SysButton)
                {
                    case ESysButton.Normal:
                        x = this.Width - width - this.CloseRect.Width - this.MaxRect.Width - this._sysButtonPos;
                        break;
                    case ESysButton.Close_Mini:
                        x = this.Width - width - this.CloseRect.Width - this._sysButtonPos;
                        break;
                }

                return new Rectangle(x, y, width, height);
            }
        }
        /// <summary>
        /// 系统按钮的矩形区域
        /// </summary>
        protected override Rectangle SysBtnRect
        {
            get
            {
                if (base._sysButton == ESysButton.Normal)
                {
                    int x = this.TitleBarMenuRect.X;
                    int y = this.CloseRect.Y;
                    int width = this.CloseRect.Width + this.MaxRect.Width +
                        this.MiniRect.Width + this.TitleBarMenuRect.Width - this._sysButtonPos;
                    int height = this.CloseRect.Height;
                    return new Rectangle(x, y, width, height);
                }
                else if (base._sysButton == ESysButton.Close_Mini)
                {
                    int x = this.TitleBarMenuRect.X;
                    int y = this.CloseRect.Y;
                    int width = this.CloseRect.Width + this.MiniRect.Width +
                        this.TitleBarMenuRect.Width - this._sysButtonPos;
                    int height = this.CloseRect.Height;
                    return new Rectangle(x, y, width, height);
                }
                else
                {
                    int x = this.TitleBarMenuRect.X;
                    int y = this.CloseRect.Y;
                    int width = this.TitleBarMenuRect.Width + this.CloseRect.Width;
                    int height = this.CloseRect.Height;
                    return new Rectangle(x, y, width, height);
                }
            }
        }
        /// <summary>
        /// 标题栏菜单按钮的矩形区域
        /// </summary>
        protected virtual Rectangle TitleBarMenuRect
        {
            get
            {
                int width = this._titleBarMenuImage.Width / 4;
                int height = this._titleBarMenuImage.Height;
                int x = 0;
                int y = this.CloseRect.Y;
                switch (base._sysButton)
                {
                    case ESysButton.Normal:
                    case ESysButton.Close_Mini:
                        x = this.MiniRect.X - width;
                        break;
                    case ESysButton.Close:
                        x = this.CloseRect.X - width;
                        break;
                }
                return new Rectangle(x, y, width, height);
            }
        }
        /// <summary>
        /// 标题栏菜单按钮的鼠标的状态
        /// </summary>
        protected virtual EMouseState TitleBarMenuState
        {
            get { return this._titleBarMenuState; }
            set
            {
                this._titleBarMenuState = value;
                this.Invalidate(this.TitleBarMenuRect);
            }
        }
        #endregion

        #region 方法
        /// <summary>
        /// 绘制窗体的系统控制按钮
        /// </summary>
        /// <param name="g">画板</param>
        /// <param name="rect">按钮所在的区域</param>
        /// <param name="image">图片</param>
        /// <param name="state">鼠标状态</param>
        private void DrawSysButton(Graphics g, Rectangle rect, Image image, EMouseState state)
        {
            Rectangle imageRect = Rectangle.Empty;
            switch (state)
            {
                case EMouseState.Normal:
                case EMouseState.Leave:
                    imageRect = new Rectangle(0, 0, rect.Width, rect.Height);
                    break;
                case EMouseState.Move:
                case EMouseState.Up:
                    imageRect = new Rectangle(rect.Width, 0, rect.Width, rect.Height);
                    break;
                case EMouseState.Down:
                    imageRect = new Rectangle(rect.Width * 2, 0, rect.Width, rect.Height);
                    break;
            }
            g.DrawImage(image, rect, imageRect, GraphicsUnit.Pixel);
        }

        /// <summary>
        /// 绘制窗体边框
        /// </summary>
        /// <param name="g"></param>
        private void DrawFrameBorder(Graphics g)
        {
            Rectangle rect = this.ClientRectangle;
            int cut1 = 1;
            int cut2 = 5;
            //左上角
            g.DrawImage(this._borderImage, new Rectangle(rect.X, rect.Y, cut2, cut2), 0, 0, cut2, cut2, GraphicsUnit.Pixel);
            //上边
            g.DrawImage(this._borderImage, new Rectangle(rect.X + cut2, rect.Y, rect.Width - cut2 * 2, cut1), cut2, 0, this._borderImage.Width - cut2 * 2, cut2, GraphicsUnit.Pixel);
            //右上角
            g.DrawImage(this._borderImage, new Rectangle(rect.X + rect.Width - cut2, rect.Y, cut2, cut2), this._borderImage.Width - cut2, 0, cut2, cut2, GraphicsUnit.Pixel);
            //左边
            g.DrawImage(this._borderImage, new Rectangle(rect.X, rect.Y + cut2, cut1, rect.Height - cut2 * 2), 0, cut2, cut1, this._borderImage.Height - cut2 * 2, GraphicsUnit.Pixel);
            //左下角
            g.DrawImage(this._borderImage, new Rectangle(rect.X, rect.Y + rect.Height - cut2, cut2, cut2), 0, this._borderImage.Height - cut2, cut2, cut2, GraphicsUnit.Pixel);
            //右边
            g.DrawImage(this._borderImage, new Rectangle(rect.X + rect.Width - cut1, rect.Y + cut2, cut1, rect.Height - cut2 * 2), this._borderImage.Width - cut1, cut2, cut1, this._borderImage.Height - cut2 * 2, GraphicsUnit.Pixel);
            //右下角
            g.DrawImage(this._borderImage, new Rectangle(rect.X + rect.Width - cut2, rect.Y + rect.Height - cut2, cut2, cut2), this._borderImage.Width - cut2, this._borderImage.Height - cut2, cut2, cut2, GraphicsUnit.Pixel);
            //下边
            g.DrawImage(this._borderImage, new Rectangle(rect.X + cut2, rect.Y + rect.Height - cut1, rect.Width - cut2 * 2, cut1), cut2, this._borderImage.Height - cut1, this._borderImage.Width - cut2 * 2, cut1, GraphicsUnit.Pixel);
        }
        #endregion

        #region Override Methods
        /// <summary>
        /// 引发 System.Windows.Forms.Form.Paint 事件。
        /// </summary>
        /// <param name="e">包含事件数据的 System.Windows.Forms.PaintEventArgs。</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect = this.ClientRectangle;

            switch (base.SysButton)
            {
                case ESysButton.Normal:
                    this.DrawSysButton(g, this.CloseRect, this._closeImage, base.CloseState);
                    if (base.WindowState != FormWindowState.Maximized)
                        this.DrawSysButton(g, this.MaxRect, this._maxImage, base.MaxState);
                    else
                        this.DrawSysButton(g, this.MaxRect, this._restoreImage, base.MaxState);
                    this.DrawSysButton(g, this.MiniRect, this._minImage, base.MinState);
                    break;
                case ESysButton.Close:
                    this.DrawSysButton(g, this.CloseRect, this._closeImage, base.CloseState);
                    break;
                case ESysButton.Close_Mini:
                    this.DrawSysButton(g, this.CloseRect, this._closeImage, base.CloseState);
                    this.DrawSysButton(g, this.MiniRect, this._minImage, base.MinState);
                    break;
            }
            // 绘制标题栏菜单按钮
            this.DrawSysButton(g, this.TitleBarMenuRect, (Bitmap)this._titleBarMenuImage, this._titleBarMenuState);


            this.DrawFrameBorder(g);
            base.OnPaint(e);
        }
        /// <summary>
        /// 引发 System.Windows.Forms.Form.Resize 事件。
        /// </summary>
        /// <param name="e">包含事件数据的 System.EventArgs。</param>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            int rgn = NativeMethods.CreateRoundRectRgn(0, 0, this.Width + 1, this.Height + 1, 6, 6);
            NativeMethods.SetWindowRgn(this.Handle, rgn, true);
        }
        /// <summary>
        /// 引发 System.Windows.Forms.Form.MouseMove。
        /// </summary>
        /// <param name="e">包含事件数据的 System.Windows.Forms.MouseEventArgs。</param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (this.TitleBarMenuRect.Contains(e.Location) && this._titleBarMenuState != EMouseState.Down)
            {
                this.TitleBarMenuState = EMouseState.Move;
            }
        }
        /// <summary>
        /// 引发 System.Windows.Forms.Form.MouseDown。
        /// </summary>
        /// <param name="e">包含事件数据的 System.Windows.Forms.MouseEventArgs。</param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (this.TitleBarMenuRect.Contains(e.Location))
            {
                this.TitleBarMenuState = EMouseState.Down;
            }
        }
        /// <summary>
        /// 引发 System.Windows.Forms.Form.MouseUp。
        /// </summary>
        /// <param name="e">包含事件数据的 System.Windows.Forms.MouseEventArgs。</param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (this.TitleBarMenuRect.Contains(e.Location))
            {
                this.TitleBarMenuState = EMouseState.Up;
            }
        }
        /// <summary>
        /// 引发 System.Windows.Forms.Form.MouseLeave。
        /// </summary>
        /// <param name="e">包含事件数据的 System.EventArgs。</param>
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            this.TitleBarMenuState = EMouseState.Leave;
        }
        #endregion
    }
}
