using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Text;
using System.Windows.Forms;
using Andwho.Windows.Resource;
using Andwho.Windows.Helper;

namespace Andwho.Windows.Forms
{
    /// <summary>
    /// Button
    /// </summary>
    [DefaultEvent("Click")]
    [DefaultProperty("Text")]
    [ToolboxBitmap(typeof(Button))]
    public class QQButton : Button
    {
        #region 变量
        /// <summary>
        /// 鼠标状态
        /// </summary>
        private EMouseState _mouseState = EMouseState.Normal;
        /// <summary>
        /// 文本对齐方式
        /// </summary>
        private TextFormatFlags _textAlign = TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter;
        /// <summary>
        /// 默认时的按钮图片
        /// </summary>
        private Image _normalImage = null;
        /// <summary>
        /// 鼠标按下时的图片
        /// </summary>
        private Image _downImage = null;
        /// <summary>
        /// 鼠标划过时的图片
        /// </summary>
        private Image _moveImage = null;
        /// <summary>
        /// 是否按下了鼠标
        /// </summary>
        private bool _isShowBorder = true;

        #endregion

        #region 构造函数
        /// <summary>
        /// 实例化 Andwho.Windows.Forms.QQButton 新的实例。
        /// </summary>
        public QQButton()
        {
            this.SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.Selectable |
                ControlStyles.DoubleBuffer |
                ControlStyles.SupportsTransparentBackColor |
                ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.Opaque, false);
            base.BackColor = Color.Transparent;
            this.UpdateStyles();
        }

        #endregion

        #region 属性
        /// <summary>
        /// 默认大小
        /// </summary>
        protected override Size DefaultSize
        {
            get { return new Size(75, 28); }
        }
        /// <summary>
        /// 默认图片
        /// </summary>
        [Description("默认图片")]
        public virtual Image NormalImage
        {
            get
            {
                if (this._normalImage == null)
                    this._normalImage = AssemblyHelper.GetImage("QQ.Button.normal.png");
                return this._normalImage;
            }
            set
            {
                this._normalImage = value;
                base.Invalidate();
            }
        }
        /// <summary>
        /// 鼠标按下时的图片
        /// </summary>
        [Description("鼠标按下时的图片")]
        public virtual Image DownImage
        {
            get
            {
                if (this._downImage == null)
                    this._downImage = AssemblyHelper.GetImage("QQ.Button.down.png");
                return this._downImage;
            }
            set
            {
                this._downImage = value;
                base.Invalidate();
            }
        }
        /// <summary>
        /// 鼠标划过时的图片
        /// </summary>
        [Description("鼠标划过时的图片")]
        public virtual Image MoveImage
        {
            get
            {
                if (this._moveImage == null)
                    this._moveImage = AssemblyHelper.GetImage("QQ.Button.highlight.png");
                return this._moveImage;
            }
            set
            {
                this._moveImage = value;

                base.Invalidate();
            }
        }
        /// <summary>
        /// 是否显示发光边框
        /// </summary>
        [Description("是否显示发光边框")]
        public virtual bool IsShowBorder
        {
            get { return this._isShowBorder; }
            set { this._isShowBorder = value; }
        }
        /// <summary>
        /// 与控件相关的文本
        /// </summary>
        [DefaultValue("QQButton")]
        public override string Text
        {
            get { return base.Text; }
            set
            {
                base.Text = value;
                base.Invalidate(this.TextRect);
            }
        }
        /// <summary>
        /// 按钮上显示的图片
        /// </summary>
        [Description("按钮上显示的图片")]
        public virtual new Image Image
        {
            get { return base.Image; }
            set
            {
                base.Image = value;
                base.Invalidate();
            }
        }

        /// <summary>
        /// 按钮上文字的对齐方式
        /// </summary>
        [Description("按钮上文字的对齐方式")]
        new public ContentAlignment TextAlign
        {
            get { return base.TextAlign; }
            set
            {
                base.TextAlign = value;
                switch (base.TextAlign)
                {
                    case ContentAlignment.BottomCenter:
                        this._textAlign = TextFormatFlags.Bottom | 
                                          TextFormatFlags.HorizontalCenter | 
                                          TextFormatFlags.SingleLine;
                        break;
                    case ContentAlignment.BottomLeft:
                        this._textAlign = TextFormatFlags.Bottom | 
                                          TextFormatFlags.Left | 
                                          TextFormatFlags.SingleLine;
                        break;
                    case ContentAlignment.BottomRight:
                        this._textAlign = TextFormatFlags.Bottom | 
                                          TextFormatFlags.Right | 
                                          TextFormatFlags.SingleLine;
                        break;
                    case ContentAlignment.MiddleCenter:
                        this._textAlign = TextFormatFlags.SingleLine | 
                                          TextFormatFlags.HorizontalCenter | 
                                          TextFormatFlags.VerticalCenter;
                        break;
                    case ContentAlignment.MiddleLeft:
                        this._textAlign = TextFormatFlags.Left | 
                                          TextFormatFlags.VerticalCenter | 
                                          TextFormatFlags.SingleLine;
                        break;
                    case ContentAlignment.MiddleRight:
                        this._textAlign = TextFormatFlags.Right | 
                                          TextFormatFlags.VerticalCenter | 
                                          TextFormatFlags.SingleLine;
                        break;
                    case ContentAlignment.TopCenter:
                        this._textAlign = TextFormatFlags.Top | 
                                          TextFormatFlags.HorizontalCenter | 
                                          TextFormatFlags.SingleLine;
                        break;
                    case ContentAlignment.TopLeft:
                        this._textAlign = TextFormatFlags.Top | 
                                          TextFormatFlags.Left | 
                                          TextFormatFlags.SingleLine;
                        break;
                    case ContentAlignment.TopRight:
                        this._textAlign = TextFormatFlags.Top | 
                                          TextFormatFlags.Right | 
                                          TextFormatFlags.SingleLine;
                        break;
                }
                base.Invalidate(this.TextRect);
            }
        }

