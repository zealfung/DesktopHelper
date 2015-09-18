using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Diagnostics;
using System.ComponentModel;

namespace Andwho.Windows.Win32
{
    /// <summary>
    /// 鼠标钩子
    /// </summary>
    public class MouseHook
    {
        #region 变量
        /// <summary>
        /// 处理鼠标钩子的过程
        /// </summary>
        private int MouseHookHandle;
        private HookProc MouseDelegate;
        /// <summary>
        /// 记录鼠标所按下的键
        /// </summary>
        private MouseButtons MouseButton;
        /// <summary>
        /// 双击鼠标的间隔计时器
        /// </summary>
        private Timer DoubleClickTimer;
        private int Old_X;
        private int Old_Y;
        #endregion

        #region 事件
        /// <summary>
        /// 
        /// </summary>
        private event MouseEventHandler EventMouseMove;
        /// <summary>
        /// 
        /// </summary>
        private event MouseEventHandler EventMouseDown;
        /// <summary>
        /// 
        /// </summary>
        private event MouseEventHandler EventMouseUp;
        /// <summary>
        /// 
        /// </summary>
        private event MouseEventHandler EventMouseWheel;
        /// <summary>
        /// 
        /// </summary>
        private event MouseEventHandler EventMouseClick;
        /// <summary>
        /// 
        /// </summary>
        private event MouseEventHandler EventMouseDoubleClick;
        /// <summary>
        /// 
        /// </summary>
        private event EventHandler<MouseEventExtArgs> EventMouseMoveExt;
        /// <summary>
        /// 
        /// </summary>
        private event EventHandler<MouseEventExtArgs> EventMouseClickExt;

