using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Runtime.InteropServices;
using DesktopHelper.Util;
using System.Diagnostics;
using System.Management;
using System.IO;
using DesktopHelper.DataAccess;
using Andwho.Logger;

namespace DesktopHelper.UI
{
    public partial class AdapterForm : Form
    {
        NavigationForm form_Navigation = null;
        CalendarForm form_Calendar = null;
        AboutBox form_AboutBox = null;
        UpdateForm form_Update = null;
        ConfigForm form_Config = null;
        ClearSystemGarbageForm form_ClearSystemGarbage = null;
        CheckUpdate _checkUpdate = new CheckUpdate();
        Rectangle rectangle_Screen = new Rectangle();
        AnchorStyles StopAanhor = AnchorStyles.None;
        Thread thread_ClearMemory = null;
        Point point_MouseOffset;
        bool bool_MouseMoved = false;
        decimal decimal_Green = new decimal(0.6);
        decimal decimal_Yellow = new decimal(0.8);
        System.Windows.Forms.Timer updateTimer = new System.Windows.Forms.Timer();
        [DllImport("psapi.dll")]
        static extern int EmptyWorkingSet(IntPtr hwProc);
        Log log = new Log(true);

        public AdapterForm()
        {
            InitializeComponent();
            UpdateAndwhoData();
        }

