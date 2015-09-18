using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using Andwho.Windows.Resource;

namespace Andwho.Windows.Forms
{
    /// <summary>
    /// 
    /// </summary>
    [DefaultEvent("CheckedChanged")]
    [ToolboxBitmap(typeof(RadioButton))]
    public class QQRadioButton : RadioButton
    {
        #region 变量
        /// <summary>
        /// 
        /// </summary>
        private EMouseState _mouseState = EMouseState.Normal;

        #endregion

        #region 构造函数
        /// <summary>
        /// 
        /// </summary>
        public QQRadioButton()
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
        /// 重写CheckBox的Text属性
        /// </summary>
        [DefaultValue("QQCheckBox")]
        public override string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }
        /// <summary>
        /// 
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
        #endregion

        #region Override 方法
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pevent"></param>
        protected override void OnPaint(PaintEventArgs pevent)
        {
            Graphics g = pevent.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.AntiAlias;

            Color foreColor = base.Enabled ? this.ForeColor : Color.Gray;
            TextFormatFlags flags = TextFormatFlags.VerticalCenter |
                                    TextFormatFlags.Left |
                                    TextFormatFlags.SingleLine;
            TextRenderer.DrawText(g, this.Text, this.Font, this.TextRect, foreColor, flags);

            switch (this.MouseState)
            {
                case EMouseState.Leave:
                case EMouseState.Normal:
                    if (base.Checked)
                    {
                        using (Image normal = AssemblyHelper.GetImage("QQ.RadioButton.tick_normal.png"))
                        {
                            g.DrawImage(normal, this.ImageRect);
                        }
                    }
                    else
                    {
                        using (Image normal = AssemblyHelper.GetImage("QQ.RadioButton.normal.png"))
                        {
                            g.DrawImage(normal, this.ImageRect);
                        }
                    }

                    break;
                case EMouseState.Move:
                case EMouseState.Down:
                case EMouseState.Up:
                    if (base.Checked)
                    {
                        using (Image high = AssemblyHelper.GetImage("QQ.RadioButton.tick_highlight.png"))
                        {
                            g.DrawImage(high, this.ImageRect);
                        }
                    }
                    else
                    {
                        using (Image high = AssemblyHelper.GetImage("QQ.RadioButton.highlight.png"))
                        {
                            g.DrawImage(high, this.ImageRect);
                        }
                    }
                    break;
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mevent"></param>
        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            base.OnMouseDown(mevent);
            this.MouseState = EMouseState.Down;
        }
        #endregion
    }
}
