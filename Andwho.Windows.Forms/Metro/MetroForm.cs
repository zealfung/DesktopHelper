using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using Andwho.Windows.Resource;

namespace Andwho.Windows.Forms.Metro
{
    public partial class MetroForm : Form
    {
        #region 变量
        /// <summary>
        /// Item宽度（高度）
        /// </summary>
        private int _itemWidth = 80;
        /// <summary>
        /// 
        /// </summary>
        private int _itemHeight = 80;
        /// <summary>
        /// 
        /// </summary>
        private MetroRenderer _renderer = null;
        /// <summary>
        /// 
        /// </summary>
        private MetroItemCollection _items = null;

        /// <summary>
        /// 
        /// </summary>
        private bool _mouseDown = false;
        /// <summary>
        /// 
        /// </summary>
        private Rectangle _startRect = Rectangle.Empty;
        /// <summary>
        /// 
        /// </summary>
        private readonly Image START_IMAGE = AssemblyHelper.GetImage("Icons.start.png");
        /// <summary>
        /// 
        /// </summary>
        private Size _startSize = new Size(50, 50);
        /// <summary>
        /// 
        /// </summary>
        private EMouseState _startState = EMouseState.Normal;
        /// <summary>
        /// 
        /// </summary>
        private static readonly object EventClickStart = new object();
        /// <summary>
        /// 
        /// </summary>
        private static readonly object EventClickMetroFormItem = new object();
        #endregion

        #region 构造函数
        /// <summary>
        /// 
        /// </summary>
        public MetroForm()
        {
            this.SetStyle(
               ControlStyles.AllPaintingInWmPaint |
               ControlStyles.OptimizedDoubleBuffer |
               ControlStyles.ResizeRedraw |
               ControlStyles.Selectable |
               ControlStyles.ContainerControl |
               ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.Opaque, false);
            this.BackColor = this.Renderer.BackColor;
            this.UpdateStyles();

            InitializeComponent();
        }

        #endregion

