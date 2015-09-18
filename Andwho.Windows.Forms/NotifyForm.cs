using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using Andwho.Windows.Win32;
using System.Diagnostics;
using Andwho.Windows.Helper;
using Andwho.Windows.Resource;

namespace Andwho.Windows.Forms
{
    /// <summary>
    /// 通知窗口
    /// </summary>
    public class NotifyForm : QQForm
    {
        #region 变量
        /// <summary>
        /// 窗口大小
        /// </summary>
        private readonly Size SIZE = new Size(250, 178);
        /// <summary>
        /// 控制窗口动画效果
        /// </summary>
        private static Timer Timer = null;
        /// <summary>
        /// 当前类的静态实例
        /// </summary>
        private static NotifyForm Notify = null;
        /// <summary>
        /// 显示窗口后停留的时间，以“秒”为单位
        /// </summary>
        private static int ShowInterval = 5;
        /// <summary>
        /// 计算时间
        /// </summary>
        private static int Interval = 0;
        /// <summary>
        /// 提示文字信息
        /// </summary>
        private string _notifyText = string.Empty;
        /// <summary>
        /// 提示信息的文字颜色
        /// </summary>
        private Color _notifyForeColor = Color.Black;
        /// <summary>
        /// 提示信息的字体
        /// </summary>
        private Font _notifyFont = new Font("宋体", 10f, FontStyle.Bold);

        #region 资源图片
        
        /// <summary>
        /// 背景图片
        /// </summary>
        private Image _backImage = AssemblyHelper.GetImage("QQ.SkinMgr.all_inside02_bkg.png");
        /// <summary>
        /// 分割线
        /// </summary>
        private Image _splitImage = AssemblyHelper.GetImage("QQ.FormFrame.ContactFilter_splitter.png");

        #endregion

        #endregion

        #region 构造函数
        /// <summary>
        /// 初始化 Andwho.Windows.Forms.NotifyForm 新的实例
        /// </summary>
        public NotifyForm()
            : base()
        {
            this.ShowInTaskbar = false;
        }

        #endregion

        #region 属性
        /// <summary>
        /// 设置窗口大小的最大值
        /// </summary>
        public override Size MaximumSize
        {
            get { return SIZE; }
        }
        /// <summary>
        /// 设置窗口大小的最小值
        /// </summary>
        public override Size MinimumSize
        {
            get { return SIZE; }
        }
        /// <summary>
        /// 提示文字信息
        /// </summary>
        public string NotifyText
        {
            get { return this._notifyText; }
            set
            {
                this._notifyText = value;
                this.Invalidate(this.NotifyTextRect);
            }
        }
        /// <summary>
        /// 提示信息的文字颜色
        /// </summary>
        public Color NotifyForeColor
        {
            get { return this._notifyForeColor; }
            set
            {
                this._notifyForeColor = value;
                this.Invalidate(this.NotifyTextRect);
            }
        }
        /// <summary>
        /// 提示信息的字体
        /// </summary>
        public Font NotifyFont
        {
            get { return this._notifyFont; }
            set
            {
                this._notifyFont = value;
                this.Invalidate(this.NotifyTextRect);
            }
        }
        /// <summary>
        /// 提示信息显示的矩型区域
        /// </summary>
        private Rectangle NotifyTextRect
        {
            get
            {
                int x = 10;
                int y = base.TitleBarRect.Height;
                int width = this.Width - x * 2;
                int height = this.Height - y - this.ControlRect.Height;
                return new Rectangle(x, y, width, height);
            }
        }
        /// <summary>
        /// 控制按钮的区域
        /// </summary>
        private Rectangle ControlRect
        {
            get
            {
                int x = 10;
                int height = 30;
                int y = this.Height - height;
                int width = this.Width - x * 2;
                return new Rectangle(x, y, width, height);
            }
        }
        #endregion

