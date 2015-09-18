using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.IO;
using System.Diagnostics;
using System.Management;
using System.Threading;
using DesktopHelper.Util;
using System.Data;
using DesktopHelper.DataAccess;
using System.Windows.Interop;
using System.Runtime.InteropServices;


namespace DesktopHelper.UI
{
    /// <summary>
    /// Adapter.xaml 的交互逻辑
    /// </summary>
    public partial class Adapter : Window
    {
        NavigationForm navigationForm = null;
        CalendarForm calendarForm = null;
        ConfigForm configForm = null;
        AboutBox aboutBox = null;
        UpdateForm updateForm = null;
        Thread clearMemoryThread = null;
        CheckUpdate check = null;
        System.Drawing.Rectangle screenRect = new System.Drawing.Rectangle();
        System.Windows.Forms.AnchorStyles StopAanhor = System.Windows.Forms.AnchorStyles.None;
        decimal lv = new decimal(0.6);
        decimal huang = new decimal(0.8);
        [System.Runtime.InteropServices.DllImport("psapi.dll")]
        static extern int EmptyWorkingSet(IntPtr hwProc);

        public Adapter()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            try
            {
                UpdateAndwhoData();

                screenRect = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea;

                //程序启动后，窗体在屏幕右侧中部
                this.Left = screenRect.Right-this.Width;
                this.Top = (screenRect.Bottom - this.Height) / 2;

                navigationForm = new NavigationForm();
                calendarForm = new CalendarForm();

                PhysicalMemoryTimer_Tick(null, null);
                System.Windows.Forms.Timer PhysicalMemoryTimer = new System.Windows.Forms.Timer();
                PhysicalMemoryTimer.Tick += new EventHandler(PhysicalMemoryTimer_Tick);
                PhysicalMemoryTimer.Interval = 3000;//时间
                PhysicalMemoryTimer.Enabled = true;


                System.Windows.Forms.Timer StopRectTimer = new System.Windows.Forms.Timer();
                StopRectTimer.Tick += new EventHandler(StopRectTimer_Tick);
                StopRectTimer.Interval = 1000;//时间
                StopRectTimer.Enabled = true;

                CheckNewVersion();
            }
            catch
            {
            }            
        }

