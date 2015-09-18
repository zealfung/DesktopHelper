using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Andwho.Windows.Resource;
using Andwho.Windows.Helper;

namespace Andwho.Windows.Forms
{
    /// <summary>
    /// 
    /// </summary>
    public partial class QQTextBoxEx : UserControl
    {
        #region 变量
        private QQTextBoxBase BaseText;

        private Image _borderImage = AssemblyHelper.GetImage("QQ.TextBox.normal.png");
        private Cursor _cursor = Cursors.IBeam;
        private EMouseState _mouseState = EMouseState.Normal;
        private EMouseState _iconMouseState = EMouseState.Normal;
        private bool _iconIsButton;
        private Image _icon;
        #endregion

        #region 构造函数
        /// <summary>
        /// 
        /// </summary>
        public QQTextBoxEx()
        {
            this.SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.DoubleBuffer, true);
            InitializeComponent();
            this.InitEvents();
            this.BackColor = Color.Transparent;
            this.UpdateStyles();
        }

        #endregion

        #region 自定义事件 && 激发事件的方法
        /// <summary>
        /// 
        /// </summary>
        public event EventHandler IconClick;
        /// <summary>
        /// 
        /// </summary>
        private void OnIconClick()
        {
            if (this.IconClick != null)
                this.IconClick(this, EventArgs.Empty);
        }
        #endregion

