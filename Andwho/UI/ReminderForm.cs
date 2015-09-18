using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Andwho.Entity;

namespace Andwho.UI
{
    public partial class ReminderForm : Form
    {
        private System.Drawing.Rectangle Rect;//定义一个存储矩形框的区域
        private static int AW_SLIDE = 0x00040000;//该变量表示出现滑行效果的窗体
        private static int AW_VER_NEGATIVE = 0x00000008;//该变量表示从下向上开屏 
        private Reminder _reminder = new Reminder();
        private string _musicPath = string.Empty;

        public ReminderForm(Reminder reminder,string musicPath)
        {
            InitializeComponent();
            _reminder = reminder;
            _musicPath = musicPath;
            InitInfo();
        }

        #region 声明API函数
        [DllImportAttribute("user32.dll")]
        private static extern bool ShowWindow(IntPtr hwnd, int dwTime, int dwFlags);
        #endregion

        private void ShowForm()
        {
            try
            {
                System.Drawing.Rectangle rect = System.Windows.Forms.Screen.GetWorkingArea(this);//实例化一个当前窗口的对象
                this.Rect = new System.Drawing.Rectangle(rect.Right - this.Width - 1, rect.Bottom - this.Height - 1, this.Width, this.Height);//为实例化的对象创建工作区域

                this.SetBounds(Rect.X, Rect.Y, Rect.Width, Rect.Height);//设置当前窗体的边界
                ShowWindow(this.Handle, 4, AW_SLIDE + AW_VER_NEGATIVE);//动态显示本窗体
            }
            catch
            { }
        }

        private void InitInfo()
        {
            this.labelInfo.Text = _reminder.ReminderInfo;            
        }

        private void BtnGotit_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void BtnGotit_MouseEnter(object sender, EventArgs e)
        {
            BtnGotit.BackColor = Color.DodgerBlue;
        }

        private void BtnGotit_MouseLeave(object sender, EventArgs e)
        {
            BtnGotit.BackColor = Color.Transparent;
        }

        private void ReminderForm_Load(object sender, EventArgs e)
        {
            ShowForm();
            
            try
            {
                this.TopMost = true;
                if (!string.IsNullOrEmpty(_musicPath))
                {
                    axWindowsMediaPlayer.URL = _musicPath;
                }
            }
            catch
            { }
        }
    }
}