        #region 更新Andwho.db
        private void UpdateAndwhoData()
        {
            try
            {
                if (File.Exists(System.Windows.Forms.Application.StartupPath + @"\Update\DeskHelper.xml"))
                {
                    File.Delete(System.Windows.Forms.Application.StartupPath + @"\Update\DeskHelper.xml");
                }
                if (File.Exists(System.Windows.Forms.Application.StartupPath + @"\Andwho.zip"))
                {
                    UnZipClass unZip = new UnZipClass();
                    string errMsg = string.Empty;
                    bool success = unZip.UnZipFile(System.Windows.Forms.Application.StartupPath + @"\Andwho.zip", System.Windows.Forms.Application.StartupPath + @"\", out errMsg);
                    if (success && SaveUserConfig())
                    {
                        File.Delete(System.Windows.Forms.Application.StartupPath + @"\Data\Andwho.db");
                        File.Delete(System.Windows.Forms.Application.StartupPath + @"\Andwho.zip");
                        File.Move(System.Windows.Forms.Application.StartupPath + @"\Andwho.db", System.Windows.Forms.Application.StartupPath + @"\Data\Andwho.db");
                    }
                }
                #region Image
                if (!Directory.Exists(System.Windows.Forms.Application.StartupPath + @"\Image"))
                {
                    Directory.CreateDirectory(System.Windows.Forms.Application.StartupPath + @"\Image");
                }
                if (File.Exists(System.Windows.Forms.Application.StartupPath + @"\lv.png"))
                {
                    if (!File.Exists(System.Windows.Forms.Application.StartupPath + @"\Image\lv.png"))
                        File.Move(System.Windows.Forms.Application.StartupPath + @"\lv.png", System.Windows.Forms.Application.StartupPath + @"\Image\lv.png");
                }
                if (File.Exists(System.Windows.Forms.Application.StartupPath + @"\huang.png"))
                {
                    if (!File.Exists(System.Windows.Forms.Application.StartupPath + @"\Image\huang.png"))
                        File.Move(System.Windows.Forms.Application.StartupPath + @"\huang.png", System.Windows.Forms.Application.StartupPath + @"\Image\huang.png");
                }
                if (File.Exists(System.Windows.Forms.Application.StartupPath + @"\hong.png"))
                {
                    if (!File.Exists(System.Windows.Forms.Application.StartupPath + @"\Image\hong.png"))
                        File.Move(System.Windows.Forms.Application.StartupPath + @"\hong.png", System.Windows.Forms.Application.StartupPath + @"\Image\hong.png");
                }
                #endregion
            }
            catch
            {
            }
        }

        private bool SaveUserConfig()
        {
            bool result = false;
            try
            {
                UpdateAndwhoData data = new UpdateAndwhoData();
                int i = 0;
                //转移"网址设置表"数据
                DataTable dtWebsiteConfig = data.GetWebsiteConfig();
                i = data.InsertWebsiteConfig2NewAndwho(dtWebsiteConfig);
                //转移"设置表"数据
                DataTable dtConfig = data.GetConfig();
                i += data.InsertConfig2NewConfig(dtConfig);
                //转移"提醒表"数据
                DataTable dtTixing = data.GetTixing();
                i += data.InsertTixing2NewTixing(dtTixing);
                if (i > 0) result = true;
            }
            catch
            {
            }
            return result;
        }
        #endregion

        #region 处理浮窗停靠
        private void StopRectTimer_Tick(object sender, EventArgs e)
        {
            switch (this.StopAanhor)
            {
                case System.Windows.Forms.AnchorStyles.Top:
                    this.Top = 0;
                    break;
                case System.Windows.Forms.AnchorStyles.Left:
                    this.Left = 0;
                    break;
                case System.Windows.Forms.AnchorStyles.Right:
                    this.Left = screenRect.Right - this.Width;
                    break;
                case System.Windows.Forms.AnchorStyles.Bottom:
                    this.Top = screenRect.Bottom - this.Height;
                    break;
            }
        }
        // 判断停靠位置
        private void mStopAnthor()
        {
            if (this.Top < screenRect.Bottom / 2)
            {
                if (this.Left < screenRect.Right / 2)
                {
                    if (this.Top < this.Left) StopAanhor = System.Windows.Forms.AnchorStyles.Top;
                    else StopAanhor = System.Windows.Forms.AnchorStyles.Left;
                }
                else
                {
                    if (this.Top < (screenRect.Right - this.Left + this.Width)) StopAanhor = System.Windows.Forms.AnchorStyles.Top;
                    else StopAanhor = System.Windows.Forms.AnchorStyles.Right;
                }
            }
            else
            {
                if (this.Left < screenRect.Right / 2)
                {
                    if ((screenRect.Bottom - this.Top - this.Height) < this.Left) StopAanhor = System.Windows.Forms.AnchorStyles.Bottom;
                    else StopAanhor = System.Windows.Forms.AnchorStyles.Left;
                }
                else
                {
                    if ((screenRect.Bottom - this.Top - this.Height) < (screenRect.Right - this.Left + this.Width)) StopAanhor = System.Windows.Forms.AnchorStyles.Bottom;
                    else StopAanhor = System.Windows.Forms.AnchorStyles.Right;
                }
            }
        }
        #endregion

        #region 鼠标事件
        private void Image_MouseEnter(object sender, MouseEventArgs e)
        {
            try
            {
                this.Opacity = 1;
                if (navigationForm != null && navigationForm.Visible == false && calendarForm.Visible == false)
                {
                    navigationForm.Visible = true;
                    navigationForm.ShowForm(StopAanhor, (int)this.Left, (int)this.Top);
                    navigationForm.Activate();
                }
            }
            catch
            { 
            }
        }

        private void Image_MouseLeave(object sender, MouseEventArgs e)
        {
            this.Opacity = 0.8;
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
            try
            {
                if (navigationForm == null || calendarForm == null) return;

                navigationForm.Hide();

                if (e.ClickCount == 2)
                {
                    ClearMemoryMenuItem_Click(null, null);
                }
                else if (e.ClickCount == 1)
                {
                    if (calendarForm.Visible)
                    {
                        calendarForm.Hide();
                    }
                    else
                    {
                        calendarForm.ShowForm(StopAanhor, (int)this.Left, (int)this.Top);
                        calendarForm.Activate();
                    }
                }
            }
            catch
            {
            }
        }

        private void imgBackground_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                navigationForm.Hide();
                calendarForm.Hide();
            }
            catch
            {
            }
        }

        private void Window_LocationChanged(object sender, EventArgs e)
        {
            mStopAnthor();
        }
        #endregion

        #region 右键菜单
        //设置
        private void ConfigMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                configForm = ConfigForm.GetInstance();
                configForm.ReloadConfigHandlor += new ConfigForm.ReloadConfigHandler(ReloadNavigation);
                configForm.Show();
                configForm.Activate();
            }
            catch
            { }
        }
        //清理内存
        private void ClearMemoryMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                clearMemoryThread = new Thread(ClearPhysicalMemoryThread);
                clearMemoryThread.IsBackground = true;
                clearMemoryThread.Start();
            }
            catch
            {
            }
        }
        //检查更新
        private void UpdateMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                check = new CheckUpdate();
                if (check.IsConnectedInternet())
                {
                    if (check.Download())
                    {
                        if (check.HasNewVersion())
                        {
                            updateForm = UpdateForm.GetInstance(check.NewVersion);
                            updateForm.CloseHandlor += new UpdateForm.CloseHandler(CloseDeskHelper);
                            updateForm.ShowForm();
                        }
                        else//HasNewVersion()
                        {
                            MessageBox.Show("已经是最新版本");
                        }
                    }
                    else//Download()
                    {
                        MessageBox.Show("已经是最新版本");
                    }
                }
                else//IsConnectedInternet()
                {
                    MessageBox.Show("本机没有连接互联网");
                }
            }
            catch
            {
            }
        }
        //关于
        private void AboutMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                aboutBox = AboutBox.GetInstance();
                aboutBox.Show();
                aboutBox.Activate();
            }
            catch (Exception)
            {
            }
        }
        //官方网站
        private void AndwhoMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string target = "http://www.andwho.com/";
                System.Diagnostics.Process.Start(target);
            }
            catch (Exception)
            {
            }
        }
        //退出
        private void ExitMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Environment.Exit(0);
                System.Diagnostics.Process.GetCurrentProcess().Kill();
            }
            catch
            {
            }
        }
        #endregion

        #region 内存整理
        private void PhysicalMemoryTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                decimal memory = (PhysicalMemory - FreePhysicalMemory) / PhysicalMemory;
                textBlock.Text = memory.ToString("p0");
                if (memory < lv
                    && File.Exists(System.Windows.Forms.Application.StartupPath + @"\Image\green.png"))
                {
                    var uriSource = new Uri(System.Windows.Forms.Application.StartupPath + @"\Image\green.png", UriKind.Absolute);
                    imgBackground.Source = new BitmapImage(uriSource);
                }
                else if (memory >= lv
                    && memory < huang
                    && File.Exists(System.Windows.Forms.Application.StartupPath + @"\Image\yellow.png"))
                {
                    var uriSource = new Uri(System.Windows.Forms.Application.StartupPath + @"\Image\yellow.png", UriKind.Absolute);
                    imgBackground.Source = new BitmapImage(uriSource);
                }
                else if (File.Exists(System.Windows.Forms.Application.StartupPath + @"\Image\red.png"))
                {
                    var uriSource = new Uri(System.Windows.Forms.Application.StartupPath + @"\Image\red.png", UriKind.Absolute);
                    imgBackground.Source = new BitmapImage(uriSource);
                }
            }
            catch
            {
            }
        }

        // 释放内存
        private void ClearMemory()
        {
            try
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                Process[] processes = Process.GetProcesses();
                foreach (Process process in processes)
                {
                    //以下系统进程没有权限，所以跳过，防止出错影响效率。
                    if ((process.ProcessName == "System") && (process.ProcessName == "Idle"))
                        continue;
                    try
                    {
                        EmptyWorkingSet(process.Handle);
                    }
                    catch
                    {
                    }
                }
            }
            catch
            {
            }
        }

        //物理内存总大小
        private decimal PhysicalMemory
        {
            get
            {
                decimal physicalMemory = 0;
                try
                {
                    //获得物理内存 
                    ManagementClass mc = new ManagementClass("Win32_ComputerSystem");
                    ManagementObjectCollection moc = mc.GetInstances();
                    foreach (ManagementObject mo in moc)
                    {
                        if (mo["TotalPhysicalMemory"] != null)
                        {
                            physicalMemory = decimal.Parse(mo["TotalPhysicalMemory"].ToString());
                        }
                    }
                }
                catch
                {
                }
                return physicalMemory;
            }
        }

        // 可用物理内存大小
        private decimal FreePhysicalMemory
        {
            get
            {
                decimal availablebytes = 0;
                try
                {
                    ManagementClass mos = new ManagementClass("Win32_OperatingSystem");
                    foreach (ManagementObject mo in mos.GetInstances())
                    {
                        if (mo["FreePhysicalMemory"] != null)
                        {
                            availablebytes = 1024 * decimal.Parse(mo["FreePhysicalMemory"].ToString());
                        }
                    }
                }
                catch
                {
                }
                return availablebytes;
            }
        }
        #endregion

        #region 私有函数
        //检查更新
        private void CheckNewVersion()
        {
            try
            {
                check = new CheckUpdate();
                if (check.IsConnectedInternet())
                {
                    if (check.Download())
                    {
                        if (check.HasNewVersion())
                        {
                            updateForm = UpdateForm.GetInstance(check.NewVersion);
                            updateForm.CloseHandlor += new UpdateForm.CloseHandler(CloseDeskHelper);
                            updateForm.ShowForm();
                        }
                    }
                }
            }
            catch
            {
            }
        }
        //退出程序
        private void CloseDeskHelper()
        {
            ExitMenuItem_Click(null, null);
        }
        //重新加载导航数据
        private void ReloadNavigation()
        {
            try
            {
                navigationForm = new NavigationForm();
            }
            catch (Exception)
            {
            }
        }

        private void ClearPhysicalMemoryThread()
        {
            try
            {
                Random ra = new Random();
                for (int i = 0; i < 23; i++)
                {
                    GetValueChange(ra.Next(6, 50));
                    Thread.Sleep(50);
                }
                ClearMemory();
                PhysicalMemoryTimer_Tick(null, null);
            }
            catch
            {
            }
        }

        delegate void SetControlValue(int num);

        private void SetConrolsValue(int i)
        {
            try
            {
                this.textBlock.Text = string.Format("{0}%", i);
            }
            catch
            {
            }
        }

        private void GetValueChange(int i)
        {
            try
            {
                SetControlValue sc = new SetControlValue(SetConrolsValue);
                this.Dispatcher.BeginInvoke(sc, i);
            }
            catch
            {
            }
        }
        #endregion

        #region 限制窗体不能移出屏幕
        void win_SourceInitialized(object sender, EventArgs e)
        {
            HwndSource hwndSource = PresentationSource.FromVisual(this) as HwndSource;
            if (hwndSource != null)
            {
                hwndSource.AddHook(new HwndSourceHook(WndProc));
            }
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            this.win_SourceInitialized(this, e);
        }

        protected virtual IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (msg)
            {
                case 0X46: //WM_WINDOWPOSCHANGING
                    //WINDOWPOS windowPos = (WINDOWPOS)msg.GetLParam(typeof(WINDOWPOS));

                    //if (windowPos.x + windowPos.cx > screenRect.Right)
                    //{
                    //    windowPos.x = screenRect.Right - windowPos.cx;
                    //}

                    //if (windowPos.y + windowPos.cy > screenRect.Bottom)
                    //{
                    //    windowPos.y = screenRect.Bottom - windowPos.cy;
                    //}

                    //if (windowPos.x < screenRect.Top)
                    //{
                    //    windowPos.x = screenRect.Top;
                    //}

                    //if (windowPos.y < screenRect.Left)
                    //{
                    //    windowPos.y = 0;
                    //}

                    //Marshal.StructureToPtr(windowPos, lParam, false);
                    //base.WndProc(ref m);
                    break;
                default:
                    //base.WndProc(ref m);
                    break;
            }
            return IntPtr.Zero;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct WINDOWPOS
        {
            internal IntPtr hWnd;
            internal IntPtr hWndInsertAfter;
            internal int x;
            internal int y;
            internal int cx;
            internal int cy;
            internal int flags;
        }
        #endregion
    }
}
