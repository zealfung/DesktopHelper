using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Text;
using System.Windows.Forms;
using Andwho.Windows.Resource;
using Andwho.Windows.Win32;

namespace Andwho.Windows.Forms
{
    /// <summary>
    /// 
    /// </summary>
    public class QQForm : FormBase
    {
        #region 变量
        /// <summary>
        /// 边框图片
        /// </summary>
        private Image _borderImage = AssemblyHelper.GetImage("QQ.FormFrame.fringe_bkg.png");
        #endregion

        #region 构造函数
        /// <summary>
        /// 
        /// </summary>
        public QQForm()
            :base()
        {
            
        }

        #endregion

        #region 属性
        /// <summary>
        /// 
        /// </summary>
        protected override Rectangle MaxRect
        {
            get { return new Rectangle(this.Width - this.CloseRect.Width - 28, -1, 28, 20); }
        }
        /// <summary>
        /// 
        /// </summary>
        protected override Rectangle MiniRect
        {
            get
            {
                int x = this.Width - this.CloseRect.Width - this.MaxRect.Width - 28;
                Rectangle rect = new Rectangle(x, -1, 28, 20);
                return rect;
                //return new Rectangle(this.Width - this.CloseRect.Width - 28, -1, 28, 20);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        protected override Rectangle SysBtnRect
        {
            get
            {
                if (base._sysButton == ESysButton.Normal)
                    return new Rectangle(this.Width - 28 * 2 - 39, 0, 39 + 28 + 28, 20);
                else if (base._sysButton == ESysButton.Close_Mini)
                    return new Rectangle(this.Width - 28 - 39, 0, 39 + 28, 20);
                else
                    return this.CloseRect;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        protected override Rectangle CloseRect
        {
            get { return new Rectangle(this.Width - 39, -1, 39, 20); }
        }
        
        #endregion

        #region 方法
        /// <summary>
        /// 绘画按钮
        /// </summary>
        /// <param name="g">画板</param>
        /// <param name="mouseState">鼠标状态</param>
        /// <param name="rect">按钮区域</param>
        /// <param name="str">图片字符串</param>
        private void DrawButton(Graphics g, EMouseState mouseState, Rectangle rect, string str)
        {
            switch (mouseState)
            {
                case EMouseState.Normal:
                    g.DrawImage(AssemblyHelper.GetImage("QQ.SysButton.btn_" + str + "_normal.png"), rect);
                    break;
                case EMouseState.Move:
                case EMouseState.Up:
                    g.DrawImage(AssemblyHelper.GetImage("QQ.SysButton.btn_" + str + "_highlight.png"), rect);
                    break;
                case EMouseState.Down:
                    g.DrawImage(AssemblyHelper.GetImage("QQ.SysButton.btn_" + str + "_down.png"), rect);
                    break;
            }
        }
        #endregion

        #region Override Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            //绘画系统控制按钮
            switch (base.SysButton)
            {
                case ESysButton.Normal:
                    this.DrawButton(g, base.MinState, this.MiniRect, "mini");
                    this.DrawButton(g, base.CloseState, this.CloseRect, "close");
                    if (this.WindowState == FormWindowState.Maximized)
                        this.DrawButton(g, base.MaxState, this.MaxRect, "restore");
                    else
                        this.DrawButton(g, base.MaxState, this.MaxRect, "max");
                    break;
                case ESysButton.Close:
                    this.DrawButton(g, base.CloseState, this.CloseRect, "close");
                    break;
                case ESysButton.Close_Mini:
                    //this.DrawButton(g, base.MinState, this.MiniRect, "mini");
                    this.DrawButton(g, base.MaxState, this.MaxRect, "mini");
                    this.DrawButton(g, base.CloseState, this.CloseRect, "close");
                    break;
            }

            //绘画边框
            g.DrawImage(this._borderImage, new Rectangle(0, 0, 10, 10), new Rectangle(5, 5, 10, 10), GraphicsUnit.Pixel);//左上角
            g.DrawImage(this._borderImage, new Rectangle(0, -5, 10, this.Height + 10), new Rectangle(5, 5, 10, this._borderImage.Height - 10), GraphicsUnit.Pixel);//左边框
            g.DrawImage(this._borderImage, new Rectangle(-5, this.Height - 10, 10, 10), new Rectangle(0, this._borderImage.Height - 15, 10, 10), GraphicsUnit.Pixel);//左下角
            g.DrawImage(this._borderImage, new Rectangle(this.Width - 9, -5, 10, 10), new Rectangle(20, 0, 10, 10), GraphicsUnit.Pixel);//右上角
            g.DrawImage(this._borderImage, new Rectangle(this.Width - 9, -5, 10, this.Height + 10), new Rectangle(20, 5, 10, this._borderImage.Height - 10), GraphicsUnit.Pixel);//右边框
            g.DrawImage(this._borderImage, new Rectangle(this.Width - 9, this.Height - 10, 10, 10), new Rectangle(20, this._borderImage.Height - 15, 10, 10), GraphicsUnit.Pixel);//右下角

            g.DrawImage(this._borderImage, new Rectangle(5, -5, this.Width - 10, 18), new Rectangle(12, 0, 6, 18), GraphicsUnit.Pixel);
            g.DrawImage(this._borderImage, new Rectangle(5, this.Height - 6, this.Width - 10, 18), new Rectangle(12, 0, 6, 18), GraphicsUnit.Pixel);

            base.OnPaint(e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            //调用API，将窗体剪成圆角
            int rgn = NativeMethods.CreateRoundRectRgn(0, 0, this.Width + 1, this.Height + 1, 4, 4);
            NativeMethods.SetWindowRgn(this.Handle, rgn, true);
        }

        #endregion

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // QQForm
            // 
            this.ClientSize = new System.Drawing.Size(300, 300);
            this.Name = "QQForm";
            this.Padding = new System.Windows.Forms.Padding(3, 26, 3, 3);
            this.ResumeLayout(false);

        }
    }
}