        #region 方法
        /// <summary>
        /// 将以动画的形式显示窗口，默认存在时间为 5 秒
        /// </summary>
        public static void AnimalShow()
        {
            NotifyForm.AnimalShow(null, null);
        }
        /// <summary>
        /// 将以动画的形式显示，默认存在时间为 5 秒
        /// </summary>
        /// <param name="caption">窗口标题</param>
        public static void AnimalShow(string caption)
        {
            NotifyForm.AnimalShow(caption, null);
        }
        /// <summary>
        /// 将以动画的形式显示，默认存在时间为 5 秒
        /// </summary>
        /// <param name="caption">窗口标题</param>
        /// <param name="text">窗口内容</param>
        public static void AnimalShow(string caption, string text)
        {
            try
            {
                if (NotifyForm.Notify == null)
                    NotifyForm.Notify = new NotifyForm();
                if (NotifyForm.Timer == null)
                    NotifyForm.Timer = new Timer();
                if(!string.IsNullOrEmpty(caption)) NotifyForm.Notify.Text = caption;
                if(!string.IsNullOrEmpty(text)) NotifyForm.Notify.NotifyText = text;
                NotifyForm.Timer.Interval = 100;
                NotifyForm.Timer.Tick += new EventHandler(Timer_Tick);
                NotifyForm.Timer.Enabled = true;
                NotifyForm.Notify.Show();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("NotifyForm.AnimalShow() :: " + ex.Message);
                throw ex;
            }
        }
        /// <summary>
        /// 将以动画的形式显示，默认存在时间为 5 秒
        /// </summary>
        /// <param name="caption">窗口标题</param>
        /// <param name="text">窗口内容</param>
        /// <param name="interval">窗口存在的时间h</param>
        public static void AnimalShow(string caption, string text, int interval)
        {
            NotifyForm.ShowInterval = interval;
            NotifyForm.AnimalShow(caption, text);
        }
        /// <summary>
        /// 执行窗口动画操作
        /// </summary>
        protected static void Timer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (NotifyForm.Notify != null)
                {
                    // 动态改变窗口位置
                    int pos = NotifyForm.Notify.Height / 100;
                    int top = Screen.PrimaryScreen.WorkingArea.Height - NotifyForm.Notify.Height;
                    if (NotifyForm.Notify.Top > top + pos * 10)
                    {
                        for (int i = 0; i < 35; i++)
                        {
                            NotifyForm.Notify.Top -= pos;
                        }
                    }
                    else
                    {
                        NotifyForm.Notify.Top = top;
                        if (NotifyForm.ShowInterval > NotifyForm.Interval)
                        {
                            NotifyForm.Timer.Interval = 1000;
                            NotifyForm.Interval++;
                        }
                        else
                        {
                            NotifyForm.Timer.Interval = 100;
                            if (NotifyForm.Notify != null)
                            {
                                if (NotifyForm.Notify.Opacity > 0)  // 动画降低窗口透明度
                                {
                                    NotifyForm.Notify.Opacity -= 0.1;
                                }
                                else                                // 释放窗口资源
                                {
                                    NotifyForm.Interval = 0;
                                    NotifyForm.Timer.Enabled = false;
                                    NotifyForm.Timer.Dispose();
                                    NotifyForm.Timer = null;
                                    NotifyForm.Notify.Dispose();
                                    NotifyForm.Notify = null;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("NotifyForm.Timer_Tick(object, EventArgs) :: " + ex.Message);
                throw ex;
            }
        }
        #endregion

        #region Override Methods

        /// <summary>
        /// 在窗口加载时，初始化部分数据
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!this.DesignMode)
            {
                int xPos = Screen.PrimaryScreen.WorkingArea.Width - this.Width;
                int yPos = Screen.PrimaryScreen.WorkingArea.Height;
                this.Location = new Point(xPos, yPos);

                this.TopMost = true;
                this.TopLevel = true;
                this.ShowIcon = false;
                base.IsResize = false;
                this.SysButton = ESysButton.Close;
                base.BackColor = Color.FromArgb(0, 122, 204);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (this._notifyText != string.Empty)
            {
                Graphics g = e.Graphics;
                StringFormat sf = new StringFormat();
                sf.FormatFlags = StringFormatFlags.LineLimit;
                sf.Trimming = StringTrimming.EllipsisCharacter;
                using (Brush brush = new SolidBrush(this.NotifyForeColor))
                {
                    g.DrawString(this.NotifyText, this.NotifyFont, brush, this.NotifyTextRect, sf);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            Graphics g = e.Graphics;
            Rectangle rect = this.ClientRectangle;
            // 左上角
            DrawHelper.DrawImage(g, this._backImage, 0, 0, 5, 5, 5, 5, 10, 10);
            // 左边
            DrawHelper.DrawImage(g, this._backImage, 0, 5, 5, this.Height - 10, 5, 10, 10, this._backImage.Height - 20);
            // 左下角
            DrawHelper.DrawImage(g, this._backImage, 0, this.Height - 5, 5, 5, 5, this._backImage.Height - 20, 5, 5);

            // 右上角
            DrawHelper.DrawImage(g, this._backImage, this.Width - 5, 0, 5, 5, this._backImage.Width - 10, 5, 5, 5);
            // 右边
            DrawHelper.DrawImage(g, this._backImage, this.Width - 5, 5, 5, this.Height - 10, this._backImage.Width - 10, 10, 5, this._backImage.Height - 20);
            // 右下角
            DrawHelper.DrawImage(g, this._backImage, this.Width - 5, this.Height - 5, 5, 5, this._backImage.Width - 10, this._backImage.Height - 10, 5, 5);

            // 上边
            DrawHelper.DrawImage(g, this._backImage, 5, 0, this.Width - 10, 5, 10, 5, this._backImage.Width - 20, 5);
            // 下边
            DrawHelper.DrawImage(g, this._backImage, 5, this.Height - 5, this.Width - 10, 5, 10, this._backImage.Height - 10, this._backImage.Width - 20, 5);
            // 填充
            DrawHelper.DrawImage(g, this._backImage, 5, 5, this.Width - 10, this.Height - 10, 10, 10, this._backImage.Width - 20, this._backImage.Height - 20);

            // 划分线
            g.DrawImage(
                this._splitImage,
                new Rectangle(this.ControlRect.X, this.ControlRect.Y, this._splitImage.Width, this._splitImage.Height),
                0,
                0,
                this._splitImage.Width,
                this._splitImage.Height,
                GraphicsUnit.Pixel);
        }

        #endregion
    }
}
