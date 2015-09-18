using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace DesktopHelper.UI
{
    public partial class UpdateForm : Form
    {
        private System.Drawing.Rectangle Rect;//定义一个存储矩形框的区域
        private static int AW_SLIDE = 0x00040000;//该变量表示出现滑行效果的窗体
        private static int AW_VER_NEGATIVE = 0x00000008;//该变量表示从下向上开屏 
        private static UpdateForm form = null;
        private static object synLock = new object();
        private string _version = string.Empty;
        public delegate void CloseHandler();
        public event CloseHandler CloseHandlor;

        public static UpdateForm GetInstance(string version)
        {
            if (form == null)
            {
                lock (synLock)
                {
                    if (form == null) form = new UpdateForm(version);
                }
            }
            return form;
        }

        private UpdateForm(string version)
        {
            InitializeComponent();
            _version = version;
        }

        #region 声明API函数
        [DllImportAttribute("user32.dll")]
        private static extern bool ShowWindow(IntPtr hwnd, int dwTime, int dwFlags);
        #endregion

        public void ShowForm()
        {
            try
            {
                this.labelTip.Text = string.Format("发现新版本 {0}", _version);

                System.Drawing.Rectangle rect = System.Windows.Forms.Screen.GetWorkingArea(this);//实例化一个当前窗口的对象
                this.Rect = new System.Drawing.Rectangle(rect.Right - this.Width - 1, rect.Bottom - this.Height - 1, this.Width, this.Height);//为实例化的对象创建工作区域

                this.SetBounds(Rect.X, Rect.Y, Rect.Width, Rect.Height);//设置当前窗体的边界
                ShowWindow(this.Handle, 4, AW_SLIDE + AW_VER_NEGATIVE);//动态显示本窗体
            }
            catch
            { }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnClose_MouseEnter(object sender, EventArgs e)
        {
            BtnClose.BackColor = Color.Red;
        }

        private void BtnClose_MouseLeave(object sender, EventArgs e)
        {
            BtnClose.BackColor = Color.Transparent;
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(Application.StartupPath + @"\aUpdate.exe");
                CloseHandlor();
                this.Close();
            }
            catch (Exception)
            {
            }
        }

        private void BtnOK_MouseEnter(object sender, EventArgs e)
        {
            BtnOK.BackColor = Color.DeepSkyBlue;
        }

        private void BtnOK_MouseLeave(object sender, EventArgs e)
        {
            BtnOK.BackColor = Color.DodgerBlue;
        }

        #region 处理Label控件闪烁问题
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }
        #endregion

        private void UpdateForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (form != null)
            {
                form.Dispose();
                form = null;
            }
        }
    }
}
