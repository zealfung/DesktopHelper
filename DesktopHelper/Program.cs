using System;
using System.Windows.Forms;
using DesktopHelper.UI;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Reflection;

namespace DesktopHelper
{
    static class Program
    {
        [DllImport("User32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);
        [DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
        private const int WS_SHOWNORMAL = 0x10;

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                //获取正在运行的进程实例
                Process instance = RunningInstance();
                if (instance == null)
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new AdapterForm());                    
                }
            }
            catch
            { 
            }
        }

        static Process RunningInstance()
        {
            try
            {
                Process current = Process.GetCurrentProcess();
                Process[] processes = Process.GetProcessesByName(current.ProcessName);
                //循环所有同名进程
                foreach (Process process in processes)
                {
                    //忽略当前进程
                    if (process.Id != current.Id)
                    {
                        //确保该进程也是从本exe启动的进程
                        if (Assembly.GetExecutingAssembly().Location.Replace("/", "\\")
                            == current.MainModule.FileName)
                        {
                            return process;
                        }
                    }
                }
                //如果没有其他同名进程存在，则返回null
                return null;
            }
            catch
            {
                return null;
            }
        }
    }
}
