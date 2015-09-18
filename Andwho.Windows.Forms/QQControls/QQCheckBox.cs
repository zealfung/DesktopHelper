using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Drawing.Imaging;
using Andwho.Windows.Resource;

namespace Andwho.Windows.Forms
{
    /// <summary>
    /// CheckBox
    /// </summary>
    [DefaultEvent("CheckedChanged")]
    [DefaultProperty("Checked")]
    [ToolboxBitmap(typeof(CheckBox))]
    public class QQCheckBox : CheckBox
    {
        #region 变量
        /// <summary>
        /// 当前的属标状态
        /// </summary>
        private EMouseState _mouseState = EMouseState.Normal;

        #endregion

        #region 构造函数
        /// <summary>
        /// 
        /// </summary>
        public QQCheckBox()
        {
            this.SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.Selectable |
                ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.Opaque, false);
            this.UpdateStyles();
            this.BackColor = Color.Transparent;
        }

        #endregion

        #region 属性

        /// <summary>
        /// 文本区域
        /// </summary>
        internal Rectangle TextRect
        {
            get { return new Rectangle(17, 0, this.Width - 17, this.Height); }
        }
        /// <summary>
        /// 图片显示区域
        /// </summary>
        internal Rectangle ImageRect
        {
            get { return new Rectangle(0, (this.Height - 17) / 2, 17, 17); }
        }
        /// <summary>
        /// 鼠标状态
        /// </summary>
        internal EMouseState MouseState
        {
            get { return this._mouseState; }
            set
            {
                this._mouseState = value;
                base.Invalidate();
            }
        }

        /// <summary>
        /// 重写CheckBox的Text属性
        /// </summary>
        [DefaultValue("QQCheckBox")]
        public override string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }
        #endregion

        #region Override 方法
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.AntiAlias;

            TextFormatFlags flags = TextFormatFlags.Left |
                                    TextFormatFlags.SingleLine |
                                    TextFormatFlags.VerticalCenter;
            Color color = this.Enabled ? this.ForeColor : Color.LightGray;
            TextRenderer.DrawText(e.Graphics, this.Text, this.Font, this.TextRect, color, flags);
            if (!base.Enabled)
            {
                switch (this.CheckState)
                {
                    case CheckState.Checked:
                        break;
                    case CheckState.Indeterminate:
                        break;
                    case CheckState.Unchecked:
                        Bitmap bitmap = new Bitmap(
                            AssemblyHelper.GetImage("QQ.CheckBox.normal.png"));
                        //bitmap = RGB2Gray(bitmap);
                        g.DrawImage(bitmap, this.ImageRect);
                        break;
                }
            }
            else
            {
                switch (this._mouseState)
                {
                    case EMouseState.Normal:
                    case EMouseState.Leave:
                        if (base.Checked)
                        {
                            using (Image normal = AssemblyHelper.GetImage("QQ.CheckBox.tick_normal.png"))
                            {
                                g.DrawImage(normal, this.ImageRect);
                            }
                        }
                        else
                        {
                            using (Image normal = AssemblyHelper.GetImage("QQ.CheckBox.normal.png"))
                            {
                                g.DrawImage(normal, this.ImageRect);
                            }
                        }
                        break;
                    case EMouseState.Down:
                    case EMouseState.Up:
                    case EMouseState.Move:
                        if (base.Checked)
                        {
                            using (Image high = AssemblyHelper.GetImage("QQ.CheckBox.tick_highlight.png"))
                            {
                                g.DrawImage(high, this.ImageRect);
                            }
                        }
                        else
                        {
                            using (Image high = AssemblyHelper.GetImage("QQ.CheckBox.hightlight.png"))
                            {
                                g.DrawImage(high, this.ImageRect);
                            }
                        }
                        break;
                }
                if (base.CheckState == CheckState.Indeterminate)
                {
                    if (this.MouseState == EMouseState.Down || this.MouseState == EMouseState.Move)
                    {
                        using (Image normal = AssemblyHelper.GetImage("QQ.CheckBox._tick_normal.png"))
                        {
                            g.DrawImage(normal, this.ImageRect);
                        }
                    }
                    else
                    {
                        using (Image high = AssemblyHelper.GetImage("QQ.CheckBox._tick_highlight.png"))
                        {
                            g.DrawImage(high, this.ImageRect);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventargs"></param>
        protected override void OnMouseEnter(EventArgs eventargs)
        {
            base.OnMouseEnter(eventargs);
            this.MouseState = EMouseState.Move;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mevent"></param>
        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            base.OnMouseDown(mevent);
            this.MouseState = EMouseState.Down;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventargs"></param>
        protected override void OnMouseLeave(EventArgs eventargs)
        {
            base.OnMouseLeave(eventargs);
            this.MouseState = EMouseState.Leave;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mevent"></param>
        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            base.OnMouseUp(mevent);
            this.MouseState = EMouseState.Up;
        }

        #endregion
    }
}