        /// <summary>
        /// 
        /// </summary>
        public event MouseEventHandler MouseMove
        {
            add
            {
                this.Install_Hook();
                this.EventMouseMove += value;
            }
            remove
            {
                this.EventMouseMove -= value;
                this.Uninstall_Hook();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public event MouseEventHandler MouseDown
        {
            add
            {
                this.Install_Hook();
                this.EventMouseDown += value;
            }
            remove
            {
                this.EventMouseDown -= value;
                this.Uninstall_Hook();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public event MouseEventHandler MouseUp
        {
            add
            {
                this.Install_Hook();
                this.EventMouseUp += value;
            }
            remove
            {
                this.EventMouseUp -= value;
                this.Uninstall_Hook();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public event MouseEventHandler MouseWheel
        {
            add
            {
                this.Install_Hook();
                this.EventMouseWheel += value;
            }
            remove
            {
                this.EventMouseWheel -= value;
                this.Uninstall_Hook();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public event MouseEventHandler MouseClick
        {
            add
            {
                this.Install_Hook();
                this.EventMouseClick += value;
            }
            remove
            {
                this.EventMouseClick -= value;
                this.Uninstall_Hook();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public event MouseEventHandler MouseDoubleClick
        {
            add
            {
                this.Install_Hook();
                if (this.EventMouseDoubleClick == null)
                {
                    this.DoubleClickTimer = new Timer
                    {
                        Interval = NativeMethods.GetDoubleClickTime(),
                        Enabled = false
                    };
                    this.DoubleClickTimer.Tick += new EventHandler(DoubleClickTimer_Tick);
                    this.MouseUp += this.OnMouseUp;
                }
                this.EventMouseDoubleClick += value;
            }
            remove
            {
                if (this.EventMouseDoubleClick != null)
                {
                    this.EventMouseDoubleClick -= value;
                    if (this.EventMouseDoubleClick == null)
                    {
                        this.MouseUp -= this.OnMouseUp;
                        // 释放计时器
                        this.DoubleClickTimer.Tick -= DoubleClickTimer_Tick;
                        this.DoubleClickTimer.Dispose();
                        this.DoubleClickTimer = null;
                    }
                }
                this.Uninstall_Hook();
            }
        }

        #endregion

        #region 方法
        /// <summary>
        /// 安装钩子
        /// </summary>
        public void Install_Hook()
        {
            if (this.MouseHookHandle == 0)
            {
                MouseDelegate = MouseHookProc;
                this.MouseHookHandle = NativeMethods.SetWindowsHookEx(
                    HookType.WH_MOUSE_LL,
                    this.MouseDelegate,
                    NativeMethods.GetModuleHandle(Process.GetCurrentProcess().MainModule.ModuleName),
                    0);
                if (this.MouseHookHandle == 0)
                {
                    int errorCode = Marshal.GetLastWin32Error();
                    throw new Win32Exception("MouseHook.EnsureGlobalMouseEvents()->" + NativeMethods.GetLastErrorString(errorCode));
                }
            }
        }

        /// <summary>
        /// 卸载钩子
        /// </summary>
        public void Uninstall_Hook()
        {
            if (this.EventMouseClick == null &&
                this.EventMouseDown == null &&
                this.EventMouseMove == null &&
                this.EventMouseUp == null &&
                this.EventMouseWheel == null &&
                this.EventMouseClickExt == null &&
                this.EventMouseMoveExt == null)
            {
                if (this.MouseHookHandle != 0)
                {
                    // 卸载钩子
                    int result = NativeMethods.UnhookWindowsHookEx(this.MouseHookHandle);
                    this.MouseHookHandle = 0;
                    this.MouseDelegate = null;
                    if (result == 0)
                    {
                        int errorCode = Marshal.GetLastWin32Error();
                        throw new Win32Exception("MouseHook.Uninstall_Hook()->" + NativeMethods.GetLastErrorString(errorCode));
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nCode"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        private int MouseHookProc(int nCode, int wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                MouseHookStruct mouseHookStruct = (MouseHookStruct)Marshal.PtrToStructure(lParam, typeof(MouseHookStruct));

                MouseButtons button = MouseButtons.None;
                short mouseDelta = 0;
                int clickCount = 0;
                bool mouseDown = false;
                bool mouseUp = false;

                switch (wParam)
                {
                    case (int)WindowsMessage.WM_LBUTTONDOWN:
                        mouseDown = true;
                        button = MouseButtons.Left;
                        clickCount = 1;
                        break;
                    case (int)WindowsMessage.WM_LBUTTONUP:
                        mouseUp = true;
                        button = MouseButtons.Left;
                        clickCount = 1;
                        break;
                    case (int)WindowsMessage.WM_LBUTTONDBLCLK:
                        button = MouseButtons.Left;
                        clickCount = 2;
                        break;
                    case (int)WindowsMessage.WM_RBUTTONDOWN:
                        mouseDown = true;
                        button = MouseButtons.Right;
                        clickCount = 1;
                        break;
                    case (int)WindowsMessage.WM_RBUTTONUP:
                        mouseUp = true;
                        button = MouseButtons.Right;
                        clickCount = 1;
                        break;
                    case (int)WindowsMessage.WM_RBUTTONDBLCLK:
                        button = MouseButtons.Right;
                        clickCount = 2;
                        break;
                    case (int)WindowsMessage.WM_MOUSEWHEEL:
                        mouseDelta = (short)((mouseHookStruct.MouseData >> 16) & 0xffff);
                        break;
                }

                MouseEventExtArgs e = new MouseEventExtArgs(
                    button,
                    clickCount,
                    mouseHookStruct.Point.X,
                    mouseHookStruct.Point.Y,
                    mouseDelta);

                if (this.EventMouseUp != null && mouseUp)
                    this.EventMouseUp.Invoke(null, e);
                if (this.EventMouseDown != null && mouseDown)
                    this.EventMouseDown.Invoke(null, e);
                if (this.EventMouseClick != null && clickCount > 0)
                    this.EventMouseClick.Invoke(null, e);
                if (this.EventMouseClickExt != null && clickCount > 0)
                    this.EventMouseClickExt.Invoke(null, e);
                if (this.EventMouseDoubleClick != null && clickCount == 2)
                    this.EventMouseDoubleClick.Invoke(null, e);
                if (this.EventMouseWheel != null && mouseDelta != 0)
                    this.EventMouseWheel.Invoke(null, e);
                if ((this.EventMouseMove != null || this.EventMouseMoveExt != null) &&
                    (this.Old_X != mouseHookStruct.Point.X || this.Old_Y != mouseHookStruct.Point.Y))
                {
                    this.Old_X = mouseHookStruct.Point.X;
                    this.Old_Y = mouseHookStruct.Point.Y;
                    if (EventMouseMove != null)
                        this.EventMouseMove.Invoke(null, e);
                    if (EventMouseMoveExt != null)
                        this.EventMouseMoveExt.Invoke(null, e);
                }
                if (e.Handled)
                    return -1;
            }
            return NativeMethods.CallNextHookEx(this.MouseHookHandle, nCode, wParam, lParam);
        }
        #endregion

        #region 事件处理
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            if (e.Clicks < 1)
                return;
            if (e.Button.Equals(this.MouseButton))
            {
                if (this.EventMouseDoubleClick != null)
                    this.EventMouseDoubleClick.Invoke(null, e);
                // 停止计时器
                this.DoubleClickTimer.Enabled = false;
                this.MouseButton = MouseButtons.None;
            }
            else
            {
                this.DoubleClickTimer.Enabled = true;
                this.MouseButton = e.Button;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DoubleClickTimer_Tick(object sender, EventArgs e)
        {
            this.DoubleClickTimer.Enabled = false;
            this.MouseButton = MouseButtons.None;
        }
        #endregion
    }
}