        #region 属性
        /// <summary>
        /// 
        /// </summary>
        public MetroRenderer Renderer
        {
            get
            {
                if (this._renderer == null)
                    this._renderer = new MetroRenderer();
                return this._renderer;
            }
            set { this._renderer = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public MetroItemCollection Items
        {
            get
            {
                if (this._items == null)
                {
                    this._items = new MetroItemCollection(this);
                }
                return this._items;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Description("每个项(Item)的高度")]
        public int ItemHeight
        {
            get { return this._itemHeight; }
            set { this._itemHeight = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        protected Rectangle StartRect
        {
            get
            {
                if (START_IMAGE != null && this._startRect == Rectangle.Empty)
                {
                    int y = this.Height - this._itemHeight;
                    this._startRect = new Rectangle(0, y, this.Width, this._itemHeight);
                }
                return this._startRect;
            }
        }

        #endregion

        #region 事件
        /// <summary>
        /// 当单击开始按钮时，激发的事件
        /// </summary>
        public event EventHandler ClickStart
        {
            add { base.Events.AddHandler(EventClickStart, value); }
            remove { base.Events.RemoveHandler(EventClickStart, value); }
        }
        /// <summary>
        /// 当单击Item时激情的事件
        /// </summary>
        public event EventHandler ClickMetroFormItem
        {
            add { base.Events.AddHandler(EventClickMetroFormItem, value); }
            remove { base.Events.RemoveHandler(EventClickMetroFormItem, value); }
        }

        #endregion

        #region 方法
        /// <summary>
        /// 绘制开始菜单UI
        /// </summary>
        /// <param name="g"></param>
        private void DrawStart(Graphics g)
        {
            Color backColor = this.Renderer.BackColor;
            switch (this._startState)
            {
                case EMouseState.Normal:
                case EMouseState.Leave:
                    backColor = this.Renderer.BackColor;
                    break;
                case EMouseState.Move:
                case EMouseState.Up:
                    backColor = this.Renderer.EnterColor;
                    break;
                case EMouseState.Down:
                    backColor = this.Renderer.DownColor;
                    break;
            }
            using (Brush brush = new SolidBrush(backColor))
            {
                g.FillRectangle(brush, this.StartRect);
            }
            using (GraphicsPath path = new GraphicsPath())
            {
                #region 手工绘制
                //const int TEMP = 4;
                //int x = 15;
                //int y = this.StartRect.Height / 5 - 2;

                //int width = this.Width - (x * 2);
                //Point[] point = new Point[4];
                //point[0] = new Point(x, this.StartRect.Y + y + TEMP);
                //point[1] = new Point(x + width, this.StartRect.Y + y - 2);
                //point[2] = new Point(x + width, this.StartRect.Bottom - y + 2);
                //point[3] = new Point(x, this.StartRect.Bottom - y - TEMP);
                //path.AddLines(point);
                //path.CloseFigure();
                //using (Brush brush = new SolidBrush(this.Renderer.StartColor))
                //{
                //    g.FillPath(brush, path);
                //}
                //using (Pen pen = new Pen(backColor, 3.0f))
                //{
                //    int lineY = this.StartRect.Y + (this.StartRect.Height / 2);
                //    g.DrawLine(pen, x - 1, lineY, width + x + 1, lineY);
                //}
                //using (Pen pen = new Pen(backColor, 3.0f))
                //{
                //    int lineX = width / 5 * 2 + x;
                //    g.DrawLine(
                //        pen,
                //        lineX,
                //        this.StartRect.Y + y,
                //        lineX,
                //        this.StartRect.Bottom - y);
                //}
                #endregion
                int x = (this.Width - this._startSize.Width) / 2;
                int y = this.StartRect.Y + (this.StartRect.Height - this._startSize.Height) / 2;
                Rectangle rect = new Rectangle(new Point(x, y), this._startSize);
                g.DrawImage(START_IMAGE, rect, 0, 0, START_IMAGE.Width, START_IMAGE.Height, GraphicsUnit.Pixel);
            }
        }

        /// <summary>
        /// 打开Item
        /// </summary>
        /// <param name="item"></param>
        private void OpenProcess(MetroItem item)
        {
            try
            {
                if (string.IsNullOrEmpty(item.FilePath))
                {
                    MessageBox.Show("找不到指定路径");
                    return;
                }
                switch (item.ItemType)
                {
                    case EItemType.Application:
                        break;
                    case EItemType.Directory:
                        break;
                    case EItemType.Exe:
                        Process.Start(item.FilePath);
                        break;
                    case EItemType.Menu:
                        break;
                    case EItemType.None:
                        break;
                    case EItemType.System:
                        if (string.IsNullOrEmpty(item.ClassID))
                        {
                            MessageBox.Show("找不到ClassID");
                            return;
                        }
                        Process.Start(item.FilePath, item.ClassID);
                        break;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("MetroForm.OpenProcess() :: " + ex.Message);
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
            base.OnPaint(e);
            if (!DesignMode)
            {
                Graphics g = e.Graphics;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                if (this.Items.Count > 0)
                {
                    int x = 0, y = 0;
                    int width = this.Width - 1;
                    //循环绘制每一项
                    foreach (MetroItem item in this._items)
                    {
                        Rectangle itemRect = new Rectangle(x, y, width, this._itemHeight - 1);
                        //itemRect.Inflate(1, 1);
                        item.Location = new Point(x, y);
                        item.Size = new System.Drawing.Size(width, this._itemHeight);
                        item.OnPaintBackground(g, itemRect, this.Renderer);
                        item.OnPaint(g, itemRect, this.Renderer);
                        y += this._itemHeight;
                    }
                }
                this.DrawStart(g);//绘制开始菜单
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (!DesignMode && !this._mouseDown)
            {
                if (this.Items.Count > 0)
                {
                    foreach (MetroItem item in this.Items)
                    {
                        if (item.Rectangle.Contains(e.Location))
                            item.MouseState = EMouseState.Move;
                        else
                            item.MouseState = EMouseState.Leave;
                        this.Invalidate(item.Rectangle);
                    }
                }
                if (this.StartRect.Contains(e.Location))
                {
                    this._startState = EMouseState.Move;
                    this.Invalidate(this.StartRect);
                }
                else
                {
                    this._startState = EMouseState.Leave;
                    this.Invalidate(this.StartRect);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (!DesignMode && e.Button == MouseButtons.Left)
            {
                if (this.Items.Count > 0)
                {
                    foreach (MetroItem item in this.Items)
                    {
                        if (item.Rectangle.Contains(e.Location))
                        {
                            item.MouseState = EMouseState.Down;
                            this._mouseDown = true;
                        }
                        this.Invalidate(item.Rectangle);
                    }
                }
                if (this.StartRect.Contains(e.Location))
                {
                    this._mouseDown = true;
                    this._startState = EMouseState.Down;
                    this.Invalidate(this.StartRect);
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
            if (!DesignMode && e.Button == MouseButtons.Left)
            {
                if (this.Items.Count > 0)
                {
                    foreach (MetroItem item in this.Items)
                    {
                        if (item.Rectangle.Contains(e.Location))
                        {
                            item.MouseState = EMouseState.Up;
                            this.OnClickMetroFormItem(item, EventArgs.Empty);
                            this.OpenProcess(item);
                        }
                        this.Invalidate(item.Rectangle);
                    }
                }
                if (this.StartRect.Contains(e.Location))
                {
                    this._startState = EMouseState.Up;
                    this.OnClickStart(this, EventArgs.Empty);
                    this.Invalidate(this.StartRect);
                }
                this._mouseDown = false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            if (!DesignMode && this.Items.Count > 0)
            {
                foreach (MetroItem item in this.Items)
                {
                    if (item.MouseState != EMouseState.Leave)
                    {
                        item.MouseState = EMouseState.Leave;
                        this.Invalidate(item.Rectangle);
                    }
                }
                if (this._startState != EMouseState.Leave)
                {
                    this._startState = EMouseState.Leave;
                    this.Invalidate(this.StartRect);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!this.DesignMode)
            {
                int width = Screen.PrimaryScreen.WorkingArea.Width;
                int height = Screen.PrimaryScreen.WorkingArea.Height;
                this.Size = new Size(this._itemWidth, height);
                this.Location = new Point(width - this._itemWidth, 0);
            }
        }

        #endregion

        #region 激发事件的方法
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnClickStart(object sender, EventArgs e)
        {
            EventHandler handler = base.Events[EventClickStart] as EventHandler;
            if (handler != null)
            {
                handler(sender, e);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnClickMetroFormItem(object sender, EventArgs e)
        {
            EventHandler handler = base.Events[EventClickMetroFormItem] as EventHandler;
            if (handler != null)
            {
                handler(sender, e);
            }
        }
        #endregion
    }
}