        #region 属性
        /// <summary>
        /// 
        /// </summary>
        [Description("指定可以在编辑控件中输入的最大字符数。"), Category("行为")]
        public virtual int MaxLength
        {
            get { return this.BaseText.MaxLength; }
            set { this.BaseText.MaxLength = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        [Description("控件编辑控件的文本是否能够跨越多行。"), Category("行为")]
        public virtual bool Multiline
        {
            get { return this.BaseText.Multiline; }
            set { this.BaseText.Multiline = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        [Description("指示将为单行编辑控件的密码输入显示的字符。"), Category("行为")]
        public char IsPasswordChat
        {
            get { return this.BaseText.PasswordChar; }
            set { this.BaseText.PasswordChar = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        [Description("控制能否更改编辑控件中的文本。"), Category("行为")]
        public virtual bool ReadOnly
        {
            get { return this.BaseText.ReadOnly; }
            set { this.BaseText.ReadOnly = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        [Description("指示编辑控件中的文本是否以默认的密码字符显示。"), Category("行为")]
        public virtual bool IsSystemPasswordChar
        {
            get { return this.BaseText.UseSystemPasswordChar; }
            set { this.BaseText.UseSystemPasswordChar = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        [Description("指示多行编辑控件是否自动换行。"), Category("行为")]
        public virtual bool WordWrap
        {
            get { return this.BaseText.WordWrap; }
            set { this.BaseText.WordWrap = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        [Description("用于显示控件中文本的字体。"), Category("外观")]
        new public virtual Font Font
        {
            get { return this.BaseText.Font; }
            set { this.BaseText.Font = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        [Description("此组件的前景色，用于显示文本。"), Category("外观")]
        new public virtual Color ForeColor
        {
            get { return this.BaseText.ForeColor; }
            set { this.BaseText.ForeColor = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        [Description("多行编辑中的文本行，作为字符串值的数组。"), Category("外观")]
        public virtual string[] Lines
        {
            get { return this.BaseText.Lines; }
            set { this.BaseText.Lines = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        [Description("指示对于多行编辑控件，将为此控件显示哪些滚动条。"), Category("外观")]
        public virtual ScrollBars ScrollBars
        {
            get { return this.BaseText.ScrollBars; }
            set { this.BaseText.ScrollBars = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        [Description("指示应该如何对齐编辑控件的文本。"), Category("外观")]
        public virtual HorizontalAlignment TextAlign
        {
            get { return this.BaseText.TextAlign; }
            set { this.BaseText.TextAlign = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        [Description("文本框的图标"), Category("自定义属性")]
        public virtual Image Icon
        {
            get { return this._icon; }
            set
            {
                this._icon = value;
                base.Invalidate(this.IconRect);
                this.PositionTextBox();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Description("文本框的图标是否是按钮"), Category("自定义属性")]
        public virtual bool IconIsButton
        {
            get { return this._iconIsButton; }
            set { this._iconIsButton = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        [Description("水印文字"), Category("自定义属性")]
        public virtual string WaterText
        {
            get { return this.BaseText.WaterText; }
            set { this.BaseText.WaterText = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        [Description("水印颜色"), Category("自定义属性")]
        public virtual Color WaterColor
        {
            get { return this.BaseText.WaterColor; }
            set { this.BaseText.WaterColor = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        [Browsable(true)]
        public override string Text
        {
            get { return this.BaseText.Text; }
            set { this.BaseText.Text = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public override Cursor Cursor
        {
            get { return this._cursor; }
            set { this._cursor = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public override Size MinimumSize
        {
            get { return new Size(20, 24); }
            set { base.MinimumSize = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        protected virtual EMouseState MouseState
        {
            get { return this._mouseState; }
            set
            {
                this._mouseState = value;
                base.Invalidate();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        protected virtual EMouseState IconMouseState
        {
            get { return this._iconMouseState; }
            set
            {
                this._iconMouseState = value;
                base.Invalidate();
            }
        }
        /// <summary>
        /// 图标的绘制区域
        /// </summary>
        protected virtual Rectangle IconRect
        {
            get { return new Rectangle(3, 3, 20, 20); }
        }
        #endregion

        #region 方法
        /// <summary>
        /// 加载事件
        /// </summary>
        private void InitEvents()
        {
            this.BaseText.MouseMove += new MouseEventHandler(BaseText_MouseMove);
            this.BaseText.MouseLeave += new EventHandler(BaseText_MouseLeave);
            this.BaseText.KeyDown += new KeyEventHandler(BaseText_KeyDown);
            this.BaseText.KeyPress += new KeyPressEventHandler(BaseText_KeyPress);
            this.BaseText.KeyUp += new KeyEventHandler(BaseText_KeyUp);
        }

        /// <summary>
        /// 设计界面
        /// </summary>
        private void InitializeComponent()
        {
            this.BaseText = new Andwho.Windows.Forms.QQTextBoxBase();
            this.SuspendLayout();
            // 
            // BaseText
            // 
            this.BaseText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BaseText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.BaseText.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BaseText.Location = new System.Drawing.Point(3, 4);
            this.BaseText.Margin = new System.Windows.Forms.Padding(0);
            this.BaseText.Name = "BaseText";
            this.BaseText.Size = new System.Drawing.Size(172, 18);
            this.BaseText.TabIndex = 0;
            this.BaseText.WaterColor = System.Drawing.Color.DarkGray;
            this.BaseText.WaterText = "";
            // 
            // TextBoxEx
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.BaseText);
            this.Name = "TextBoxEx";
            this.Size = new System.Drawing.Size(178, 24);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        /// <summary>
        /// 偏移文本框
        /// </summary>
        protected virtual void PositionTextBox()
        {
            if (this._icon != null)
            {
                int position = 23;
                this.BaseText.Width -= position; 
                this.BaseText.Location = new Point(
                    this.BaseText.Location.X + position, 
                    this.BaseText.Location.Y);
            }
        }

        #endregion

        #region 事件
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BaseText_MouseLeave(object sender, EventArgs e)
        {
            this.MouseState = EMouseState.Leave;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BaseText_MouseMove(object sender, MouseEventArgs e)
        {
            this.MouseState = EMouseState.Move;
        }

        private void BaseText_KeyUp(object sender, KeyEventArgs e)
        {
            this.OnKeyUp(e);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BaseText_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.OnKeyPress(e);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BaseText_KeyDown(object sender, KeyEventArgs e)
        {
            this.OnKeyDown(e);
        }
        #endregion

        #region Override Methods
        /// <summary>
        /// 当文本框的大小发生改变时，将文本框的类型换成多行文本
        /// </summary>
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            if (this.Height > 26)
                this.BaseText.Multiline = true;
            else
                this.BaseText.Multiline = false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            switch (this._mouseState)
            {
                case EMouseState.Move:
                    using (Image hotLine = AssemblyHelper.GetImage("QQ.TextBox.move.png"))
                    {
                        DrawHelper.RendererBackground(g, this.ClientRectangle, hotLine, true);
                    }
                    break;
                default:
                    DrawHelper.RendererBackground(g, this.ClientRectangle, this._borderImage, true);
                    break;
            }
            if (this._icon != null)
            {
                Rectangle iconRect = this.IconRect;
                if (this._iconMouseState == EMouseState.Down)
                {
                    iconRect.X += 1;
                    iconRect.Y += 1;
                }
                g.DrawImage(this._icon, iconRect, 0, 0, this._icon.Width, this._icon.Height, GraphicsUnit.Pixel);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            this.MouseState = EMouseState.Move;
            if (this._icon != null && this.IconRect.Contains(e.Location))
            {
                if (this._iconIsButton)
                    this.Cursor = Cursors.Hand;
                else
                    this.Cursor = Cursors.Default;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (this._icon != null && this._iconIsButton)
            {
                if (e.Button == MouseButtons.Left && this.IconRect.Contains(e.Location))
                {
                    this.IconMouseState = EMouseState.Down;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (this._icon != null && this._iconIsButton)
            {
                this.IconMouseState = EMouseState.Up;
                if (e.Button == MouseButtons.Left && this.IconRect.Contains(e.Location))
                    this.OnIconClick();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            this.MouseState = EMouseState.Leave;
            this.Cursor = Cursors.Default;
        }
        #endregion
    }
}