        private void AdapterForm_Load(object sender, EventArgs e)
        {
            try
            {
                rectangle_Screen = SystemInformation.WorkingArea;
                //rectangle_Screen = Screen.GetWorkingArea(this);

                //程序启动后，窗体在屏幕右侧中部
                this.Left = SystemInformation.PrimaryMonitorMaximizedWindowSize.Width;
                this.Top = (SystemInformation.PrimaryMonitorMaximizedWindowSize.Height - this.Height) / 2;

                form_Navigation = new NavigationForm();
                form_Calendar = new CalendarForm();
                form_Navigation.Owner = this;
                form_Calendar.Owner = this;

                PhysicalMemoryTimer_Tick(null, null);
                System.Windows.Forms.Timer PhysicalMemoryTimer = new System.Windows.Forms.Timer();
                PhysicalMemoryTimer.Tick += new EventHandler(PhysicalMemoryTimer_Tick);
                PhysicalMemoryTimer.Interval = 3000;
                PhysicalMemoryTimer.Enabled = true;


                System.Windows.Forms.Timer StopRectTimer = new System.Windows.Forms.Timer();
                StopRectTimer.Tick += new EventHandler(StopRectTimer_Tick);
                StopRectTimer.Interval = 1000;
                StopRectTimer.Enabled = true;

                this.RegisterAppBar(false);

                updateTimer = new System.Windows.Forms.Timer();
                updateTimer.Tick += new EventHandler(CheckNewVersion_Tick);
                updateTimer.Interval = 10 * 1000;
                updateTimer.Enabled = true;             
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
                if (File.Exists(Application.StartupPath + @"\Andwho.zip"))
                {
                    UnZipClass unZip = new UnZipClass();
                    string errMsg = string.Empty;
                    bool success = unZip.UnZipFile(Application.StartupPath + @"\Andwho.zip", Application.StartupPath + @"\", out errMsg);
                    if (success && SaveUserConfig())
                    {
                        File.Delete(Application.StartupPath + @"\Data\Andwho.db");
                        File.Delete(Application.StartupPath + @"\Andwho.zip");
                        File.Move(Application.StartupPath + @"\Andwho.db", Application.StartupPath + @"\Data\Andwho.db");
                    }
                }
                #region Image
                if (File.Exists(Application.StartupPath + @"\hong.png"))
                {
                    File.Delete(Application.StartupPath + @"\hong.png");
                }
                if (File.Exists(Application.StartupPath + @"\huang.png"))
                {
                    File.Delete(Application.StartupPath + @"\huang.png");
                }
                if (File.Exists(Application.StartupPath + @"\lv.png"))
                {
                    File.Delete(Application.StartupPath + @"\lv.png");
                }
                //if (!Directory.Exists(Application.StartupPath + @"\Image"))
                //{
                //    Directory.CreateDirectory(Application.StartupPath + @"\Image");
                //}
                //if (File.Exists(Application.StartupPath + @"\green.png"))
                //{
                //    if (!File.Exists(Application.StartupPath + @"\Image\green.png"))
                //        File.Move(Application.StartupPath + @"\green.png", Application.StartupPath + @"\Image\green.png");
                //}
                //if (File.Exists(Application.StartupPath + @"\yellow.png"))
                //{
                //    if (!File.Exists(Application.StartupPath + @"\Image\yellow.png"))
                //        File.Move(Application.StartupPath + @"\yellow.png", Application.StartupPath + @"\Image\yellow.png");
                //}
                //if (File.Exists(Application.StartupPath + @"\red.png"))
                //{
                //    if (!File.Exists(Application.StartupPath + @"\Image\red.png"))
                //        File.Move(Application.StartupPath + @"\red.png", Application.StartupPath + @"\Image\red.png");
                //}
                #endregion
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
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
                //转移"定时表"数据
                DataTable dtDingshi = data.GetDingshi();
                i += data.InsertDingshi2NewDingshi(dtDingshi);
                if (i > 0) result = true;
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
            return result;
        }
        #endregion

        #region 处理浮窗停靠
        private void StopRectTimer_Tick(object sender, EventArgs e)
        {
            switch (this.StopAanhor)
            {
                case AnchorStyles.Top:
                    this.Location = new Point(this.Location.X, 0);
                    break;
                case AnchorStyles.Left:
                    this.Location = new Point(0, this.Location.Y);
                    break;
                case AnchorStyles.Right:
                    this.Location = new Point(rectangle_Screen.Right - this.Width, this.Location.Y);
                    break;
                case AnchorStyles.Bottom:
                    this.Location = new Point(this.Location.X, rectangle_Screen.Right - this.Height);
                    break;
            }
        }
        // 判断停靠位置
        private void mStopAnthor()
        {
            if (this.Top < rectangle_Screen.Bottom / 2)
            {
                if (this.Left < rectangle_Screen.Right / 2)
                {
                    if (this.Top < this.Left) StopAanhor = AnchorStyles.Top;
                    else StopAanhor = AnchorStyles.Left;
                }
                else
                {
                    if (this.Top < (rectangle_Screen.Right - this.Right)) StopAanhor = AnchorStyles.Top;
                    else StopAanhor = AnchorStyles.Right;
                }
            }
            else
            {
                if (this.Left < rectangle_Screen.Right / 2)
                {
                    if ((rectangle_Screen.Bottom - this.Bottom) < this.Left) StopAanhor = AnchorStyles.Bottom;
                    else StopAanhor = AnchorStyles.Left;
                }
                else
                {
                    if ((rectangle_Screen.Bottom - this.Bottom) < (rectangle_Screen.Right - this.Right)) StopAanhor = AnchorStyles.Bottom;
                    else StopAanhor = AnchorStyles.Right;
                }
            }
        }
        #endregion

        #region 鼠标事件
        private void AdapterForm_MouseHover(object sender, EventArgs e)
        {
            try
            {
                //Thread.Sleep(1000);
                this.Opacity = 1;
                if (form_Navigation != null && form_Navigation.Visible == false && form_Calendar.Visible == false)
                {
                    form_Navigation.Visible = true;
                    form_Navigation.ShowForm(StopAanhor, this.Location.X, this.Location.Y);
                    form_Navigation.Activate();
                }
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
        }

        private void AdapterForm_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                this.Opacity = 0.5;
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
        }

        private void AdapterForm_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (form_Navigation == null || form_Calendar == null) return;

                form_Navigation.Hide();
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
        }

        private void AdapterForm_DoubleClick(object sender, EventArgs e)
        {
            bool_MouseMoved = true;
            if (form_Calendar.Visible) form_Calendar.Hide();
            ClearMemoryToolStripMenuItem_Click(null, null);
        }

        private void AdapterForm_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                //创建一个路径对象
                GraphicsPath Myformpath = new GraphicsPath();
                //使用圆构造一个区域，并将此区域作为程序窗体区域
                Myformpath.AddEllipse(0, 0, 55, 55);
                this.Region = new Region(Myformpath);
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
        }

        private void AdapterForm_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                point_MouseOffset = new Point(-e.X, -e.Y);
                if (e.Button == MouseButtons.Right)
                {
                    form_Navigation.Hide();
                    form_Calendar.Hide();
                }
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
        }