        /// <summary>
        /// 整个按钮的区域
        /// </summary>
        internal Rectangle AllRect
        {
            get { return new Rectangle(0, 0, this.Width, this.Height); }
        }
        /// <summary>
        /// 文字区域
        /// </summary>
        internal Rectangle TextRect
        {
            get { return new Rectangle(2, 2, this.AllRect.Width - 4, this.AllRect.Height - 4); }
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

        #endregion

        #region Override 方法
        /// <summary>
        /// 引发 System.Windows.Forms.Form.Paint 事件。
        /// </summary>
        /// <param name="pevent">包含事件数据的 System.Windows.Forms.PaintEventArgs。</param>
        protected override void OnPaint(PaintEventArgs pevent)
        {
            Graphics g = pevent.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.AntiAlias;
            //g.DrawImage(AssemblyHelper.GetAssemblyImage("Button.all_btn_White-side.png"), this.AllRect);
            if (base.Enabled)
            {
                if (this.IsShowBorder)
                {
                    if (this.Focused)//得到焦点的时候，绘制边框
                    {
                        using (Image light = AssemblyHelper.GetImage("QQ.Button.Light.png"))
                        {
                            //g.DrawImage(light, this.AllRect);
                            DrawHelper.RendererBackground(g, this.AllRect, light, true);
                        }
                    }
                }

                switch (this.MouseState)
                {
                    case EMouseState.Leave:
                    case EMouseState.Normal:
                        if (base.Focused)
                        {
                            if (this.IsShowBorder)
                            {
                                using (Image focus = AssemblyHelper.GetImage("QQ.Button.focus.png"))
                                {
                                    DrawHelper.RendererBackground(g, this.TextRect, focus, true);
                                }
                            }
                            else
                            {
                                DrawHelper.RendererBackground(g, this.TextRect, this.NormalImage, true);
                            }
                        }
                        else
                        {
                            DrawHelper.RendererBackground(g, this.TextRect, this.NormalImage, true);
                        }
                        break;
                    case EMouseState.Up:
                    case EMouseState.Move:
                        DrawHelper.RendererBackground(g, this.TextRect, this.MoveImage, true);
                        break;
                    case EMouseState.Down:
                        DrawHelper.RendererBackground(g, this.TextRect, this.DownImage, true);
                        break;
                }

                //if (this.Image != null)
                //{
                //int y = (this.Height - this.Image.Height) / 2;
                //int x = 10;

                //if (!string.IsNullOrEmpty(this.Text))
                //    x = (this.AllRect.Width - (TextHelper.GetStringLen(this.Text) * 6 + this.Image.Width)) / 2;
                //else
                //    x = (this.Width - this.Image.Width) / 2;//将图片绘制在按钮居中位置
                //Rectangle rect = new Rectangle(x, y, this.Image.Width, this.Image.Height);
                //Rectangle imgRect = new Rectangle(0, 0, this.Image.Width, this.Image.Height);
                ////绘制图片
                //g.DrawImage(this.Image, rect, imgRect, GraphicsUnit.Pixel);
                ////绘制文字
                //Rectangle textRect = new Rectangle(this.TextRect.X + this.Image.Width, this.TextRect.Y, this.TextRect.Width, this.TextRect.Height);
                //TextRenderer.DrawText(g, this.Text, this.Font, textRect, this.ForeColor, _textAlign);
                //}
                //else
                //{
                //绘制按钮上的文字
                TextRenderer.DrawText(g, this.Text, this.Font, this.TextRect, this.ForeColor, this._textAlign);
                //}
            }
            else
            {
                using (Image gray = AssemblyHelper.GetImage("QQ.Button.gray.png"))
                {
                    DrawHelper.RendererBackground(g, this.TextRect, gray, true);
                }
                TextRenderer.DrawText(g, this.Text, this.Font, this.TextRect, Color.Gray, this._textAlign);
            }
        }
        /// <summary>
        /// 引发 System.Windows.Forms.Form.MouseEnter 事件。
        /// </summary>
        /// <param name="e">包含事件数据的 System.EventArgs。</param>
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            this.MouseState = EMouseState.Move;
        }
        /// <summary>
        /// 引发 System.Windows.Forms.Form.MouseLeave 事件。
        /// </summary>
        /// <param name="e">包含事件数据的 System.EventArgs。</param>
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            this.MouseState = EMouseState.Leave;
        }
        /// <summary>
        /// 引发 System.Windows.Forms.Form.MouseDown 事件。
        /// </summary>
        /// <param name="mevent">包含事件数据的 System.Windows.Forms.MouseEventArgs。</param>
        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            base.OnMouseDown(mevent);
            this.MouseState = EMouseState.Down;
        }
        /// <summary>
        /// 引发 System.Windows.Forms.Form.MouseUp 事件。
        /// </summary>
        /// <param name="mevent">包含事件数据的 System.Windows.Forms.MouseEventArgs。</param>
        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            base.OnMouseUp(mevent);
            this.MouseState = EMouseState.Up;
        }
        #endregion
    }
}