        private void AdapterForm_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left && !bool_MouseMoved)
                {
                    if (form_Calendar.Visible)
                    {
                        form_Calendar.Hide();
                    }
                    else
                    {
                        form_Calendar.ShowForm(StopAanhor, this.Location.X, this.Location.Y);
                        form_Calendar.Activate();
                    }
                }
                bool_MouseMoved = false;
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
        }

        private void AdapterForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point mousePos = Control.MousePosition;
                mousePos.Offset(point_MouseOffset.X, point_MouseOffset.Y);

                if (Math.Abs(Location.X - mousePos.X) > 10 ||
                    Math.Abs(Location.Y - mousePos.Y) > 10)
                {
                    Location = mousePos;
                    bool_MouseMoved = true;
                }
            }
        }

        private void AdapterForm_LocationChanged(object sender, EventArgs e)
        {
            this.mStopAnthor();
        }
        #endregion

        #region 右键菜单
        //设置
        private void ConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                form_Config = ConfigForm.GetInstance();
                form_Config.ReloadConfigHandlor += new ConfigForm.ReloadConfigHandler(ReloadNavigation);
                form_Config.Show();
                form_Config.Activate();
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
        }
        //清理系统垃圾
        private void ClearGarbageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                form_ClearSystemGarbage = ClearSystemGarbageForm.GetInstance();
                form_ClearSystemGarbage.Show();
                form_ClearSystemGarbage.Activate();
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
        }
        //清理内存
        private void ClearMemoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                thread_ClearMemory = new Thread(ClearPhysicalMemoryThread);
                thread_ClearMemory.IsBackground = true;
                thread_ClearMemory.Start();
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
        }
        //检查更新
        private void UpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                _checkUpdate = new CheckUpdate();
                if (_checkUpdate.IsConnectedInternet())
                {
                    if (_checkUpdate.Download())
                    {
                        if (_checkUpdate.HasNewVersion())
                        {
                            form_Update = UpdateForm.GetInstance(_checkUpdate.NewVersion);
                            form_Update.CloseHandlor += new UpdateForm.CloseHandler(CloseDeskHelper);
                            form_Update.ShowForm();
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
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
        }
        //关于
        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                form_AboutBox = AboutBox.GetInstance();
                form_AboutBox.Show();
                form_AboutBox.Activate();
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
        }
        //官方网站
        private void AndwhoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string target = "http://www.andwho.com/";
                System.Diagnostics.Process.Start(target);
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
        }
        //退出
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.RegisterAppBar(true);
                Environment.Exit(0);
                System.Diagnostics.Process.GetCurrentProcess().Kill();
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
        }
        #endregion

        #region 私有函数
        //检查更新
        private void CheckNewVersion_Tick(object sender, EventArgs e)
        {
            try
            {
                _checkUpdate = new CheckUpdate();
                if (_checkUpdate.IsConnectedInternet())
                {
                    if (_checkUpdate.Download())
                    {
                        if (_checkUpdate.HasNewVersion())
                        {
                            form_Update = UpdateForm.GetInstance(_checkUpdate.NewVersion);
                            form_Update.CloseHandlor += new UpdateForm.CloseHandler(CloseDeskHelper);
                            form_Update.ShowForm();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
            finally
            {
                updateTimer.Enabled = false;
            }
        }

        //退出程序
        private void CloseDeskHelper()
        {
            ExitToolStripMenuItem_Click(null, null);
        }

        private void ReloadNavigation()
        {
            try
            {
                form_Navigation = new NavigationForm();
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
        }

        private void ClearPhysicalMemoryThread()
        {
            try
            {
                Random ra = new Random();
                for (int i = 0; i < 23; i++)
                {
                    SetControlText(labelPhysicalMemory, string.Format("{0}%", ra.Next(6, 50)));
                    Thread.Sleep(50);
                }
                ClearMemory();
                PhysicalMemoryTimer_Tick(null, null);
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
        }

        delegate void SetControlTextHandler(Control control, string text);

        private void SetControlText(Control control, string text)
        {
            try
            {
                if (control.InvokeRequired == true)
                {
                    SetControlTextHandler set = new SetControlTextHandler(SetControlText);
                    control.Invoke(set, new object[] { control, text });
                }
                else
                {
                    control.Text = text;
                }
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
        }
        #endregion

        #region 限制窗体不能移出屏幕
        protected override void WndProc(ref Message m)
        {
            try
            {
                if (m.Msg == uCallBackMsg)
                {
                    switch (m.WParam.ToInt32())
                    {
                        case (int)ABNotify.ABN_FULLSCREENAPP:
                            {
                                if ((int)m.LParam == 1)
                                {
                                    this.RunningFullScreenApp = true;
                                    this.TopMost = false;
                                }
                                else
                                {
                                    this.RunningFullScreenApp = false;
                                    this.TopMost = true;
                                }
                                break;
                            }
                        default:
                            break;
                    }
                }
                switch (m.Msg)
                {
                    case 0X46: //WM_WINDOWPOSCHANGING
                        WINDOWPOS windowPos = (WINDOWPOS)m.GetLParam(typeof(WINDOWPOS));

                        if (windowPos.x + windowPos.cx > rectangle_Screen.Right)
                        {
                            windowPos.x = rectangle_Screen.Right - windowPos.cx;
                        }

                        if (windowPos.y + windowPos.cy > rectangle_Screen.Bottom)
                        {
                            windowPos.y = rectangle_Screen.Bottom - windowPos.cy;
                        }

                        if (windowPos.x < rectangle_Screen.Top)
                        {
                            windowPos.x = rectangle_Screen.Top;
                        }

                        if (windowPos.y < rectangle_Screen.Left)
                        {
                            windowPos.y = 0;
                        }

                        Marshal.StructureToPtr(windowPos, m.LParam, false);
                        base.WndProc(ref m);
                        break;
                    default:
                        base.WndProc(ref m);
                        break;
                }
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
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

        #region 内存整理
        private void PhysicalMemoryTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                decimal decimal_Memory = (PhysicalMemory - FreePhysicalMemory) / PhysicalMemory;
                SetControlText(labelPhysicalMemory, decimal_Memory.ToString("p0"));
                //labelPhysicalMemory.Text = decimal_Memory.ToString("p0");
                if (decimal_Memory < decimal_Green
                    && File.Exists(Application.StartupPath + @"\Image\green.png"))
                {
                    this.BackgroundImage = Image.FromFile(Application.StartupPath + @"\Image\green.png");
                }
                else if (decimal_Memory >= decimal_Green 
                    && decimal_Memory < decimal_Yellow
                    && File.Exists(Application.StartupPath + @"\Image\yellow.png"))
                {
                    this.BackgroundImage = Image.FromFile(Application.StartupPath + @"\Image\yellow.png");
                }
                else if (File.Exists(Application.StartupPath + @"\Image\red.png"))
                {
                    this.BackgroundImage = Image.FromFile(Application.StartupPath + @"\Image\red.png");
                }
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
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
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
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
                catch (Exception ex)
                {
                    log.WriteLog(ex.ToString());
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
                catch (Exception ex)
                {
                    log.WriteLog(ex.ToString());
                }
                return availablebytes;
            }
        }
        #endregion

        #region 检测是否有程序全屏运行
        public class APIWrapper
        {
            [DllImport("SHELL32", CallingConvention = CallingConvention.StdCall)]
            public static extern uint SHAppBarMessage(int dwMessage, ref APPBARDATA pData);
            [DllImport("User32.dll", CharSet = CharSet.Auto)]
            public static extern int RegisterWindowMessage(string msg);
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;

            public override string ToString()
            {
                return "{left=" + left.ToString() + ", " + "top=" + top.ToString() + ", " +
                "right=" + right.ToString() + ", " + "bottom=" + bottom.ToString() + "}";
            }
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct APPBARDATA
        {
            public int cbSize;
            public IntPtr hWnd;
            public int uCallbackMessage;
            public int uEdge;
            public RECT rc;
            public IntPtr lParam;
        }
        public enum ABMsg : int
        {
            ABM_NEW = 0,
            ABM_REMOVE,
            ABM_QUERYPOS,
            ABM_SETPOS,
            ABM_GETSTATE,
            ABM_GETTASKBARPOS,
            ABM_ACTIVATE,
            ABM_GETAUTOHIDEBAR,
            ABM_SETAUTOHIDEBAR,
            ABM_WINDOWPOSCHANGED,
            ABM_SETSTATE
        }
        public enum ABNotify : int
        {
            ABN_STATECHANGE = 0,
            ABN_POSCHANGED,
            ABN_FULLSCREENAPP,
            ABN_WINDOWARRANGE
        }
        public enum ABEdge : int
        {
            ABE_LEFT = 0,
            ABE_TOP,
            ABE_RIGHT,
            ABE_BOTTOM
        }

        bool RunningFullScreenApp = false;
        int uCallBackMsg;
        private void RegisterAppBar(bool registered)
        {
            try
            {
                APPBARDATA abd = new APPBARDATA();
                abd.cbSize = Marshal.SizeOf(abd);
                abd.hWnd = this.Handle;
                if (!registered)
                {
                    uCallBackMsg = APIWrapper.RegisterWindowMessage("APPBARMSG_CSDN_HELPER");
                    abd.uCallbackMessage = uCallBackMsg;
                    uint ret = APIWrapper.SHAppBarMessage((int)ABMsg.ABM_NEW, ref abd);
                }
                else
                {
                    APIWrapper.SHAppBarMessage((int)ABMsg.ABM_REMOVE, ref abd);
                }
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
        }
        #endregion
    }    
}
